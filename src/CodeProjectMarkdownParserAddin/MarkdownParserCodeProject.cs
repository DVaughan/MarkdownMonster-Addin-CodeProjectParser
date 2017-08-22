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

		public override string Parse(string markdown)
		{
			var parsed = base.Parse(markdown);

			return parsed;
		}

		protected override IMarkdownRenderer CreateRenderer(TextWriter writer)
		{
			var renderer = new HtmlRenderer(writer);

			CodeBlockRenderer codeBlockRender = null;

			foreach (var objectRenderer in renderer.ObjectRenderers)
			{
				codeBlockRender = objectRenderer as CodeBlockRenderer;
				if (codeBlockRender != null)
				{
					break;
				}
			}

			var cpCodeBlockRenderer = new CPCodeBlockRenderer();

			if (codeBlockRender != null)
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
