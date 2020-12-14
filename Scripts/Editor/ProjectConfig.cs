// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor
{
    public class ProjectConfig : EditorWindow
    {
        [MenuItem("Window/General/Project Config")]
        private static void ShowWindow()
        {
            ProjectConfig window = GetWindow<ProjectConfig>();
            window.titleContent = new GUIContent("Project Config");
            window.Show();
        }

        private void OnGUI()
        {
            
        }
    }
}