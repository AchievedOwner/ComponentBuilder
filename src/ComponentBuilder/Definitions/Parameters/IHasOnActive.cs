﻿namespace ComponentBuilder.Automation.Definitions;
/// <summary>
/// Provides parameters for which the component has callback events that can be activated.
/// </summary>
public interface IHasOnActive : IHasActive, IHasEventCallback
{
    /// <summary>
    /// A callback function to be executed when the component state is activated.
    /// </summary>
    EventCallback<bool> OnActive { get; set; }
}
