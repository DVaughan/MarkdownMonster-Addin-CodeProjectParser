using MarkdownMonster.AddIns;

namespace CodeProjectMarkdownParserAddin
{
	public class CodeProjectMarkdownParserAddinConfiguration : BaseAddinConfiguration<CodeProjectMarkdownParserAddinConfiguration>
	{
		public CodeProjectMarkdownParserAddinConfiguration()
		{
			// uses this file for storing settings in `%appdata%\Markdown Monster`
			// to persist settings call `CodeProjectMarkdownParserAddinConfiguration.Current.Write()`
			// at any time or when the addin is shut down
			ConfigurationFilename = "CodeProjectMarkdownParserAddin.json";
		}

		// Add properties for any configuration setting you want to persist and reload
		// you can access this object as 
		//     CodeProjectMarkdownParserAddinConfiguration.Current.PropertyName
	}
}