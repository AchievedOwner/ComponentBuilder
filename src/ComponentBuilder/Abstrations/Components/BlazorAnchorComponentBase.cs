using System.Diagnostics;

using Microsoft.AspNetCore.Components.Routing;

namespace ComponentBuilder;

/// <summary>
/// 表示具备和 <see cref="NavLink"/> 组件一样的超链接组件功能的基类。
/// </summary>
public abstract class BlazorAnchorComponentBase : BlazorComponentBase, IHasChildContent, IDisposable
{
    private string? _hrefAbsolute;
    private bool _isActive;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Inject] protected NavigationManager NavigationManger { get; set; } = default!;
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 路由匹配方式。
    /// </summary>
    [Parameter] public NavLinkMatch Match { get; set; } = NavLinkMatch.All;

    /// <summary>
    /// 获取一个布尔值，表示当前 url 是否与超链接路由匹配。你可以通过此值，设置当 url 匹配时的样式或 CSS 的值。
    /// </summary>
    protected bool IsActive => _isActive;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string TagName => "a";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        // We'll consider re-rendering on each location change
        base.OnInitialized();
        NavigationManger.LocationChanged += OnLocationChanged;
    }
    /// <summary>
    /// 重写方法并判断包含的 <c>href</c> 属性是否符合 <see cref="Match"/> 属性的配置，并指示 <see cref="IsActive"/> 属性当前路由是否匹配成功。
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // Update computed state
        var href = (string?)null;
        if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("href", out var obj))
        {
            href = Convert.ToString(obj);
        }

        _hrefAbsolute = href == null ? null : NavigationManger.ToAbsoluteUri(href).AbsoluteUri;
        _isActive = ShouldMatch(NavigationManger.Uri);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        // To avoid leaking memory, it's important to detach any event handlers in Dispose()
        NavigationManger.LocationChanged -= OnLocationChanged;
    }
    /// <summary>
    /// 当路由导航变更时触发。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    private void OnLocationChanged(object? sender, LocationChangedEventArgs args)
    {
        // We could just re-render always, but for this component we know the
        // only relevant state change is to the _isActive property.
        var shouldBeActiveNow = ShouldMatch(args.Location);
        if (shouldBeActiveNow != _isActive)
        {
            _isActive = shouldBeActiveNow;
            StateHasChanged();
        }
    }

    private bool ShouldMatch(string currentUriAbsolute)
    {
        if (_hrefAbsolute == null)
        {
            return false;
        }

        if (EqualsHrefExactlyOrIfTrailingSlashAdded(currentUriAbsolute))
        {
            return true;
        }

        if (Match == NavLinkMatch.Prefix
            && IsStrictlyPrefixWithSeparator(currentUriAbsolute, _hrefAbsolute))
        {
            return true;
        }

        return false;
    }

    private bool EqualsHrefExactlyOrIfTrailingSlashAdded(string currentUriAbsolute)
    {
        Debug.Assert(_hrefAbsolute != null);

        if (string.Equals(currentUriAbsolute, _hrefAbsolute, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (currentUriAbsolute.Length == _hrefAbsolute.Length - 1)
        {
            // Special case: highlight links to http://host/path/ even if you're
            // at http://host/path (with no trailing slash)
            //
            // This is because the router accepts an absolute URI value of "same
            // as base URI but without trailing slash" as equivalent to "base URI",
            // which in turn is because it's common for servers to return the same page
            // for http://host/vdir as they do for host://host/vdir/ as it's no
            // good to display a blank page in that case.
            if (_hrefAbsolute[_hrefAbsolute.Length - 1] == '/'
                && _hrefAbsolute.StartsWith(currentUriAbsolute, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsStrictlyPrefixWithSeparator(string value, string prefix)
    {
        var prefixLength = prefix.Length;
        if (value.Length > prefixLength)
        {
            return value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                && (
                    // Only match when there's a separator character either at the end of the
                    // prefix or right after it.
                    // Example: "/abc" is treated as a prefix of "/abc/def" but not "/abcdef"
                    // Example: "/abc/" is treated as a prefix of "/abc/def" but not "/abcdef"
                    prefixLength == 0
                    || !char.IsLetterOrDigit(prefix[prefixLength - 1])
                    || !char.IsLetterOrDigit(value[prefixLength])
                );
        }
        else
        {
            return false;
        }
    }
}
