using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows;
using FontAwesome.WPF;
using MarkdownMonster;
using MarkdownMonster.AddIns;

namespace CodeProjectMarkdownParserAddin
{
	public class CodeProjectMarkdownParserAddin : MarkdownMonsterAddin
	{
		public override void OnApplicationStart()
		{
			base.OnApplicationStart();

			Id = "CodeProjectMarkdownParserAddin";

			Name = "CodeProject Markdown Parser";

			AddInMenuItem menuItem = new AddInMenuItem(this)
			{
				Caption = "Copy HTML to Clipboard",
				FontawesomeIcon = FontAwesomeIcon.Clipboard
			};

			// if you don't want to display config or main menu item clear handler
			menuItem.ExecuteConfiguration = null;

			// Must add the menu to the collection to display menu and toolbar items            
			MenuItems.Add(menuItem);

			EnsureThemeExists();
		}

		public override IMarkdownParser GetMarkdownParser()
		{
			return new MarkdownParserCodeProject();
		}

		public override void OnExecute(object sender)
		{
			CopyHtmlToClipboard();
		}

		void CopyHtmlToClipboard()
		{
			MarkdownDocument document = ActiveDocument;
			string html = document.RenderHtml();

			Clipboard.SetText(html);

			ShowStatus("HTML copied to clipboard.", 3000);
		}

		void EnsureThemeExists()
		{
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var previewThemesDirectory = Path.Combine(baseDirectory, "PreviewThemes");

			if (!Directory.Exists(previewThemesDirectory))
			{
				/* Unable to find preview themes directory. Abort. */
				return;
			}

			var cpThemeDir = Path.Combine(previewThemesDirectory, "CodeProject");

			if (!Directory.Exists(cpThemeDir))
			{
				Directory.CreateDirectory(cpThemeDir);
			}

			string themeFile = Path.Combine(previewThemesDirectory, "Theme.html");

			if (!File.Exists(themeFile))
			{
				CopyEmbeddedResources(cpThemeDir, "CodeProjectMarkdownParserAddin.PreviewTheme", 
					new List<string> { "CodeProject_Main.min.css", "Theme.html" });
			}
		}

		static void CopyEmbeddedResources(string outputDir, string resourceLocation, List<string> files)
		{
			var assembly = Assembly.GetExecutingAssembly();

			foreach (var file in files)
			{
				string embeddedResourcePath = resourceLocation + @"." + file;
				using (Stream stream = assembly.GetManifestResourceStream(embeddedResourcePath))
				{
					if (stream == null)
					{
						throw new Exception("Unable to locate embedded resource " + embeddedResourcePath);
					}

					string filePath = Path.Combine(outputDir, file);
					using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
					{
						for (var i = 0; i < stream.Length; i++)
						{
							fileStream.WriteByte((byte)stream.ReadByte());
						}

						fileStream.Close();
					}
				}
			}
		}
	}
}
