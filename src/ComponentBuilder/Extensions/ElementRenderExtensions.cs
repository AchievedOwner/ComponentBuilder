﻿using OneOf;

namespace ComponentBuilder;

/// <summary>
/// 扩展 <see cref="RenderTreeBuilder"/> 创建元素。
/// </summary>
public static class ElementRenderExtensions
{
    /// <summary>
    /// 创建元素名称为 div 的 HTML 元素。
    /// </summary>
    /// <param name="builder">The <see cref="RenderTreeBuilder"/> class to create element.</param>
    /// <param name="sequence">一个整数，表示该指令在源代码中的位置。</param>
    /// <param name="childContent">元素的 UI 片段。</param>
    /// <param name="attributes">元素的 HTML 属性。
    /// 可使用匿名类，<code>new { @class="class1", id="my-id" , onclick = xxx, data_target="xxx" }</code></param>
    /// <param name="condition">当条件时 <c>true</c> 时创建。</param>
    /// <param name="appendFunc">用于追加自定义框架的函数委托。
    /// <para>
    /// <list type="bullet">
    /// <item>第一个参数：<see cref="RenderTreeBuilder"/> 的实例。</item>
    /// <item>第二个参数：当前源代码中最新的位置序列。</item>
    /// <item>返回值：任何操作过后最新的源代码位置序列。</item>
    /// </list>
    /// </para>
    /// </param>
    public static void CreateDiv(this RenderTreeBuilder builder,
                                 int sequence,
                                 OneOf<string?, RenderFragment?, MarkupString?>? childContent = default,
                                 OneOf<IReadOnlyDictionary<string, object>, object>? attributes = default,
                                 bool condition = true,
                                 Func<RenderTreeBuilder, int, int>? appendFunc = default)
        => builder.CreateElement(sequence, "div", childContent, attributes, condition, appendFunc);

    /// <summary>
    /// 创建元素名称为 div 的 HTML 元素。
    /// </summary>
    /// <param name="builder">The <see cref="RenderTreeBuilder"/> class to create element.</param>
    /// <param name="sequence">一个整数，表示该指令在源代码中的位置。</param>
    /// <param name="childContent">元素的 UI 片段。</param>
    /// <param name="attributes">元素的 HTML 属性。
    /// 可使用匿名类，<code>new { @class="class1", id="my-id" , onclick = xxx, data_target="xxx" }</code></param>
    /// <param name="condition">当条件时 <c>true</c> 时创建。</param>
    /// <param name="appendFunc">用于追加自定义框架的函数委托。
    /// <para>
    /// <list type="bullet">
    /// <item>第一个参数：<see cref="RenderTreeBuilder"/> 的实例。</item>
    /// <item>第二个参数：当前源代码中最新的位置序列。</item>
    /// <item>返回值：任何操作过后最新的源代码位置序列。</item>
    /// </list>
    /// </para>
    /// </param>
    public static void CreateDiv(this RenderTreeBuilder builder,
                                 int sequence,
                                 RenderFragment? childContent = default,
                                 OneOf<IReadOnlyDictionary<string, object>, object>? attributes = default,
                                 bool condition = true,
                                 Func<RenderTreeBuilder, int, int>? appendFunc = default)
        => builder.CreateDiv(sequence, OneOf<string?, RenderFragment?, MarkupString?>.FromT1(childContent), attributes, condition, appendFunc);

    /// <summary>
    /// 创建元素名称为 span 的 HTML 元素。
    /// </summary>
    /// <param name="builder">The <see cref="RenderTreeBuilder"/> class to create element.</param>
    /// <param name="sequence">一个整数，表示该指令在源代码中的位置。</param>
    /// <param name="childContent">元素的 UI 片段。</param>
    /// <param name="attributes">元素的 HTML 属性。
    /// 可使用匿名类，<code>new { @class="class1", id="my-id" , onclick = xxx, data_target="xxx" }</code></param>
    /// <param name="condition">当条件时 <c>true</c> 时创建。</param>
    /// <param name="appendFunc">用于追加自定义框架的函数委托。
    /// <para>
    /// <list type="bullet">
    /// <item>第一个参数：<see cref="RenderTreeBuilder"/> 的实例。</item>
    /// <item>第二个参数：当前源代码中最新的位置序列。</item>
    /// <item>返回值：任何操作过后最新的源代码位置序列。</item>
    /// </list>
    /// </para>
    /// </param>
    public static void CreateSpan(this RenderTreeBuilder builder,
                                  int sequence,
                                  OneOf<string?, RenderFragment?, MarkupString?>? childContent = default,
                                  OneOf<IReadOnlyDictionary<string, object>, object>? attributes = default,
                                  bool condition = true,
                                  Func<RenderTreeBuilder, int, int>? appendFunc = default)
        => builder.CreateElement(sequence, "span", childContent, attributes, condition, appendFunc);

