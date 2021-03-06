using ComponentBuilder.Abstrations;

using ComponentBuilder.Parameters;

using Microsoft.AspNetCore.Components;

namespace ComponentBuilder.Test
{
    public class CssClassAttributeResolverTest : TestBase
    {
        private readonly ICssClassAttributeResolver _resolver;
        public CssClassAttributeResolverTest()
        {
            _resolver = GetService<ICssClassAttributeResolver>();
        }

        [Fact]
        public void Given_Invoke_Resolve_Method_When_Input_Null_Value_Then_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _resolver.Resolve(null));
        }

        [Fact]
        public void Given_Component_For_Parameter_Mark_CssClass_When_Input_A_String_Then_Combile_Two_Of_Them()
        {
            var com = new ComponentWithStringParameter
            {
                Name = "abc",
                Block = true
            };

            var result = _resolver.Resolve(com);
            result.Should().Be("cssabc block");
        }

        [Fact]
        public void Given_Component_For_Enum_When_Parameter_Has_CssClassAttribute_Then_Get_The_Css_By_Enum_Member()
        {
            _resolver.Resolve(new ComponentWithEnumParameter
            {
                Color = ComponentWithEnumParameter.ColorType.Primary
            }).Should().Be("primary");

            _resolver.Resolve(new ComponentWithEnumParameter
            {
                Color = ComponentWithEnumParameter.ColorType.Light
            }).Should().Be("lt");


            _resolver.Resolve(new ComponentWithEnumParameter
            {
                BtnColor = ComponentWithEnumParameter.ColorType.Primary
            }).Should().Be("btn-primary");

            _resolver.Resolve(new ComponentWithEnumParameter
            {
                BtnColor = ComponentWithEnumParameter.ColorType.Light
            }).Should().Be("btn-lt");
        }

        [Fact]
        public void Given_Component_When_Parameter_Only_Has_CssClassAttribute_Without_Name_Then_Css_Name_Use_Parameter_Property_Name()
        {
            _resolver.Resolve(new NoNameCssClassComponent
            {
                Margin = 1,
            }).Should().Be("margin1");
        }

        [Fact]
        public void Given_Component_When_CssClassAttribute_Suffix_Is_True_For_Parameter_Then_CssClassAttributeValue_Is_Suffix_Of_Parameter_Value()
        {
            _resolver.Resolve(new SuffixComponent
            {
                Padding = 1,
            }).Should().Be("1-p");
        }

        [Fact]
        public void Given_Component_Implement_ParameterInterface_When_Not_Use_CssClassAttribute_Then_Use_CssClass_From_Interface()
        {
            _resolver.Resolve(new InterfaceComponent { Active = true })
                .Should().Be("active");
        }

        [Fact]
        public void Given_Component_Implement_ParameterInterface_When_Use_CssClassAttribute_Then_Use_CssClass_From_Class()
        {
            _resolver.Resolve(new InterfaceComponent { Toggle = true })
                .Should().Be("toggle");

            _resolver.Resolve(new InterfaceComponent { Toggle = true, Active = true })
                .Should().Be("active toggle");
        }

        [Fact]
        public void Given_OrderedComponent_GetOrderedParameter_When_Parameter_Set_Then_Ordered()
        {
            _resolver.Resolve(new OrderedComponent { Active = true, Disabled = true })
                .Should().Be("hello disabled");
        }

        [Fact]
        public void Given_InterfaceClassComponent_When_Has_Interface_CssClassAttribute_Then_Use_Interface_CssClassAttribute()
        {
            _resolver.Resolve(new InterfaceClassComponent()).Should().Be("ui");

            TestContext.RenderComponent<InterfaceClassComponent>()
                .MarkupMatches("<div class=\"ui\"></div>");
        }

        [Fact]
        public void Given_InterfaceClassComponent_When_Has_Interface_CssClassAttribute_But_Class_Has_CssClassAttribute_Then_Use_Class_CssClassAttribute()
        {
            _resolver.Resolve(new InterfaceClassOverrideComponent()).Should().Be("ui button");
        }

        [Fact]
        public void Given_InterfaceClassComponent_When_Has_Interface_CssClassAttribute_ButDisabled_HasParameter_ThatDisabled_Then_Ignore_Class_That_Disabled()
        {
            _resolver.Resolve(new DisableCssClassComponent { Toggle = true, Disabled = true }).Should().Be("ui disabled");
        }

        [Fact]
        public void Given_BooleanCssAttribute_When_Parameter_Is_True_Then_Get_TrueCssClass_When_Parameter_Is_False_Then_Get_FalseCssClass()
        {
            _resolver.Resolve(new BoolAttributeComponent { Make = true }).Should().Be("make");
            _resolver.Resolve(new BoolAttributeComponent { Make = false }).Should().Be("made");

        }

        [Fact]
        public void Given_Render_Component_Check_CssClass_Order_When_Implement_From_Interface_Then_According_To_Order_To_Get_CssClass()
        {
            _resolver.Resolve(new OrderCssClassComponent()).ToString().Should().Be("ui order visible");

            _resolver.Resolve(new OrderWithParameterCssClassComponent { Disabled = true, Active = true }).Should().Be("ui disabled order active visible");
        }
    }

    class ComponentWithStringParameter : BlazorComponentBase
    {
        [CssClass("css")] public string Name { get; set; }

        [CssClass("block")] public bool Block { get; set; }
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

        [CssClass] public ColorType? Color { get; set; }

        [CssClass("btn-")] public ColorType? BtnColor { get; set; }
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
        public bool Active { get; set; }
        [CssClass("toggle")] public bool Toggle { get; set; }
    }


    class OrderedComponent : BlazorComponentBase, IActiveParameter, IDisableParameter
    {
        public bool Disabled { get; set; }
        [CssClass("hello", Order = 5)] public bool Active { get; set; }
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
        public bool Disabled { get; set; }
        [CssClass(Disabled = true)] public bool Toggle { get; set; }
    }
    class NoNameCssClassComponent : BlazorComponentBase
    {
        [CssClass("margin")] public int Margin { get; set; }
    }
    class SuffixComponent : BlazorComponentBase
    {
        [CssClass("-p", Suffix = true)] public int Padding { get; set; }
    }

    class BoolAttributeComponent : BlazorComponentBase
    {
        [BooleanCssClass("make", "made")] public bool? Make { get; set; }
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
        [CssClass("active", Order = 15)] public bool Active { get; set; }
        public bool Disabled { get; set; }
    }
}
