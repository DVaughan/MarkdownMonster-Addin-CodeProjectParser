using System.Collections.Generic;
using System.Linq;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace CodeProjectMarkdownParserAddin
{
	/// <summary>
	/// The purpose of this renderder is to change the behavior 
	/// of the Markdig CodeBlockRenderer, and to make the resulting HTML
	/// compatible with CodeProject.
	/// CodeProject uses pre tags decorated with a lang attribute
	/// to indicate the programming language. CodeProject also
	/// doesn't like the nest &lt;code&gt; element that the default
	/// <c>CodeBlockRenderer</c> produces.
	/// </summary>
	public class CPCodeBlockRenderer : CodeBlockRenderer
	{
		protected override void Write(HtmlRenderer renderer, CodeBlock codeBlock)
		{
			renderer.EnsureLine();
			renderer.Write("<pre");

			var attributes = codeBlock.TryGetAttributes();
			string cssClass = attributes?.Classes.FirstOrDefault();

			if (cssClass != null)
			{
				string langAttributeValue = TranslateCodeClass(cssClass);
				renderer.Write(" lang=\"");
				renderer.WriteEscape(langAttributeValue);
				renderer.Write("\" ");
			}

			if (attributes?.Id != null)
			{
				renderer.Write(" id=\"").WriteEscape(attributes.Id).Write("\" ");
			}

			renderer.Write(">");

			renderer.WriteLeafRawLines(codeBlock, true, true);
			renderer.WriteLine("</pre>");
		}

		string TranslateCodeClass(string cssClass)
		{
			string result;
			if (!langLookup.TryGetValue(cssClass, out result))
			{
				const string languagePrefix = "language-";
				if (cssClass.StartsWith(languagePrefix))
				{
					result = cssClass.Substring(languagePrefix.Length);
				}
			}

			return result;
		}

		Dictionary<string, string> langLookup = new Dictionary<string, string>
		{
			{"language-csharp", "cs"},
			{"language-javascript", "jscript"},
			//			{"language-asp", "html"},
			//			{"language-css", "css" },
			//			{"language-cpp", "c++" },
			//			{"language-cpp", "mc++" },
			//			{"language-fsharp", "f#" },
			//			{"", "fortran" },
			//			{"", "html" },
			//			{"", "java" },
			//			{"", "asm" },
			//			{"", "bat" },
			//			{"", "msil" },
			//			{"", "midl" },
			//			{"", "objc" },
			//			{"", "pascal" },
			//			{"", "perl" },
			//			{"", "powershell" },
			//			{"", "php" },
			//			{"", "python" },
			//			{"", "razor" },
			//			{"", "sql" },
			//			{"", "swift" },
			//			{"", "vb.net" },
			//			{"", "vbscript" },
			//			{"language-xml", "xml"},
		};
	}
}