    /// <summary>
    /// 创建元素名称为 span 的 HTML 元素。
    /// </summary>
    /// <param name="builder">The <see cref="RenderTreeBuilder"/> class to create element.</param>
    /// <param name="sequence">一个整数，表示该指令在源代码中的位置。</param>
    /// <param name="childContent">元素的 UI 片段。</param>
    /// <param name="attributes">元素的 HTML 属性。
    /// 可使用匿名类，<code>new { @class="class1", id="my-id" , onclick = xxx, data_target="xxx" }</code></param>
    /// <param name="condition">当条件时 <c>true</c> 时创建。</param>
    /// <param name="appendFunc">用于追加自定义框架的函数委托。
    /// <para>
    /// <list type="bullet">
    /// <item>第一个参数：<see cref="RenderTreeBuilder"/> 的实例。</item>
    /// <item>第二个参数：当前源代码中最新的位置序列。</item>
    /// <item>返回值：任何操作过后最新的源代码位置序列。</item>
    /// </list>
    /// </para>
    /// </param>
    public static void CreateSpan(this RenderTreeBuilder builder,
                             int sequence,
                             RenderFragment? childContent = default,
                             OneOf<IReadOnlyDictionary<string, object>, object>? attributes = default,
                             bool condition = true,
                             Func<RenderTreeBuilder, int, int>? appendFunc = default)
    => builder.CreateSpan(sequence, OneOf<string?, RenderFragment?, MarkupString?>.FromT1(childContent), attributes, condition, appendFunc);

    /// <summary>
    /// 创建元素名称为 p 的 HTML 元素。
    /// </summary>
    /// <param name="builder">The <see cref="RenderTreeBuilder"/> class to create element.</param>
    /// <param name="sequence">一个整数，表示该指令在源代码中的位置。</param>
    /// <param name="childContent">元素的 UI 片段。</param>
    /// <param name="attributes">元素的 HTML 属性。
    /// 可使用匿名类，<code>new { @class="class1", id="my-id" , onclick = xxx, data_target="xxx" }</code></param>
    /// <param name="condition">当条件时 <c>true</c> 时创建。</param>
    /// <param name="appendFunc">用于追加自定义框架的函数委托。
    /// <para>
    /// <list type="bullet">
    /// <item>第一个参数：<see cref="RenderTreeBuilder"/> 的实例。</item>
    /// <item>第二个参数：当前源代码中最新的位置序列。</item>
    /// <item>返回值：任何操作过后最新的源代码位置序列。</item>
    /// </list>
    /// </para>
    /// </param>
    public static void CreateParagraph(this RenderTreeBuilder builder,
                                       int sequence,
                                       OneOf<string?, RenderFragment?, MarkupString?>? childContent = default,
                                       OneOf<IReadOnlyDictionary<string, object>, object>? attributes = default,
                                       bool condition = true,
                                       Func<RenderTreeBuilder, int, int>? appendFunc = default)
        => builder.CreateElement(sequence, "p", childContent, attributes, condition, appendFunc);

    /// <summary>
    /// 创建元素名称为 br 的 HTML 元素。
    /// </summary>
    /// <param name="builder">The <see cref="RenderTreeBuilder"/> class to create element.</param>
    /// <param name="sequence">一个整数，表示该指令在源代码中的位置。</param>
    /// <param name="attributes">元素的 HTML 属性。
    /// 可使用匿名类，<code>new { @class="class1", id="my-id" , onclick = xxx, data_target="xxx" }</code></param>
    /// <param name="condition">当条件时 <c>true</c> 时创建。</param>
    public static void CreateBr(this RenderTreeBuilder builder,
                                int sequence,
                                OneOf<IReadOnlyDictionary<string, object>, object>? attributes = default,
                                bool condition = true)
        => builder.CreateElement(sequence, "br", attributes: attributes, condition: condition);


    /// <summary>
    /// 创建元素名称为 hr 的 HTML 元素。
    /// </summary>
    /// <param name="builder">The <see cref="RenderTreeBuilder"/> class to create element.</param>
    /// <param name="sequence">一个整数，表示该指令在源代码中的位置。</param>
    /// <param name="attributes">元素的 HTML 属性。
    /// 可使用匿名类，<code>new { @class="class1", id="my-id" , onclick = xxx, data_target="xxx" }</code></param>
    /// <param name="condition">当条件时 <c>true</c> 时创建。</param>
    public static void CreateHr(this RenderTreeBuilder builder,
                                int sequence,
                                OneOf<IReadOnlyDictionary<string, object>, object>? attributes = default,
                                bool condition = true)
        => builder.CreateElement(sequence, "hr", attributes: attributes, condition: condition);
}
