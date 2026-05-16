using UnityEditor;

namespace Evolutex.Evolunity.Editor
{
	public class NamespacePostprocessor : AssetPostprocessor
	{
		public static string OnGeneratedCSProject(string path, string content)
		{
			if (path.EndsWith("Evolunity.Runtime.csproj") || path.EndsWith("Evolunity.Editor.csproj"))
			{
				string rootNamespaceTag = "<RootNamespace>Bodix</RootNamespace>";

				content = content.Replace("<RootNamespace></RootNamespace>", rootNamespaceTag);
			}

			return content;
		}
	}
}