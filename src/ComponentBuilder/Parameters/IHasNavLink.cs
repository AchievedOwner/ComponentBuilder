﻿using Microsoft.AspNetCore.Components.Routing;

namespace ComponentBuilder.Parameters;

/// <summary>
/// Provides a component has navigation link.
/// </summary>
public interface IHasNavLink : IBlazorComponent
{
    /// <summary>
    /// Gets the navigation manage.
    /// </summary>
    NavigationManager NavigationManager { get; }

    /// <summary>
    /// Gets or sets the behavior of nav link can be matched.
    /// </summary>
    NavLinkMatch Match { get; set; }

    /// <summary>
    /// Gets a boolean value indicates the link is matched with url.
    /// </summary>
    bool IsActive { get; set; }
}
