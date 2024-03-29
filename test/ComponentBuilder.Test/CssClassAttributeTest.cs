﻿using ComponentBuilder.Definitions;

using Microsoft.AspNetCore.Components;

namespace ComponentBuilder.Test;
public class CssClassAttributeTest : AutoTestBase
{
    //private readonly ICssClassResolver _resolver;
    //public CssClassAttributeResolverTest()
    //{
    //    _resolver = GetService<ICssClassResolver>();
    //}

    [Fact]
    public void Given_Component_For_Parameter_Mark_CssClass_When_Input_A_String_Then_Combile_Two_Of_Them()
    {
        var com = new ComponentWithStringParameter
        {
            Name = "abc",
            Block = true
        };

        var result = GetComponent<ComponentWithStringParameter>(m => m.Add(p => p.Name, "abc").Add(p => p.Block, true)).Should().HaveClass("cssabc").And.HaveClass("block");
    }

    public void Given_Component_For_Enum_When_Parameter_Has_CssClassAttribute_Then_Get_The_Css_By_Enum_Member()
    {
        //_resolver.Resolve(new ComponentWithEnumParameter
        //{
        //    Color = ComponentWithEnumParameter.ColorType.Primary
        //}).Should().Contain("primary");

        //_resolver.Resolve(new ComponentWithEnumParameter
        //{
        //    Color = ComponentWithEnumParameter.ColorType.Light
        //}).Should().Contain("lt");

        GetComponent<ComponentWithEnumParameter>(m => m.Add(p => p.BtnColor, ComponentWithEnumParameter.ColorType.Primary)).Should().HaveClass("btn-primary");


        GetComponent<ComponentWithEnumParameter>(m => m.Add(p => p.BtnColor, ComponentWithEnumParameter.ColorType.Light)).Should().HaveClass("btn-lt");

        //_resolver.Resolve(new ComponentWithEnumParameter
        //{
        //    BtnColor = ComponentWithEnumParameter.ColorType.Primary
        //}).Should().Contain("btn-primary");

        //_resolver.Resolve(new ComponentWithEnumParameter
        //{
        //    BtnColor = ComponentWithEnumParameter.ColorType.Light
        //}).Should().Contain("btn-lt");
    }

    [Fact]
    public void Given_Component_When_Parameter_Only_Has_CssClassAttribute_Without_Name_Then_Css_Name_Use_Parameter_Property_Name()
    {
        GetComponent<NoNameCssClassComponent>(m => m.Add(p => p.Margin, 1)).Should().HaveClass("margin1");

        //_resolver.Resolve(new NoNameCssClassComponent
        //{
        //    Margin = 1,
        //}).Should().Contain("margin1");
    }

    [Fact]
    public void Given_Component_Implement_ParameterInterface_When_Not_Use_CssClassAttribute_Then_Use_CssClass_From_Interface()
    {
        GetComponent<InterfaceComponent>(m => m.Add(p => p.Active, true)).Should().HaveClass("active");
        //_resolver.Resolve(new InterfaceComponent { Active = true }).Should().Contain("active");
    }

    [Fact]
    public void Given_Component_Implement_ParameterInterface_When_Use_CssClassAttribute_Then_Use_CssClass_From_Class()
    {
        GetComponent<InterfaceComponent>(m => m.Add(p => p.Toggle, true).Add(p => p.Active, true)).Should().HaveClass("toggle").And.HaveClass("active");

        //_resolver.Resolve(new InterfaceComponent { Toggle = true })
        //    .Should().Contain("toggle");

        //_resolver.Resolve(new InterfaceComponent { Toggle = true, Active = true })
        //    .Should().Contain("active toggle");
    }

    [Fact]
    public void Given_OrderedComponent_GetOrderedParameter_When_Parameter_Set_Then_Ordered()
    {
        GetComponent<OrderedComponent>(m => m.Add(p => p.Active, true).Add(p => p.Disabled, true)).Should().HaveAttribute("class", "hello disabled");
    }

    [Fact]
    public void Given_InterfaceClassComponent_When_Has_Interface_CssClassAttribute_Then_Use_Interface_CssClassAttribute()
    {
        //_resolver.Resolve(new InterfaceClassComponent()).Should().Contain("ui");

        GetComponent<InterfaceClassComponent>()
            .MarkupMatches("<div class=\"ui\"></div>");
    }

