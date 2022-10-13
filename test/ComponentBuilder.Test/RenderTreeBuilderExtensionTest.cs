﻿using ComponentBuilder.Parameters;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ComponentBuilder.Test
{
    public class RenderTreeBuilderExtensionTest : TestBase
    {
        [Fact]
        public void Test_CreateElement()
        {
            TestContext.Render(builder => builder.CreateElement(0, "div", "abc"))
                .MarkupMatches("<div>abc</div>");

            TestContext.Render(builder => builder.CreateDiv(1, childContent =>
            {
                childContent.CreateElement(0, "span", "test");
            })).MarkupMatches("<div><span>test</span></div");

            TestContext.Render(b => b.CreateParagraph(0, "aaa")).MarkupMatches("<p>aaa</p>");

            TestContext.Render(b => b.CreateHr(0)).MarkupMatches("<hr/>");

            TestContext.Render(b => b.CreateBr(0)).MarkupMatches("<br/>");
        }

        [Fact]
        public void Test_CreateElement_With_Anonymouse_Class()
        {
            TestContext.Render(builder => builder.CreateElement(0, "div", "abc", new { @class = "myclass" }))
                .MarkupMatches("<div class=\"myclass\">abc</div>");

            TestContext.Render(builder => builder.CreateDiv(0, "abc", new { @class = "myclass" }))
                .MarkupMatches("<div class=\"myclass\">abc</div>");

            TestContext.Render(builder => builder.CreateSpan(0, "abc", new { @class = "myclass" }))
                .MarkupMatches("<span class=\"myclass\">abc</span>");
        }

        [Fact]
        public void Test_CreateElement_With_Dictionary_Class()
        {
            TestContext.Render(builder =>
            builder.CreateElement(0, "div", "abc", new Dictionary<string, object>() { { "class", "myclass" } }))
                .MarkupMatches("<div class=\"myclass\">abc</div>");


            TestContext.Render(builder =>
            builder.CreateElement(0, "div", "abc", HtmlHelper.CreateHtmlAttributes(dic => dic["class"] = "myclass")))
                .MarkupMatches("<div class=\"myclass\">abc</div>");
        }

        [Fact]
        public void Test_CreateComponent()
        {
            TestContext.Render(builder => builder.CreateComponent<CreateComponent>(0))
                .MarkupMatches("<div></div>");
        }

        [Fact]
        public void Test_CreateComponent_With_Parameter()
        {
            TestContext.Render(builder =>
            builder.CreateComponent<CreateComponent>(0, attributes: new { Disabled = true }))
                .MarkupMatches("<div disabled=\"disabled\"></div>");
        }

        [Fact]
        public void Test_CreateComponent_With_Parameter_And_Class()
        {
            TestContext.Render(builder =>
            builder.CreateComponent<CreateComponent>(0, attributes: new { Disabled = true, @class = "my-class" }))
                .MarkupMatches("<div disabled=\"disabled\" class=\"my-class\"></div>");
        }

        [Fact]
        public void Test_CreateComponent_With_ChildContent()
        {
            TestContext.Render(builder =>
            builder.CreateComponent<CreateComponent>(0, "test"))
                .MarkupMatches("<div>test</div>");

            TestContext.Render(builder =>
            builder.CreateComponent<CreateComponent>(0, content => content.CreateElement(0, "span", "test")))
                .MarkupMatches("<div><span>test</span></div>");
        }
    }

    class CreateComponent : BlazorComponentBase, IHasChildContent
    {
        [Parameter][HtmlAttribute("disabled")] public bool Disabled { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
