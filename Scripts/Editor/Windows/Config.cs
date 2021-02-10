// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Windows
{
    // In Unity 2018.4.30f1 we can't create a new instance of a class inherited from UnityEngine.Object.
    //
    // If we create it via constructor or Activator.CreateInstance<T>():
    // ArgumentException: Object at index 0 is null
    //
    // If we create it via ObjectFactory.CreateInstance<T>()
    // ArgumentException: 'type' parameter is abstract and can't be used in the ObjectFactory : DerivedObject
    //
    // https://forum.unity.com/threads/argumentexception-type-parameter-is-abstract-and-cant-be-used-in-the-objectfactory.1052954/
    // https://forum.unity.com/threads/how-do-you-create-a-new-object-instance-from-a-type-that-is-derived-from-unityengine-object.730037/
    //
    // So we will use a custom editor layout instead of SerializedProperty.
    public class Config : EditorWindow
    {
        private const int MaxDisplayRefreshRate = 240;

        [MenuItem("Window/General/Config")]
        private static void ShowWindow()
        {
            Config window = GetWindow<Config>();
            window.titleContent = new GUIContent("Config");
            window.Show();
        }

        private void OnGUI()
        {
            Application.targetFrameRate = EditorGUILayout.IntSlider(
                "Target frame rate", Application.targetFrameRate, 0, MaxDisplayRefreshRate);
        }
    }
}