// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.IO;
using UnityEditor;
using UnityEngine;
using Evolutex.Evolunity.Editor.Utilities;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Evolutex.Evolunity.Editor
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

        [MenuItem("Edit/Clear AssetBundles Cache", priority = 270)]
        [MenuItem("Tools/Evolunity/Clear AssetBundles Cache")]
        public static void ClearAssetBundlesCache()
        {
            Caching.ClearCache();
            EditorUtility.DisplayDialog("Clear AssetBundles Cache", "AssetBundles cache was successfully cleared",
                "OK");
        }

        [MenuItem("Assets/Open Persistent Data Folder", priority = 111)]
        [MenuItem("Tools/Evolunity/Open Persistent Data Folder")]
        public static void OpenPersistentDataFolder()
        {
            FileExplorer.Open(Application.persistentDataPath);
        }

        [MenuItem("Assets/Open Temporary Cache Folder", priority = 112)]
        [MenuItem("Tools/Evolunity/Open Temporary Cache Folder")]
        public static void OpenTemporaryCacheFolder()
        {
            FileExplorer.Open(Application.temporaryCachePath);
        }

        [MenuItem("Edit/Open/Open Editor Folder", priority = 268)]
        [MenuItem("Tools/Evolunity/Open Editor Folder")]
        public static void OpenEditorFolder()
        {
            FileExplorer.Open(EditorApplication.applicationPath);
        }

        [MenuItem("Edit/Open/Open Editor Logs Folder", priority = 268)]
        [MenuItem("Tools/Evolunity/Open Editor Logs Folder")]
        private static void OpenEditorLogsFolder()
        {
#if UNITY_EDITOR_OSX
			string rootFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string logsFolderPath = Path.Combine(rootFolderPath, "Library", "Logs", "Unity");
#elif UNITY_EDITOR_WIN
            string rootFolderPath = System.Environment.ExpandEnvironmentVariables("%localappdata%");
            string logsFolderPath = Path.Combine(rootFolderPath, "Unity", "Editor");
#endif

            FileExplorer.Open(logsFolderPath);
        }

        [MenuItem("Edit/Open/Open Asset Store Packages Folder", priority = 268)]
        [MenuItem("Tools/Evolunity/Open Asset Store Packages Folder")]
        private static void OpenAssetStorePackagesFolder()
        {
#if UNITY_EDITOR_OSX
            string rootFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string unityFolderPath = Path.Combine(rootFolderPath, "Library", "Unity");
#elif UNITY_EDITOR_WIN
            string rootFolderPath = System.Environment.ExpandEnvironmentVariables("%appdata%");
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

        [MenuItem("Assets/Take Screenshot")]
        [MenuItem("Tools/Evolunity/Take Screenshot &s")]
        public static void TakeScreenshot()
        {
            CameraScreenshot.Take();
        }

        /// <summary>
        /// https://gist.github.com/nicoplv/0ba7924abe82356d9bbcbf119c0a4c7f
        /// https://docs.unity3d.com/2017.1/Documentation/ScriptReference/SceneManagement.EditorSceneManager-playModeStartScene.html
        /// </summary>
        [InitializeOnLoad]
        public static class PlayModeStartScene
        {
            private const string EditorPrefsKey = nameof(EditorSceneManager.playModeStartScene);

            static PlayModeStartScene()
            {
                string scenePath = EditorPrefs.GetString(EditorPrefsKey);
                SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

                EditorSceneManager.playModeStartScene = sceneAsset;
            }

            [MenuItem("Edit/Play Mode Start Scene/Set Start Scene")]
            [MenuItem("Tools/Evolunity/Play Mode Start Scene/Set Start Scene")]
            public static void SetStartScene()
            {
                string scenePath = SceneManager.GetActiveScene().path;

                EditorSceneManager.playModeStartScene =
                    AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

                EditorPrefs.SetString(EditorPrefsKey, scenePath);
            }

            [MenuItem("Edit/Play Mode Start Scene/Unset Start Scene")]
            [MenuItem("Tools/Evolunity/Play Mode Start Scene/Unset Start Scene")]
            public static void UnsetStartScene()
            {
                EditorSceneManager.playModeStartScene = null;

                EditorPrefs.DeleteKey(EditorPrefsKey);
            }

            [MenuItem("Edit/Play Mode Start Scene/Unset Start Scene", true)]
            [MenuItem("Tools/Evolunity/Play Mode Start Scene/Unset Start Scene", true)]
            public static bool UnsetStartSceneValidate()
            {
                return EditorSceneManager.playModeStartScene != null;
            }
        }
    }
}