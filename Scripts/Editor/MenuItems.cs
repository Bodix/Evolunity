// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor
{
    public class MenuItems
    {
        [MenuItem("Edit/Toggle Inspector Lock %l", priority = 143)]
        [MenuItem("Tools/Toolkit/Toggle Inspector Lock %l")]
        public static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }

#if DEVELOPMENT
        [MenuItem("Edit/Defines/" + Define.DEVELOPMENT + "/Remove", priority = 269)]
        [MenuItem("Tools/Toolkit/Defines/" + Define.DEVELOPMENT + "/Remove")]
        private static void RemoveDevelopmentDefine()
        {
            Define.Set(Define.DEVELOPMENT, false);
        }
#else
        [MenuItem("Edit/Defines/" + Define.DEVELOPMENT + "/Add", priority = 269)]
        [MenuItem("Tools/Toolkit/Defines/" + Define.DEVELOPMENT + "/Add")]
        private static void AddDevelopmentDefine()
        {
            Define.Set(Define.DEVELOPMENT, true);
        }
#endif

        [MenuItem("Edit/Clear AssetBundles Cache", priority = 270)]
        [MenuItem("Tools/Toolkit/Clear AssetBundles Cache")]
        private static void ClearAssetBundlesCache()
        {
            Caching.ClearCache();
            EditorUtility.DisplayDialog("Clear AssetBundles Cache", "AssetBundles cache was successfully cleared", "OK");
        }

        [MenuItem("Assets/Open Persistent Data Folder", priority = 111)]
        [MenuItem("Tools/Toolkit/Open Persistent Data Folder")]
        private static void OpenPersistentDataFolder()
        {
            OpenInFileManager.Open(Application.persistentDataPath);
        }

        [MenuItem("Assets/Take Screenshot &s")]
        [MenuItem("Tools/Toolkit/Take Screenshot &s")]
        private static void TakeScreenshot()
        {
            CameraScreenshot.Take();
        }
    }
}