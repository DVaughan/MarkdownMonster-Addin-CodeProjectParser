using System.IO;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using MarkdownMonster;

namespace CodeProjectMarkdownParserAddin
{
	class MarkdownParserCodeProject : MarkdownParserMarkdig
	{
		public MarkdownParserCodeProject(bool pragmaLines = false, bool forceLoad = false)
			: base(pragmaLines, forceLoad)
		{
		}

		protected override IMarkdownRenderer CreateRenderer(TextWriter writer)
		{
			var renderer = new HtmlRenderer(writer);

			CodeBlockRenderer codeBlockRenderer = null;

			foreach (var objectRenderer in renderer.ObjectRenderers)
			{
				codeBlockRenderer = objectRenderer as CodeBlockRenderer;
				if (codeBlockRenderer != null)
				{
					break;
				}
			}

			var cpCodeBlockRenderer = new CPCodeBlockRenderer();

			if (codeBlockRenderer != null)
			{
				renderer.ObjectRenderers.Replace<CodeBlockRenderer>(cpCodeBlockRenderer);
			}
			else
			{
				renderer.ObjectRenderers.Add(cpCodeBlockRenderer);
			}

			return renderer;
		}
	}
}
