﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ComponentBuilder.Parameters;
using Microsoft.AspNetCore.Components;

namespace ComponentBuilder.Test
{
    public class DynamicComponentEventTest : TestBase
    {
        [Fact]
        public async void Given_Component_Has_Active_After_Invoke_Active_Method_When_Create_Component_Active_Manually_Then_Active_Is_True()
        {
            var component = TestContext.RenderComponent<ActiveComponent>();

            await component.Instance.Activate();

            component.MarkupMatches("<div class=\"active\"></div>");
        }

        class ActiveComponent : BlazorAbstractComponentBase, IHasOnActive
        {
            [Parameter] public EventCallback<bool> OnActive { get; set; }
            [Parameter][CssClass("active")] public bool Active { get; set; }
        }
    }
}
