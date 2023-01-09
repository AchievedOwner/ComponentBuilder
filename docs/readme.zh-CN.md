## Quick Start
**���� 2 �����ɴ����Զ������**

1. �̳� `BlazorComponentBase` ������ `ComponentBase`
2. **���ڡ�.razor���ļ����**����Ϊ����ָ�� HTML Ԫ�ص��Զ����������`@attributes="Additional Attributes"`����
2. Ϊ������������ `CssClassAttribute` ����

## `.razor` �ļ�������
* ���� `Element.razor` �ļ�
```html
@inherits BlazorComponentBase

<span @attributes="AdditionalAttributes"> <!--@attributes �Ǳ����-->
	@ChildContent
</span>

@code{
	[Parameter] public RenderFragment? ChildContent { get; set; }

	[Parameter][CssClass("active")] public bool Active { get; set; }
}
```
* �� razor ��ִ��
```html
<Element>Hello</Element>
<span>Hello</span>

<Element Active>Active Hello</Element>
<span class="active">Active Hello</span>
```

## ʹ�� `Element.cs` ��
* ���� `Element` ��
```csharp
[HtmlTag("span")]
public class Element : BlazorComponentBase, IHasChildContent
{
	[Parameter] public RenderFragment? ChildContent { get; set; }

	[Parameter][CssClass("active")] public bool Active { get; set; }
}
```
* �� razor ��ִ��
```html
<Element>Hello</Element>
<span>Hello</span>

<Element Active>Active Hello</Element>
<span class="active">Active Hello</span>
```

> ����������ʹ������������ȫ�Զ����������ȱ��������Ҫһ���ľ���˼ά���������ڴ��������ʱ����Ȼ�������Խ����߻��ʹ�ã���ֻҪ�����Ч�ʣ����Ǹ������⡣