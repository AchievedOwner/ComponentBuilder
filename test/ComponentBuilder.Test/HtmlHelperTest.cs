﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ComponentBuilder.Test;
public class HtmlHelperTest : TestBase
{
    public HtmlHelperTest()
    {

    }

    [Fact]
    public void Test_MergeAttribute()
    {
        HtmlHelper.MergeHtmlAttributes(new Dictionary<string, object>()
        {
            ["max"] = 10,
            ["min"] = 5
        }).Should().ContainKey("max").And.ContainValue(10)
            .And.ContainKey("min").And.ContainValue(5)
            ;


        HtmlHelper.MergeHtmlAttributes(new { @class = "class", data_toggle = "toggle" })
            .Should().ContainKey("class").And.ContainValue("class")
            .And.ContainKey("data-toggle").And.ContainValue("toggle");
    }

    //[Fact]
    //public void Test_CreateCssBuilder()
    //{
    //    HtmlHelper.CreateClass().Append("active").Append("show")
    //        .ToString().Should().Be("active show");
    //}

    //[Fact]
    //public void Test_CreateStyleBuilder()
    //{
    //    HtmlHelper.Style.Append("display:block").ToString().Should().Be("display:block");
    //}

    [Fact]
    public void Test_CreateContent()
    {
        TestContext.RenderComponent<HtmlHelperComponent>(p => p.AddChildContent(HtmlHelper.CreateContent("abc"))).MarkupMatches("<div>abc</div>");
    }
}

class HtmlHelperComponent : ComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.CreateElement(0, "div", ChildContent);
    }
}
