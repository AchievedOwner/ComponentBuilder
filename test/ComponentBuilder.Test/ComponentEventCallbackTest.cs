using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ComponentBuilder.Abstrations;

using ComponentBuilder.Parameters;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ComponentBuilder.Test
{
    public class ComponentEventCallbackTest : TestBase
    {
        [Fact]
        public void Test_EventResolver()
        {
           GetService<IHtmlEventAttributeResolver>().Resolve(new ComponentEventCallback())
                .Should().ContainKey("onclick");
            
        }

        [Fact]
        public void Test_ReplaceEventNameByClass()
        {
            GetService<IHtmlEventAttributeResolver>().Resolve(new ComponentEventCallback())
                 .Should().ContainKey("ontest");

        }

        [Fact]
        public void Invoke_Onclick_Event_When_OnClick_HasCallback_Then_OnClick_Invoke()
        {
            var clicked = false;
            base.TestContext.RenderComponent<ComponentEventCallback>(m => m.Add(p => p.OnClick, () => clicked=true))
                .Find("div").Click()
                ;
            Assert.True(clicked);
        }
    }

    class ComponentEventCallback : BlazorComponentBase, IHasOnClick,IHasTest
    {
        [Parameter]public EventCallback<MouseEventArgs> OnClick { get; set; }
        [HtmlEvent("ondbclick")]public EventCallback OnTest { get; set; }
    }

    interface IHasTest
    {
        [HtmlEvent("ontest")]EventCallback OnTest { get; set; }
    }
}
