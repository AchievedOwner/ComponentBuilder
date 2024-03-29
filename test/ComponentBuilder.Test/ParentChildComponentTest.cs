﻿using ComponentBuilder.Abstrations;
using ComponentBuilder.Definitions;
using Microsoft.AspNetCore.Components;

namespace ComponentBuilder.Test;
public class ParentChildComponentTest : AutoTestBase
{
    public ParentChildComponentTest()
    {

    }

    [Fact]
    public void When_Child_Component_Not_In_Parent_Component_Then_Throw_InvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() => GetComponent<ChildComponent>());

    }

    [Fact]
    public void When_Child_Component_In_Parent_Component_Then_Render()
    {
        GetComponent<MyParentComponent>(builder =>
        {
            builder.AddChildContent(component => component.CreateComponent<ChildComponent>(1));
        })
            .MarkupMatches("<div><div></div></div>");
    }

    [Fact(Skip ="Skip to active")]
    public void Given_Parent_Active_Child_Component_When_Parent_Index_Is_One_Then_Child_Component_At_First_Index_Is_Actived()
    {
        var tab = GetComponent<TabComponent>(builder =>
         {
             builder.AddChildContent(tab =>
             {
                 tab.CreateComponent<TabItemComponent>(1);
                 tab.CreateComponent<TabItemComponent>(2);
             });
         })
            ;

        tab.MarkupMatches(@"
<tab>
    <tabitem class=""active""></tabitem>
    <tabitem></tabitem>
</tab>
");

    }


    [Fact]
    public void When_Has_ParentCompnentAttribute_Then_Has_This_Value_For_Cascading_Parameter()
    {
        var childComponents = GetComponent<MyParentComponent>(p => p.AddChildContent(c =>
          {
              c.CreateComponent<MyChildComponent>(0);
              c.CreateComponent<MyChildComponent>(1);
          })).Instance.ChildComponents;

        childComponents.Should().HaveCount(2);

        childComponents.Should().AllBeOfType<MyChildComponent>();
    }

    [Fact]
    public void Give_NestedComponent_IsRequired_When_Not_Create_Component_Under_Specific_Component_Then_Throw_Exception()
    {
        Assert.Throws<InvalidOperationException>(() => GetComponent<ReuiredChildComponent>());

        //see the exception text
        //GetComponent<ReuiredChildComponent>();
    }

    [Fact]
    public void When_Has_Mutiple_NestedComponent()
    {
        var parentComponents1 = GetComponent<MyParentComponent>(p => p.AddChildContent(c =>
         {
             c.CreateComponent<MyNestedChildComponent>(0);
             c.CreateComponent<MyNestedChildComponent>(1);
         })).Instance.ChildComponents;

        parentComponents1.Should().HaveCount(2);
        parentComponents1.Should().AllBeOfType<MyNestedChildComponent>();


        var parentComponent2 = GetComponent<MyNestedParentComponent>(p => p.AddChildContent(c =>
         {
             c.CreateComponent<MyNestedChildComponent>(0);
             c.CreateComponent<MyNestedChildComponent>(1);
         })).Instance.ChildComponents;

        parentComponent2.Should().HaveCount(2);
        parentComponent2.Should().AllBeOfType<MyNestedChildComponent>();
    }
}

[ChildComponent(typeof(MyParentComponent))]
class ChildComponent : BlazorComponentBase, IHasChildContent
{
    [CascadingParameter] public MyParentComponent Parent { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}

[ParentComponent]
[HtmlTag("tab")]
class TabComponent : BlazorComponentBase, IHasChildContent, IHasOnSwitch
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    public int? SwitchIndex { get; set; } = 0;
    public EventCallback<int?> OnSwitch { get; set; }
}

[ChildComponent(typeof(TabComponent))]
[HtmlTag("tabitem")]
class TabItemComponent : BlazorComponentBase, IHasChildContent, IHasOnActive
{
    [Parameter] public RenderFragment ChildContent { get; set; }
    [Parameter][CssClass("active")] public bool Active { get; set; }
    public EventCallback<bool> OnActive { get; set; }
}

[ParentComponent]
class MyParentComponent : BlazorComponentBase, IHasChildContent
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}

[ParentComponent]
class MyNestedParentComponent : BlazorComponentBase, IHasChildContent
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}

[ChildComponent(typeof(MyParentComponent))]
class MyChildComponent : BlazorComponentBase
{
    [CascadingParameter] public MyParentComponent Component { get; set; }
}

[ChildComponent(typeof(MyParentComponent))]
class MyNullableChildComponent : BlazorComponentBase
{
    [CascadingParameter] public MyParentComponent? Parent { get; set; }
}

[ChildComponent(typeof(MyParentComponent))]
class ReuiredChildComponent : BlazorComponentBase
{
    [CascadingParameter] public MyParentComponent ParentComponent { get; set; }
}

[ChildComponent(typeof(MyParentComponent), true)]
[ChildComponent(typeof(MyNestedParentComponent), true)]
class MyNestedChildComponent : BlazorComponentBase
{
    [CascadingParameter] public MyParentComponent? ParentComponent { get; set; }
    [CascadingParameter] public MyNestedParentComponent? NestedComponent { get; set; }
}