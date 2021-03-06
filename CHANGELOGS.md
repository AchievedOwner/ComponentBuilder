# Change Logs
## 1.2
* [New]`AnchorComponentBase` to provide user create any component with anchor function.
* [New]Enum supoort `CssClassAttribute` for prefix of CSS class when building.
* [New]Add `Enumeration` abstract class to define a class like enum.
* [Rename]All `ComponentBase` with this library change namespace to `ComponentBuilder`
## 1.1
* [New]`ParentComponentAttribute` to create cascading component automatically
* [New]`ChildComponentAttribute` to get cascading parameter for component and add to be child of component
* [New]`RenderComponentAttribute` to render a specific component
* [Rename]`BlazorFormBase` to `BlazorFormComponentBase`
* [Rename]`BlazorInputBase` to `BlazorInputComponentBase`
## 0.7
* [New]`BlazorFormBase<TForm>` to help create form component.
* [New]`BlazorInputBase<TValue>` to help create input control component with two-way binding
* [New]`CreateStyleBuilder` in `CssHelper`
* [New]`CreateCallback` method in `HtmlHelper` to create `EventCallback` delegate
* [Rename]`CssHelper` to `HtmlHelper`, `MergeAttributes` to `MergeHtmlAttrbutes`, `CreateBuilder` to `CreateCssBuilder`
## 0.6
* [New]`appendFunc` argument witch is a `Func<RenderTreeBuilder, int, int>` type for extension method `CreateElement` and `CreateComponent`
* [New] Override `BuildStyle` method to build `style` pre-definition of element
* [New]`AdditionalStyle` parameter to append value after `BuildStyle` method executed
* [New]`Insert` method for `ICssClassBuilder`
* [New]`Suffix` property for `CssClassAttribute` to change append value of parameter from **prefix** to **suffix**
* [New]`HtmlDataAttribute` to build `data-{name}="{value}"` for parameter
* [New]`BlazorComponentCollection` class
* [New]`BooleanCssClassAttribute` class to apply CSS class for parameter witch is `bool` data type
* [New]interface pre-definition to apply CSS class
* [New]Add events `OnCssClassBuilding` & `OnCssClassBuilt` in `GetCssClassString` method to raise.
* [Fixed]`HtmlAttributeAttribute` to support enum member
* [Fixed]`GetCssClassString` does not support for `CssClassAttribute` and `CssClass` when has parameters
* [Fixed]A lot of bugs
## 0.5
* [Rename]`BuildBlazorComponentAttributes` method to `BuildComponentAttributes`
* [New]`HtmlEventAttribute` for `EventCallback` parameter
* [New]`IHtmlEventAttributeResolver` and default implementation `HtmlEventAttributeResolver` to resolve `HtmlEventAttribute`
* [New]`TagName` property instead `GetElementTagName` method
* [New]`Disabled` property for `CssClassAttribute` to prevent apply CSS class
* [Delete] `GetElementTagName` method
## 0.4
* [New] `Import` method for `IJSRuntime` and call js function in C# dynamiclly.
* [Fix] `CreateElement` & `CreateComponent` Cannot create child content using `string`
* [Fix] Renamed `Js` property in `BlazorComponentBase` to `JS`
## 0.3
* [New] `CssClass` parameter in `BlazorComponentBase` for builing css class utility by extensions class.
* [New] `GetCssClassString()` method can be overrided by drived class.
* [New] Discover `class` attribute in element can override any configurations of `CssClassAttribute` of parameter.
* [New] More extensions for `CreateElement` and `CreateComponent` method for `RenderTreeBuilder` class.
* [Fix] Rename `HtmlHelper` to `CssHelper`
* [Fix] Change `AdditionalAttributes` to be `IDictionary<string, object>` type.
## 0.2
* [Refactor] Some methods in `BlazorComponentBase`
* [New] `BlazorChildContentComponentBase` class within `ChildContent` parameter
* [New] `BlazorChildComponentBase` & `BlazorParentComponentBase` class
* [Update] `ElementAttributeAttribute` support null name for parameter name
## 0.1
Initial projects


