﻿using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ComponentBuilder
{
    /// <summary>
    /// Represents a blazor component.
    /// </summary>
    public interface IBlazorComponent : IComponent
    {
        /// <summary>
        /// Notify component's state has been changed.
        /// </summary>
        /// <returns></returns>
        Task NotifyStateChanged();

        /// <summary>
        /// Build all css class as string.
        /// </summary>
        /// <returns></returns>
        string BuildCssClassString();
    }
}
