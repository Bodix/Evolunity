// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEditor;

namespace Bodix.Evolunity.Editor.Postprocessors
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