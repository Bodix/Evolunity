// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.IO;
using Bodix.Evolunity.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Editor
{
	public static class MenuItems
	{
		[MenuItem("Edit/Create Group")]
		[MenuItem("Tools/Evolunity/Create Group %g")]
		public static void Group()
		{
			GameObject group = new GameObject("Group");
			Undo.RegisterCreatedObjectUndo(group, "Group");

			if (Selection.gameObjects.Length > 0)
				foreach (GameObject gameObject in Selection.gameObjects)
					Undo.SetTransformParent(gameObject.transform, group.transform, "Group");

			Selection.activeGameObject = group;
			SceneHierarchy.SetExpanded(group, true);
		}

		[MenuItem("Edit/Toggle Inspector Lock", priority = 143)]
		[MenuItem("Tools/Evolunity/Toggle Inspector Lock %&e")]
		public static void ToggleInspectorLock()
		{
			ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
			ActiveEditorTracker.sharedTracker.ForceRebuild();
		}

		[MenuItem("Assets/Take Screenshot")]
		[MenuItem("Tools/Evolunity/Take Screenshot &s")]
		public static void TakeScreenshot()
		{
			CameraScreenshot.Take();
		}

		[MenuItem("Assets/Open Persistent Data Folder", priority = 111)]
		[MenuItem("Tools/Evolunity/Open/Open Persistent Data Folder")]
		public static void OpenPersistentDataFolder()
		{
			FileExplorer.Open(Application.persistentDataPath);
		}

		[MenuItem("Assets/Open Temporary Cache Folder", priority = 112)]
		[MenuItem("Tools/Evolunity/Open/Open Temporary Cache Folder")]
		public static void OpenTemporaryCacheFolder()
		{
			FileExplorer.Open(Application.temporaryCachePath);
		}

		[MenuItem("Edit/Open/Open Editor Folder", priority = 268)]
		[MenuItem("Tools/Evolunity/Open/Open Editor Folder")]
		public static void OpenEditorFolder()
		{
			FileExplorer.Open(EditorApplication.applicationPath);
		}

		[MenuItem("Edit/Open/Open Editor Logs Folder", priority = 268)]
		[MenuItem("Tools/Evolunity/Open/Open Editor Logs Folder")]
		private static void OpenEditorLogsFolder()
		{
#if UNITY_EDITOR_OSX
			string rootFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string logsFolderPath = Path.Combine(rootFolderPath, "Library", "Logs", "Unity");
#elif UNITY_EDITOR_WIN
			string rootFolderPath = Environment.ExpandEnvironmentVariables("%localappdata%");
			string logsFolderPath = Path.Combine(rootFolderPath, "Unity", "Editor");
#endif

			FileExplorer.Open(logsFolderPath);
		}

		[MenuItem("Edit/Open/Open Asset Store Packages Folder", priority = 268)]
		[MenuItem("Tools/Evolunity/Open/Open Asset Store Packages Folder")]
		private static void OpenAssetStorePackagesFolder()
		{
#if UNITY_EDITOR_OSX
            string rootFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string unityFolderPath = Path.Combine(rootFolderPath, "Library", "Unity");
#elif UNITY_EDITOR_WIN
			string rootFolderPath = Environment.ExpandEnvironmentVariables("%appdata%");
			string unityFolderPath = Path.Combine(rootFolderPath, "Unity");
#endif
			string packagesFolderPath = Path.Combine(unityFolderPath, "Asset Store-5.x");

			FileExplorer.Open(packagesFolderPath);
		}

#if DEVELOPMENT
        [MenuItem("Edit/Defines/" + Define.DEVELOPMENT + "/Remove", priority = 269)]
        [MenuItem("Tools/Evolunity/Defines/" + Define.DEVELOPMENT + "/Remove")]
        public static void RemoveDevelopmentDefine()
        {
            Define.Set(Define.DEVELOPMENT, false);
        }
#else
		[MenuItem("Edit/Defines/" + Define.DEVELOPMENT + "/Add", priority = 269)]
		[MenuItem("Tools/Evolunity/Defines/" + Define.DEVELOPMENT + "/Add")]
		public static void AddDevelopmentDefine()
		{
			Define.Set(Define.DEVELOPMENT, true);
		}
#endif

		[MenuItem("Edit/Clear AssetBundles Cache", priority = 270)]
		[MenuItem("Tools/Evolunity/Clear AssetBundles Cache")]
		public static void ClearAssetBundlesCache()
		{
			Caching.ClearCache();
			EditorUtility.DisplayDialog("Clear AssetBundles Cache", "AssetBundles cache was successfully cleared",
				"OK");
		}

		[MenuItem("Assets/Reserialize All Assets")]
		[MenuItem("Tools/Evolunity/Reserialize All Assets")]
		public static void ReserializeAllAssets()
		{
			AssetDatabase.ForceReserializeAssets();

			Debug.Log("All assets were successfully reserialized.");
		}
	}
}