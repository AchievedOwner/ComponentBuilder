namespace ComponentBuilder;
/// <summary>
/// Represents a base child component that associated with <see cref="BlazorParentComponentBase{TParentComponent}"/> class.
/// </summary>
/// <typeparam name="TParentComponent">The parent component type.</typeparam>
[Obsolete("Use BlazorComponentBase instead, this will be removed in next version")]
public abstract class BlazorChildComponentBase<TParentComponent> : BlazorComponentBase
    where TParentComponent : ComponentBase
{
    /// <summary>
    /// Gets cascading parameter instance of parent component.
    /// </summary>
    [CascadingParameter] protected TParentComponent ParentComponent { get; private set; }


    /// <summary>
    /// Overried to validate and throw exception when <see cref="ParentComponent"/> is <c>null</c> value.
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        ThrowIfParentComponentNull();
        await base.OnInitializedAsync();
    }

    /// <summary>
    /// Throws an exception when <see cref="ParentComponent"/> is <c>null</c> value.
    /// </summary>
    /// <exception cref="InvalidOperationException">This component must be the child of <see cref="ParentComponent"/> component.</exception>
    protected virtual void ThrowIfParentComponentNull()
    {
        if (ParentComponent is null)
        {
            throw new InvalidOperationException($"The '{GetType().Name}' component must be the child of '{typeof(TParentComponent).Name}' component");
        }
    }
}
/// <summary>
/// Represents a base child component that associated with <see cref="BlazorParentComponentBase{TParentComponent, TChildComponent}"/> class.
/// </summary>
/// <typeparam name="TParentComponent">The parent component type.</typeparam>
/// <typeparam name="TChildComponent">The child component type.</typeparam>
[Obsolete("Use BlazorComponentBase instead, this will be removed in next version")]
public abstract class BlazorChildComponentBase<TParentComponent, TChildComponent> : BlazorChildComponentBase<TParentComponent>
    where TParentComponent : BlazorParentComponentBase<TParentComponent, TChildComponent>
    where TChildComponent : ComponentBase
{
    private int _componentIndex = -1;
    /// <summary>
    /// Gets current child component index in parent component.
    /// </summary>
    protected int Index => _componentIndex;
    /// <summary>
    /// Overried to validate and throw exception when parent component is <c>null</c> value.
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _componentIndex = await ParentComponent.AddChildComponent(this);
    }

    /// <summary>
    /// Throws an exception when parent component is <c>null</c> value.
    /// </summary>
    /// <exception cref="InvalidOperationException">This component must be the child of parent component.</exception>
    protected override void ThrowIfParentComponentNull()
    {
        if (ParentComponent is null)
        {
            throw new InvalidOperationException($"The '{typeof(TChildComponent).Name}' component must be the child of '{typeof(TParentComponent).Name}' component");
        }
    }

    /// <summary>
    /// Build a onclick event of attribute to active child component with specified index.
    /// </summary>
    /// <param name="attributes"></param>
    protected override void BuildAttributes(IDictionary<string, object> attributes)
    {
        attributes["onclick"] = EventCallback.Factory.Create(this, async () => await ParentComponent.Activate(_componentIndex));
    }
}
