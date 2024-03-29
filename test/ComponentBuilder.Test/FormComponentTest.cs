﻿using ComponentBuilder.Definitions;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

using System.Collections.Generic;
using System.Linq.Expressions;

namespace ComponentBuilder.Test;
public class FormComponentTest : AutoTestBase
{
    public FormComponentTest()
    {

    }

    [Fact]
    public void Given_A_Form_Then_Has_Form_Element_Tag()
    {
        GetComponent<TestForm>(p => p.Add(m => m.Model, this))
            .Should().HaveTag("form");
    }

    //[Fact]
    //public void Test_Input_String()
    //{
    //    var value = "";
    //    GetComponent<TestInput>(p => p.Bind(m => m.Value, value, changedValue =>
    //    {
    //        value = changedValue;
    //    }, () => value));
    //    Assert.NotEmpty(value);
    //}
}
[HtmlTag("form")]
class TestForm : BlazorComponentBase, IFormComponent
{
    [Parameter] public object? Model { get; set; }
    [Parameter] public EventCallback<FormEventArgs> OnSubmit { get; set; }
    [Parameter] public bool IsSubmitting { get; set; }
    [Parameter] public EditContext? EditContext { get; set; }
    [Parameter] public RenderFragment<EditContext>? ChildContent { get; set; }
}

class TestInput : BlazorComponentBase, IHasInputValue<string?>
{
    [Parameter] public Expression<Func<string?>>? ValueExpression { get; set; }
    [CascadingParameter] public EditContext? CascadedEditContext { get; set; }
    [Parameter] public string? Value { get; set; } = "hello";
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }

    protected override void AfterSetParameters(ParameterView parameters)
    {
        this.InitializeInputValue();
    }

    protected override void BuildAttributes(IDictionary<string, object?> attributes)
    {
        attributes["value"] = this.GetValueAsString();
        attributes["onchange"] = this.CreateValueChangedCallback();
    }

    protected override void DisposeComponentResources()
    {
        this.DisposeInputValue();
    }
}