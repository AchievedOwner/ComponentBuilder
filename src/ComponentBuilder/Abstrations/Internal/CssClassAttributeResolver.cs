﻿using System.Linq;
using System.Reflection;

namespace ComponentBuilder.Abstrations.Internal;

/// <summary>
/// To resolve css class attribute for component.
/// </summary>
public class CssClassAttributeResolver : ICssClassAttributeResolver
{
    private readonly ICssClassBuilder _cssClassBuilder;

    /// <summary>
    /// Initializes a new instance of <see cref="CssClassAttributeResolver"/> class.
    /// </summary>
    /// <param name="cssClassBuilder"></param>
    public CssClassAttributeResolver(ICssClassBuilder cssClassBuilder)
    {
        this._cssClassBuilder = cssClassBuilder;
    }

    /// <summary>
    /// Resolve specified component that defined <see cref="CssClassAttribute"/> attribute.
    /// </summary>
    /// <param name="component">The component to be resolved.</param>
    /// <returns>Resolved css class string seperated by space for each item.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="component"/> is null.</exception>
    public virtual string Resolve(ComponentBase component)
    {
        if (component is null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        var componentType = component.GetType();

        //support interface CssClassAttribute

        var componentInterfaceTypes = componentType.GetInterfaces();

        //interface is defined CssClassAttribute
        var interfaceCssClassAttributes = componentInterfaceTypes.Where(m => m.IsDefined(typeof(CssClassAttribute))).Select(m => m.GetCustomAttribute<CssClassAttribute>()).OrderBy(m => m.Order);

        // for component that defined CssClassAttribute, could concat value with interface has pre-definition of CssClassAttribute

        // Question:
        // How to disable to concat with interface pre-definition of CssClassAttribute?

        foreach (var item in interfaceCssClassAttributes)
        {
            if (!CanApplyCss(item, component))
            {
                continue;
            }
            _cssClassBuilder.Append(item.Name);
        }

        if (componentType.TryGetCustomAttribute<CssClassAttribute>(out var classCssAttribute) && CanApplyCss(classCssAttribute, component))
        {
            _cssClassBuilder.Append(classCssAttribute.Name);
        }


        //interface properties is defined CssClassAttribute
        var interfacePropertisWithCssClassAttributes = componentInterfaceTypes
            .SelectMany(m => m.GetProperties())
            .Where(m => m.IsDefined(typeof(CssClassAttribute)));

        //class properties is defined CssClassAttribute
        var classPropertiesWithCssAttributes = componentType.GetProperties()
            .Where(m => m.IsDefined(typeof(CssClassAttribute)));

        //override same key & value from class property
        var mergeCssClassAttributes = CompareToTake(interfacePropertisWithCssClassAttributes, classPropertiesWithCssAttributes);

        var cssClassValuePaires = GetCssClassAttributesInOrderFromParameters(mergeCssClassAttributes, component);

        foreach (var parameters in cssClassValuePaires)
        {
            var name = parameters.name;
            var value = parameters.value;
            var attr = parameters.attr;
            var suffix = attr.Suffix;

            var css = string.Empty;

            if (value is null)
            {
                continue;
            }

            switch (value)
            {
                case bool:
                    if (attr is BooleanCssClassAttribute boolAttr)
                    {
                        css = (bool)value ? boolAttr.TrueCssClass : boolAttr.FalseCssClass;
                    }
                    else if ((bool)value)
                    {
                        css = name;
                    }
                    break;
                case Enum://css + enum css
                    value = ((Enum)value).GetCssClass();
                    goto default;
                default:// css + value
                    css = suffix ? $"{value}{name}" : $"{name}{value}";
                    break;
            }
            _cssClassBuilder.Append(css);
        }

        return _cssClassBuilder.Build(true);


        static IEnumerable<PropertyInfo> CompareToTake(IEnumerable<PropertyInfo> interfaces, IEnumerable<PropertyInfo> classes)
        {
            var list = interfaces.ToList();

            foreach (var item in classes)
            {
                var index = list.FindIndex(m => m.Name == item.Name);
                if (index >= 0)
                {
                    list[index] = item;
                }
                else
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Gets CSS class value from parameters which has defined <see cref="CssClassAttribute"/> attribute.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="instace">Object to get value from property</param>
        /// <returns>A key/value pairs contains CSS class and value.</returns>
        static IEnumerable<(string name, object value, CssClassAttribute attr)> GetCssClassAttributesInOrderFromParameters(IEnumerable<PropertyInfo> properties, object instance)
        {
            return properties.Where(m => m.IsDefined(typeof(CssClassAttribute)))
                .Select(m => new { property = m, attr = m.GetCustomAttribute<CssClassAttribute>() })
                .Where(m => CanApplyCss(m.attr, m.property.GetValue(instance)))
                .OrderBy(m => m.attr.Order)
                .Select(m => (name: m.attr.Name, value: m.property.GetValue(instance), m.attr))
                ;
        }

        static bool CanApplyCss(CssClassAttribute attribute, object value) => !attribute.Disabled;
    }
}
