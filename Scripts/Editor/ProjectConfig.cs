/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

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