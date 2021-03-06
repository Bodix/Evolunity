// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEditor;
using UnityEngine;
using Evolutex.Evolunity.Editor.Utilities;

namespace Evolutex.Evolunity.Editor
{
    public static class MenuItems
    {
        [MenuItem("Edit/Toggle Inspector Lock", priority = 143)]
        [MenuItem("Tools/Evolunity/Toggle Inspector Lock %l")]
        public static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }

        [MenuItem("Edit/Clear AssetBundles Cache", priority = 270)]
        [MenuItem("Tools/Evolunity/Clear AssetBundles Cache")]
        private static void ClearAssetBundlesCache()
        {
            Caching.ClearCache();
            EditorUtility.DisplayDialog("Clear AssetBundles Cache", "AssetBundles cache was successfully cleared", "OK");
        }

        [MenuItem("Assets/Open Persistent Data Folder", priority = 111)]
        [MenuItem("Tools/Evolunity/Open Persistent Data Folder")]
        private static void OpenPersistentDataFolder()
        {
            FileManager.Open(Application.persistentDataPath);
        }
        
#if DEVELOPMENT
        [MenuItem("Edit/Defines/" + Define.DEVELOPMENT + "/Remove", priority = 269)]
        [MenuItem("Tools/Evolunity/Defines/" + Define.DEVELOPMENT + "/Remove")]
        private static void RemoveDevelopmentDefine()
        {
            Define.Set(Define.DEVELOPMENT, false);
        }
#else
        [MenuItem("Edit/Defines/" + Define.DEVELOPMENT + "/Add", priority = 269)]
        [MenuItem("Tools/Evolunity/Defines/" + Define.DEVELOPMENT + "/Add")]
        private static void AddDevelopmentDefine()
        {
            Define.Set(Define.DEVELOPMENT, true);
        }
#endif
        
        [MenuItem("Assets/Take Screenshot")]
        [MenuItem("Tools/Evolunity/Take Screenshot &s")]
        private static void TakeScreenshot()
        {
            CameraScreenshot.Take();
        }
    }
}