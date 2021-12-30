﻿using Microsoft.JSInterop;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ComponentBuilder
{
    /// <summary>
    /// The extensions of component builder.
    /// </summary>
    public static class ComponentBuilderExtensions
    {
        /// <summary>
        /// Try to get specified attribute from <see cref=" Type"/> instance.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <param name="type">The instance of type.</param>
        /// <param name="attribute">If found, return a specified attribute instance, otherwise return <c>null</c>.</param>
        /// <returns><c>true</c> if found the specified attribute, otherwise <c>false</c>.</returns>
        public static bool TryGetCustomAttribute<TAttribute>(this Type type, out TAttribute attribute) where TAttribute : Attribute
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            attribute = type.GetCustomAttribute<TAttribute>();
            return attribute != null;
        }
        /// <summary>
        /// Try to get specified attribute from <see cref="FieldInfo"/> instance.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <param name="field">The instance of field.</param>
        /// <param name="attribute">If found, return a specified attribute instance, otherwise return <c>null</c>.</param>
        /// <returns><c>true</c> if found the specified attribute, otherwise <c>false</c>.</returns>
        public static bool TryGetCustomAttribute<TAttribute>(this FieldInfo field, out TAttribute attribute) where TAttribute : Attribute
        {
            if (field is null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            attribute = field.GetCustomAttribute<TAttribute>();
            return attribute != null;
        }
        /// <summary>
        /// Try to get specified attribute from <see cref="PropertyInfo"/> instance.
        /// </summary>
        /// <typeparam name="TAttribute">The type of attribute.</typeparam>
        /// <param name="property">The instance of property.</param>
        /// <param name="attribute">If found, return a specified attribute instance, otherwise return <c>null</c>.</param>
        /// <returns><c>true</c> if found the specified attribute, otherwise <c>false</c>.</returns>
        public static bool TryGetCustomAttribute<TAttribute>(this PropertyInfo property, out TAttribute attribute) where TAttribute : Attribute
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            attribute = property.GetCustomAttribute<TAttribute>();
            return attribute != null;
        }
        /// <summary>
        /// Gets CSS class value from parameters which has defined <see cref="CssClassAttribute"/> attribute.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="instace">Object to get value from property</param>
        /// <returns>A key/value pairs contains CSS class and value.</returns>
        public static IEnumerable<KeyValuePair<string,object>> GetCssClassAttributesInOrderFromParameters(this IEnumerable<PropertyInfo> properties,object instace)
        {
            return properties.Where(m => m.IsDefined(typeof(CssClassAttribute)))
                .Select(m => new { property = m, attr = m.GetCustomAttribute<CssClassAttribute>() })
                .OrderBy(m => m.attr.Order)
                .Select(m => new KeyValuePair<string, object>(m.attr.Css ?? m.property.Name.ToLower(), m.property.GetValue(instace)));
        }

        /// <summary>
        /// Return <see cref="CssClassAttribute.Css"/> for enum member if specified, otherwise return enum member name.
        /// </summary>
        /// <param name="enum">The instance of enum.</param>
        /// <param name="prefix">A prefix string of return value.</param>
        /// <param name="original">If not specified <see cref="CssClassAttribute"/> for enum member, <c>true</c> to return the member name, otherwise return enum member name with lower case.</param>
        /// <returns>A value represent for css class string.</returns>
        public static string GetCssClass(this Enum @enum, string prefix = default, bool original = default)
        {
            var enumType = @enum.GetType();

            var enumMember = enumType.GetField(@enum.ToString());
            if (enumMember is null)
            {
                return string.Empty;
            }
            if (enumMember.TryGetCustomAttribute<CssClassAttribute>(out var cssClassAttribute))
            {
                return prefix + cssClassAttribute.Css;
            }
            return prefix + (original ? enumMember.Name : enumMember.Name.ToLower());
        }
        /// <summary>
        /// Return the value of <see cref="DefaultValueAttribute.Value"/> for enum member that defined <see cref="DefaultValueAttribute"/>.
        /// </summary>
        /// <param name="enum">The instance of enum.</param>
        /// <returns>A value of <see cref="DefaultValueAttribute.Value"/> for member.</returns>
        public static object GetDefaultValue(this Enum @enum)
        {
            var enumType = @enum.GetType();
            var enumName = @enum.ToString().ToLower();
            var fieldInfo = enumType.GetTypeInfo().GetDeclaredField(@enum.ToString());

            if (fieldInfo == null)
            {
                return enumName;
            }

            var attr = fieldInfo.GetCustomAttribute<DefaultValueAttribute>();
            if (attr == null)
            {
                return enumName;
            }
            return attr.Value;
        }

        /// <summary>
        /// Build css class string and dispose builder collection.
        /// </summary>
        /// <param name="builder">The instance of <see cref="ICssClassBuilder"/>.</param>
        /// <param name="disposing"><c>true</c> to dispose collection of builder, otherwise <c>false</c>.</param>
        /// <returns>A css class string separated by space for each item.</returns>
        public static string Build(this ICssClassBuilder builder, bool disposing)
        {
            var result = builder.ToString();
            if (disposing)
            {
                builder.Dispose();
            }
            return result;
        }

        /// <summary>
        /// Append specified value when condition is <c>true</c>.
        /// </summary>
        /// <param name="builder">The instance of <see cref="ICssClassBuilder"/>.</param>
        /// <param name="value">Value to be appended.</param>
        /// <param name="condition"><c>true</c> to append value.</param>
        /// <returns></returns>
        public static ICssClassBuilder Append(this ICssClassBuilder builder, string value, bool condition)
        {
            if (condition)
            {
                builder.Append(value);
            }
            return builder;
        }

        /// <summary>
        /// Append specified values to <see cref="ICssClassBuilder"/> instance.
        /// </summary>
        /// <param name="builder">The instance of <see cref="ICssClassBuilder"/>.</param>
        /// <param name="values">Values to be appended.</param>
        public static ICssClassBuilder Append(this ICssClassBuilder builder, IEnumerable<string> values)
        {
            foreach (var value in values)
            {
                builder.Append(value);
            }
            return builder;
        }

        /// <summary>
        /// Asynchrosouly import javascript module from specified content path.
        /// </summary>
        /// <param name="js">Instance of <see cref="IJSRuntime"/>.</param>
        /// <param name="contentPath">The path of javascript to import.</param>
        /// <returns>A task that represent the module from javascript.</returns>
        public static async Task<dynamic> Import(this IJSRuntime js, string contentPath)
        {
            var module = await js.InvokeAsync<IJSObjectReference>("import", contentPath);
            return new DynamicJsReferenceObject(module);
        }
    }
}