// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Windows
{
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