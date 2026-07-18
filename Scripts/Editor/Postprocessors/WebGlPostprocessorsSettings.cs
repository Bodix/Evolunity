using System.IO;
using UnityEngine;

namespace Bodix.Evolunity.Editor.Postprocessors
{
	[System.Serializable]
	public class WebGlPostprocessorsSettings
	{
		public bool NicifyTitle = true;
		public string FaviconPath = "Assets/favicon.ico";

		private static string SettingsPath => "ProjectSettings/WebGLPostprocessorsSettings.json";

		public static WebGlPostprocessorsSettings Load()
		{
			if (File.Exists(SettingsPath))
			{
				string json = File.ReadAllText(SettingsPath);

				return JsonUtility.FromJson<WebGlPostprocessorsSettings>(json);
			}

			return new WebGlPostprocessorsSettings();
		}

		public void Save()
		{
			string json = JsonUtility.ToJson(this, true);

			File.WriteAllText(SettingsPath, json);
		}
	}
}