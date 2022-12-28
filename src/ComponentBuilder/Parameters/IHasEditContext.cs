﻿namespace ComponentBuilder.Parameters;

/// <summary>
/// Provides a component has edit context.
/// </summary>
public interface IHasEditContext
{
    /// <summary>
    /// Gets or sets the editing context.
    /// </summary>
    EditContext? EditContext { get; set; }
}
