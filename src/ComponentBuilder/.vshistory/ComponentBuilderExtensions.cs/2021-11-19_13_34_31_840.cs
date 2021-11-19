﻿using System;
using System.Reflection;

namespace ComponentBuilder
{
    /// <summary>
    /// The extensions of blazor component.
    /// </summary>
    public static class BlazorComponentExtensions
    {
        public static bool TryGetAttribute<TAttribute>(this Type type, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = type.GetCustomAttribute<TAttribute>();
            return attribute != null;
        }
        public static bool TryGetAttribute<TAttribute>(this FieldInfo field, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = field.GetCustomAttribute<TAttribute>();
            return attribute != null;
        }

        public static string GetEnumCssClass(this object @enum, bool original = default)
        {
            if (@enum is not Enum)
            {
                throw new InvalidOperationException($"This type is not a Enum type");
            }

            var enumType = @enum.GetType();

            var enumMember = enumType.GetField(@enum.ToString());

            if (enumMember.TryGetAttribute<CssClassAttribute>(out var cssClassAttribute))
            {
                return cssClassAttribute.Css;
            }
            return original ? enumMember.Name : enumMember.Name.ToLower();
        }
    }
}