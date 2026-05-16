// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEditor;
using UnityEngine.SceneManagement;

namespace Bodix.Evolunity.Editor.Utilities
{
	[InitializeOnLoad]
	public static class LastActiveSceneCache
	{
		public const string LastActiveScenePathKey = "LastActiveScenePath";

		static LastActiveSceneCache()
		{
			EditorApplication.playModeStateChanged += CacheLastActiveScene;
		}

		private static void CacheLastActiveScene(PlayModeStateChange state)
		{
			if (state == PlayModeStateChange.ExitingEditMode)
				EditorPrefs.SetString(LastActiveScenePathKey, SceneManager.GetActiveScene().path);
		}
	}
}