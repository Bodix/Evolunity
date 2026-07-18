// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Bodix.Evolunity.Editor
{
	public class WebGlTitlePostprocessor : IPostprocessBuildWithReport
	{
		public int callbackOrder => 0;
		private static string SettingsPath => "ProjectSettings/WebGLTitlePostprocessorSettings.txt";
		private static bool IsEnabled
		{
			get
			{
				if (File.Exists(SettingsPath))
				{
					string content = File.ReadAllText(SettingsPath);

					return content == "true";
				}

				return true;
			}
			set => File.WriteAllText(SettingsPath, value ? "true" : "false");
		}

		[MenuItem("Tools/WebGL/Remove Web Player from Title")]
		private static void ToggleAction()
		{
			IsEnabled = !IsEnabled;
		}

		[MenuItem("Tools/WebGL/Remove Web Player from Title", true)]
		private static bool ToggleActionValidate()
		{
			Menu.SetChecked("Tools/WebGL/Remove Web Player from Title", IsEnabled);
			return true;
		}

		public void OnPostprocessBuild(BuildReport report)
		{
			if (!IsEnabled)
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