    [Fact]
    public void Given_InterfaceClassComponent_When_Has_Interface_CssClassAttribute_But_Class_Has_CssClassAttribute_Then_Use_Class_CssClassAttribute()
    {

        GetComponent<InterfaceClassOverrideComponent>().Should().HaveClass("ui").And.HaveClass("button");
        //_resolver.Resolve(new InterfaceClassOverrideComponent()).Should().Contain("ui button");
    }

    [Fact]
    public void Given_InterfaceClassComponent_When_Has_Interface_CssClassAttribute_ButDisabled_HasParameter_ThatDisabled_Then_Ignore_Class_That_Disabled()
    {
        GetComponent<DisableCssClassComponent>(m => m.Add(p => p.Toggle, true).Add(p => p.Disabled, true)).Should().HaveClass("ui").And.HaveClass("disabled");
    }

    [Fact]
    public void Given_BooleanCssAttribute_When_Parameter_Is_True_Then_Get_TrueCssClass_When_Parameter_Is_False_Then_Get_FalseCssClass()
    {
        GetComponent<BoolAttributeComponent>(m => m.Add(p => p.Make, true)).Should().HaveClass("make");
        GetComponent<BoolAttributeComponent>(m => m.Add(p => p.Make, false)).Should().HaveClass("made");

        //_resolver.Resolve(new BoolAttributeComponent { Make = true }).Should().Contain("make");
        //_resolver.Resolve(new BoolAttributeComponent { Make = false }).Should().Contain("made");

    }

    [Fact]
    public void Given_Render_Component_Check_CssClass_Order_When_Implement_From_Interface_Then_According_To_Order_To_Get_CssClass()
    {
        GetComponent<OrderCssClassComponent>().Should().HaveAttribute("class", "ui order visible");

        //_resolver.Resolve(new OrderCssClassComponent()).ToString().Should().Be("ui order visible");

        GetComponent<OrderWithParameterCssClassComponent>(m => m.Add(p => p.Disabled, true).Add(p => p.Active, true))
            .Should().HaveAttribute("class", "ui disabled order active visible");

        //_resolver.Resolve(new OrderWithParameterCssClassComponent { Disabled = true, Active = true }).Should().Contain("ui disabled order active visible");
    }

    [Fact]
    public void Given_Render_Component_Has_NullCssClass_With_Parameter_When_Parameter_Is_Not_Null_Then_Has_No_CssClass_Value()
    {
        //_resolver.Resolve(new NullParameterCssClassComponent()).Should().NotBeEmpty();

        GetComponent<NullParameterCssClassComponent>(m => m.Add(p => p.Disabled, true))
            .Should().NotHaveClass("btn-disbaled");
    }

    [Fact]
    public void Given_Render_Component_Has_NullCssClass_With_Parameter_When_Parameter_Is_Null_Then_Has_CssClass_Value()
    {
        //_resolver.Resolve(new NullParameterCssClassComponent()).Should().Contain("btn-disabled");

        GetComponent<NullParameterCssClassComponent>().Should().HaveClass("btn-disabled");
    }


    public void Given_Render_Component_Has_StringFormat_CssClass()
    {
        GetComponent<FormatCssClassComponent>(m => m.Add(p => p.Margin, 5)).Should().HaveClass("m-5-1");
    }

    [Fact]
    public void Test_Drived_Component_CssClass_Can_Concat_From_Base_Component()
    {
        GetComponent<ConcatChildComponent>()
            .Should().HaveClass("concat-child").And.HaveClass("concat-base");
    }

    [Fact]
    public void Test_ArrayParameter_Concat()
    {
        GetComponent<EnumerableParameterComponent>(m => m.Add(p => p.Value, new[] { "first", "second" }))
            .Should().HaveClass("test-first").And.HaveClass("test-second");
    }

    [Fact]
    public void Test_ArrayParameter_Concat_For_Enum()
    {
        GetComponent<EnumerableParameterComponent>(m => m.Add(p => p.BreakPoints, new[] { EnumerableParameterComponent.BreakPoint.Samll, EnumerableParameterComponent.BreakPoint.Large, }))
            .Should().HaveClass("bp-sm").And.HaveClass("bp-lg");
    }

