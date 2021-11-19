﻿using System;
using System.Linq;
using System.Reflection;

namespace ComponentBuilder.Abstrations
{
    internal class CssClassAttributeResolver : ICssClassResolver
    {
        private readonly ICssClassBuilder _cssClassBuilder;

        public CssClassAttributeResolver(ICssClassBuilder cssClassBuilder)
        {
            this._cssClassBuilder = cssClassBuilder;
        }

        public string Resolve(Type componentType)
        {
            if (componentType.TryGetAttribute<CssClassAttribute>(out var classCssAttribute))
            {
                _cssClassBuilder.Append(classCssAttribute.Css);
            }

            foreach (var parameters in componentType.GetProperties().Where(m => m.IsDefined(typeof(CssClassAttribute))))
            {
                var name = parameters.Name;
                var value = parameters.GetValue(componentType);

                var css = parameters.GetCustomAttribute<CssClassAttribute>().Css;



                if (value is null)
                {
                    continue;
                }

                switch (value)
                {
                    case Boolean:
                        if ((bool)value)
                        {
                            _cssClassBuilder.Append($"{css}");
                        }
                        break;
                    case Enum://css + enum css
                        _cssClassBuilder.Append($"{css}{((Enum)value).GetCssClass()}");
                        break;
                    default:// css + value
                        _cssClassBuilder.Append($"{css}{value}");
                        break;
                }
            }

            return _cssClassBuilder.Build();
        }
    }
}
