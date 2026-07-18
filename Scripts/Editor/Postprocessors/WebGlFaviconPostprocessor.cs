using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Bodix.Evolunity.Editor.Postprocessors
{
	public class WebGlFaviconPostprocessor : IPostprocessBuildWithReport
	{
		public int callbackOrder => 1;

		public void OnPostprocessBuild(BuildReport report)
		{
			if (report.summary.platform != BuildTarget.WebGL)
				return;

			WebGlPostprocessorsSettings settings = WebGlPostprocessorsSettings.Load();
			string sourceIconPath = settings.FaviconPath;

			if (string.IsNullOrEmpty(sourceIconPath))
				return;

			if (!Path.IsPathRooted(sourceIconPath))
				sourceIconPath = Path.Combine(Directory.GetCurrentDirectory(), sourceIconPath);

			string targetIconPath = Path.Combine(report.summary.outputPath, "TemplateData", "favicon.ico");

			if (File.Exists(sourceIconPath) && Directory.Exists(Path.Combine(report.summary.outputPath, "TemplateData")))
				File.Copy(sourceIconPath, targetIconPath, true);
			else if (!File.Exists(sourceIconPath))
				Debug.LogWarning($"Favicon NOT found at path: {sourceIconPath}. Default icon will be used.");
		}
	}
}