    [Fact]
    public void Test_CssClassBuilder_Parameter()
    {
        GetComponent<ClassBuilderParameterComponent>(m => m.Add(p => p.CssClass, HtmlHelper.Class.Append("hello")))
            .Should().HaveClass("hello");
    }
}

class ComponentWithStringParameter : BlazorComponentBase
{
    [Parameter][CssClass("css")] public string Name { get; set; }

    [Parameter][CssClass("block")] public bool Block { get; set; }
}

class ComponentWithEnumParameter : BlazorComponentBase
{
    internal enum ColorType
    {
        Primary,
        Secondary,
        Danger,
        [CssClass("lt")] Light
    }

    [Parameter][CssClass] public ColorType? Color { get; set; }

    [Parameter][CssClass("btn-")] public ColorType? BtnColor { get; set; }
}



interface IActiveParameter
{
    [CssClass("active")] bool Active { get; set; }
}

interface IToggleParameter
{
    [CssClass("disabled")] bool Toggle { get; set; }
}

interface IDisableParameter
{
    [CssClass("disabled", Order = 100)] bool Disabled { get; set; }
}

[CssClass("ui")]
interface IComponentUI
{

}

class InterfaceComponent : BlazorComponentBase, IActiveParameter, IToggleParameter
{
    [Parameter] public bool Active { get; set; }
    [Parameter][CssClass("toggle")] public bool Toggle { get; set; }
}


class OrderedComponent : BlazorComponentBase, IActiveParameter, IDisableParameter
{
    [Parameter] public bool Disabled { get; set; }
    [Parameter][CssClass("hello", Order = 5)] public bool Active { get; set; }
}

class InterfaceClassComponent : BlazorComponentBase, IComponentUI
{

}

[CssClass("button")]
class InterfaceClassOverrideComponent : BlazorComponentBase, IComponentUI
{
}

[CssClass(Disabled = true)]
class DisableCssClassComponent : BlazorComponentBase, IComponentUI, IToggleParameter, IDisableParameter
{
    [Parameter] public bool Disabled { get; set; }
    [Parameter][CssClass(Disabled = true)] public bool Toggle { get; set; }
}
class NoNameCssClassComponent : BlazorComponentBase
{
    [Parameter][CssClass("margin")] public int Margin { get; set; }
}

class BoolAttributeComponent : BlazorComponentBase
{
    [Parameter][BooleanCssClass("make", "made")] public bool? Make { get; set; }
}

class FormatCssClassComponent : BlazorComponentBase
{
    [Parameter][CssClass("m-{0}-1")] public int? Margin { get; set; }
}


[CssClass("ui", Order = -999)]
interface IHasUI { }

[CssClass("visible", Order = 100)]
interface IHasVisible { }

[CssClass("order", Order = 10)]
class OrderCssClassComponent : BlazorComponentBase, IHasUI, IHasVisible
{

}

[CssClass("order", Order = 10)]
class OrderWithParameterCssClassComponent : BlazorComponentBase, IHasUI, IHasVisible, IHasDisabled
{
    [Parameter][CssClass("active", Order = 15)] public bool Active { get; set; }
    [Parameter][CssClass("disabled")] public bool Disabled { get; set; }
}

class NullParameterCssClassComponent : BlazorComponentBase
{
    [Parameter][NullCssClass("btn-disabled")] public bool? Disabled { get; set; }
}

[CssClass("concat-base")]
class ConcatBaseComponent : BlazorComponentBase
{

}

[CssClass("concat-child", Inherited = true)]
class ConcatChildComponent : ConcatBaseComponent
{

}


class EnumerableParameterComponent : BlazorComponentBase
{
    [Parameter][CssClass("test-")] public IEnumerable<string> Value { get; set; } = [];
    [Parameter][CssClass("bp-")] public IEnumerable<BreakPoint> BreakPoints { get; set; } = [];


    public enum BreakPoint
    {
        [CssClass("sm")] Samll,
        [CssClass("md")] Medium,
        [CssClass("lg")] Large,
        [CssClass("xl")] ExtraLarge,
        [CssClass("xxl")] ExtraExtraLarge
    }
}

class ClassBuilderParameterComponent : BlazorComponentBase
{
    [Parameter][CssClass] public ICssClassBuilder? CssClass { get; set; }
}