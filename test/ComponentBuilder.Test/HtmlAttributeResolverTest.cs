﻿using ComponentBuilder.Abstrations;
using ComponentBuilder.Definitions.Parameters;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ComponentBuilder.Test
{
    public class HtmlAttributeResolverTest : TestBase
    {
        public HtmlAttributeResolverTest()
        {
        }


        [Fact]
        public void Given_Invoke_Resolve_When_Component_Input_Title_Then_Title_Has_Value()
        {
            TestContext.RenderComponent<ElementPropertyComponent>(
                p => p.Add(m => m.Title, "abc").Add(m => m.Href, "www.bing.com")
            )
            .Should().HaveAttribute("title", "abc")
                .And.HaveAttribute("href", "www.bing.com");
        }

        [Fact]
        public void When_Enum_Has_HtmlAttribute_Then_Parameter_Is_Enum_To_Use_HtmlAttribute()
        {
            TestContext.RenderComponent<ElementPropertyComponent>(
                p => p.Add(m => m.Title, "abc").Add(m => m.Href, "www.bing.com").Add(m => m.Target, ElementPropertyComponent.LinkTarget.Blank)
            )
            .Should().HaveAttribute("title", "abc")
                .And.HaveAttribute("href", "www.bing.com")
                .And.HaveAttribute("target", "_blank")
                ;
        }

        [Fact]
        public void Given_Invoke_Resolve_When_Component_Drop_Is_Bool_Then_DataToggle_Has_Value_Of_Drop()
        {
            TestContext.RenderComponent<ElementPropertyComponent>(
                p => p.Add(m => m.Drop, true)
            ).Should().HaveAttribute("data-toggle", "drop");
        }



        [Fact]
        public void Given_Invoke_Resolve_When_Component_Drap_Has_Value_Then_Has_Key_DataDrag_With_Value_Yes()
        {
            TestContext.RenderComponent<ElementPropertyComponent>(
                p => p.Add(m => m.Drag, "drag")
            ).Should().HaveAttribute("data-drag", "drag");
        }

        [Fact]
        public void Given_Invoke_Resolve_When_Component_Height_Has_Value_Then_Has_Key_Height_With_Number_Value()
        {
            TestContext.RenderComponent<ElementPropertyComponent>(
                p => p.Add(m => m.Number, 100).Add(m => m.Title, "height")
            ).Should().HaveAttribute("data-height", "100")
            .And.HaveAttribute("title", "height")
            ;
        }

        [Fact]
        public void Given_Invoke_Resolve_When_Component_Auto_Is_Bool_Without_Name_Then_Has_Key_Is_Auto_With_Value_Auto()
        {
            TestContext.RenderComponent<ElementPropertyComponent>(
                p => p.Add(m => m.Auto, true)
            ).Should().HaveAttribute("data-auto", "auto");
        }

        [Fact]
        public void When_Disabled_Is_False_Then_No_Such_Html_Attribute()
        {
            TestContext.RenderComponent<ElementPropertyComponent>()
                .Should().HaveMarkup(b => b.CreateElement(0, "a"));
        }

        [Fact]
        public void When_Click_Then_OnClick_Event_Is_Called()
        {
            var text = "hello";
           var component= TestContext.RenderComponent<ElementPropertyComponent>(m => m.Add(p => p.OnClick, HtmlHelper.Event.Create<MouseEventArgs>(this, () =>
            {
                text = "test";
            })));

            component.Find("a").Click();

            Assert.Equal(text, "test");
        }

        [Fact]
        public void Pre_HtmlAttribute()
        {
            TestContext.RenderComponent<Define>().Should().HaveTag("button");

            TestContext.RenderComponent<Define>(m => m.Add(p => p.Title, "tool"))
                .Should().HaveAttribute("title", "tool");

            var counter = 0;
            var component = TestContext.RenderComponent< Define>(m => m.Add(p => p.OnClick, () =>
            {
                counter++;
            }));

            component.AsElement().Click();

            Assert. Equal(1, counter);


            TestContext.RenderComponent<Define>(m => m.Add(p => p.Active, true)).Should().HaveAttribute("active","active");
        }

        [Fact]
        public void Test_GetMultiple_HtmlAttributeAttribute()
        {
            TestContext.RenderComponent<MultiHtmlAttributeComponent>()
                .Should().HaveAttribute("role", "nav").And.HaveAttribute("aria-label", "multiple");
        }
    }

    [HtmlTag("a")]
    class ElementPropertyComponent : BlazorComponentBase
    {
        [Parameter][HtmlAttribute("title")] public string Title { get; set; }
        [Parameter][HtmlAttribute] public string Href { get; set; }

        [Parameter][HtmlAttribute("data-toggle",Value ="drop")] public bool Drop { get; set; }

        [Parameter][HtmlAttribute] public LinkTarget? Target { get; set; }

        [Parameter][HtmlAttribute("data-drag")] public string Drag { get; set; }

        [Parameter][HtmlAttribute("data-height")] public int? Number { get; set; }

        [Parameter][HtmlAttribute("data-auto", Value ="auto")] public bool Auto { get; set; }

        [Parameter]
        [HtmlAttribute] public bool Disabled { get; set; }

        [Parameter]
        [HtmlAttribute("onclick")] public EventCallback<MouseEventArgs> OnClick { get; set; }

        public enum LinkTarget
        {
            [HtmlAttribute("_blank")] Blank,
            [HtmlAttribute("_self")] Self
        }
    }

    [HtmlTag("button")]
    interface IPreDefine:IBlazorComponent
    {
        [HtmlAttribute]public string? Title { get; set; }

    }
    interface IActive
    {
        [HtmlAttribute("data-active")]public bool Active { get; set; }
    }
    class Define : BlazorComponentBase, IPreDefine,IHasOnClick,IActive
    {
        [Parameter]public string? Title { get; set; }
        [Parameter]public EventCallback<MouseEventArgs?> OnClick { get; set; }
        [Parameter][HtmlAttribute("active")]public bool Active { get; set; }
    }

    [HtmlRole("nav")]
    [HtmlAria("label",Value ="multiple")]
    class MultiHtmlAttributeComponent : BlazorComponentBase
    {

    }
}
