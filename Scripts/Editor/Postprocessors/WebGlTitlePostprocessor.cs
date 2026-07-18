// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Bodix.Evolunity.Editor.Postprocessors
{
	public class WebGlTitlePostprocessor : IPostprocessBuildWithReport
	{
		public int callbackOrder => 0;

		[MenuItem("Tools/WebGL/Remove Web Player from Title")]
		private static void ToggleAction()
		{
			WebGlPostprocessorsSettings settings = WebGlPostprocessorsSettings.Load();
			settings.NicifyTitle = !settings.NicifyTitle;
			settings.Save();
		}

		[MenuItem("Tools/WebGL/Remove Web Player from Title", true)]
		private static bool ToggleActionValidate()
		{
			WebGlPostprocessorsSettings settings = WebGlPostprocessorsSettings.Load();
			Menu.SetChecked("Tools/WebGL/Remove Web Player from Title", settings.NicifyTitle);
			return true;
		}

		public void OnPostprocessBuild(BuildReport report)
		{
			WebGlPostprocessorsSettings settings = WebGlPostprocessorsSettings.Load();
			if (!settings.NicifyTitle)
				return;

			if (report.summary.platform == BuildTarget.WebGL)
			{
				string indexPath = Path.Combine(report.summary.outputPath, "index.html");

				if (File.Exists(indexPath))
				{
					string html = File.ReadAllText(indexPath);
					string productName = PlayerSettings.productName;

					// Replace the old title with the clean product name.
					html = Regex.Replace(html, @"<title>.*?</title>", $"<title>{productName}</title>");

					File.WriteAllText(indexPath, html);
				}
			}
		}
	}
}