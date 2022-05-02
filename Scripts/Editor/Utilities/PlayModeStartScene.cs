using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Evolutex.Evolunity.Editor.Utilities
{
    /// <summary>
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