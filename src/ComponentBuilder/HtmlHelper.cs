using System.Globalization;
using System.Text;

using ComponentBuilder.Abstrations.Internal;

namespace ComponentBuilder;

/// <summary>
/// HTML 的工具。
/// </summary>
public static class HtmlHelper
{
    /// <summary>
    /// 合并指定的 HTML 属性，如果出现重复的键，将使用最后一次的值。
    /// </summary>
    /// <param name="htmlAttributes">要合并的 HTML 属性。使用匿名类，<c>new { @class="class1", id="my-id" , onclick = xxx }</c></param>
    /// <returns>包含 HTML 属性的键值对集合。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="htmlAttributes"/> 是 <c>null</c>.</exception>
    public static IEnumerable<KeyValuePair<string, object>> MergeHtmlAttributes(object htmlAttributes)
    {
        if (htmlAttributes is null)
        {
            throw new ArgumentNullException(nameof(htmlAttributes));
        }

        if (htmlAttributes is IEnumerable<KeyValuePair<string, object>> keyValueAttributes)
        {
            return keyValueAttributes;
        }

        return htmlAttributes.GetType().GetProperties()
            .Select(property =>
            {
                var name = property.Name.Replace("_", "-");
                var value = property.GetValue(htmlAttributes);
                return new KeyValuePair<string, object>(name, value);
            }).Distinct();
    }

    /// <summary>
    /// 创建并返回满足条件的 CSS 类的字符串。
    /// </summary>
    /// <param name="condition">要满足的条件。</param>
    /// <param name="trueCssClass">当 <paramref name="condition"/> 是 <c>true</c> 时返回的 CSS 类名称。</param>
    /// <param name="falseCssClass">当 <paramref name="condition"/> 是 <c>false</c> 时返回的 CSS 类名称。</param>
    /// <param name="prepend">一个固定的字符串。当这个值不是 null 或空时，会在第一个 CSS 类的前面添加这个参数的字符串，并使用空格与后面的 CSS 类名称进行分割。</param>
    /// <returns>A CSS class string.</returns>
    public static string CreateCssClass(bool condition, string trueCssClass, string? falseCssClass = default, string? prepend = default)
    {
        var builder = new StringBuilder();
        if (!string.IsNullOrEmpty(prepend))
        {
            builder.Append(prepend).Append(' ');
        }
        return builder.Append(condition ? trueCssClass : falseCssClass).ToString();
    }

    /// <summary>
    /// 创建临时的 CSS 构造器。
    /// </summary>
    public static ICssClassBuilder CreateCssBuilder() => new DefaultCssClassBuilder();

    /// <summary>
    /// 创建临时的样式构造器。
    /// </summary>
    public static IStyleBuilder CreateStyleBuilder() => new DefaultStyleBuilder();

    /// <summary>
    /// 创建一个组件的 <see cref="EventCallback"/> 事件。
    /// </summary>
    /// <typeparam name="TValue">回调操作的参数类型。</typeparam>
    /// <param name="receiver">事件回调触发器的对象。</param>
    /// <param name="callback">要执行的回调操作。</param>
    /// <param name="condition">一个满足创建事件回调的条件。</param>
    /// <returns>绑定事件处理程序委托。</returns>
    public static EventCallback<TValue> CreateCallback<TValue>(object receiver, Action<TValue> callback, bool condition = true)
    {
        if (condition)
        {
            return EventCallback.Factory.Create(receiver, callback);
        }

        return default;
    }

    /// <summary>
    /// 创建一个组件的 <see cref="EventCallback"/> 事件。
    /// </summary>
    /// <param name="receiver">事件回调触发器的对象。</param>
    /// <param name="callback">要执行的回调操作。</param>
    /// <param name="condition">一个满足创建事件回调的条件。</param>
    /// <returns>绑定事件处理程序委托。</returns>
    public static EventCallback CreateCallback(object receiver, Action callback, bool condition = true)
    {
        if (condition)
        {
            return EventCallback.Factory.Create(receiver, callback);
        }

        return default;
    }

    /// <summary>
    /// 创建一个组件的 <see cref="EventCallback{TValue}"/> 事件。
    /// </summary>
    /// <typeparam name="TValue">回调操作的参数类型。</typeparam>
    /// <param name="receiver">事件回调触发器的对象。</param>
    /// <param name="callback">要执行的回调操作。</param>
    /// <param name="condition">一个满足创建事件回调的条件。</param>
    /// <returns>绑定事件处理程序委托。</returns>
    public static EventCallback<TValue> CreateCallback<TValue>(object receiver, Func<TValue, Task> callback, bool condition = true)
    {
        if (condition)
        {
            return EventCallback.Factory.Create(receiver, callback);
        }

        return default;
    }

    /// <summary>
    /// 创建一个组件的 <see cref="EventCallback"/> 事件。
    /// </summary>
    /// <param name="receiver">事件回调触发器的对象。</param>
    /// <param name="callback">要执行的回调操作。</param>
    /// <param name="condition">一个满足创建事件回调的条件。</param>
    /// <returns>绑定事件处理程序委托。</returns>
    public static EventCallback CreateCallback(object receiver, Func<Task> callback, bool condition = true)
    {
        if (condition)
        {
            return EventCallback.Factory.Create(receiver, callback);
        }

        return default;
    }

    /// <summary>
    /// 创建具备双向绑定操作的事件回调。
    /// </summary>
    /// <typeparam name="TValue">值的类型。</typeparam>
    /// <param name="receiver">事件回调触发器的对象。</param>
    /// <param name="setter">将当前值替换为参数中的新值的操作。</param>
    /// <param name="existingValue">已存在的值。</param>
    /// <param name="culture">值的本地化格式。</param>
    /// <returns>绑定事件处理程序委托。</returns>
    public static EventCallback<ChangeEventArgs> CreateCallbackBinder<TValue>(object receiver, Action<TValue?> setter, TValue? existingValue, CultureInfo? culture = default) 
        => EventCallback.Factory.CreateBinder(receiver, setter, existingValue, culture);
}
