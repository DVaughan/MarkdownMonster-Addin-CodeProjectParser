## Introduction

This file demonstrates the CodeProject markdown parser for Markdown Monster.

```csharp
void CopyHtmlToClipboard()
{
	MarkdownDocument document = ActiveDocument;
	string html = document.RenderHtml();

	Clipboard.SetText(html);

	ShowStatus("HTML copied to clipboard.");
}
```

```javascript
var foo = "bah";
```

```cpp
i++;
```