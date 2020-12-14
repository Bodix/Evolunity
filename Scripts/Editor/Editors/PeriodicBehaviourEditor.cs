// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using Evolunity.Components;
using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Editor.Editors
{
    [CustomEditor(typeof(PeriodicBehaviour), true)]
    public class PeriodicBehaviourEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            PeriodicBehaviour periodicBehaviour = (PeriodicBehaviour) target;
            if (periodicBehaviour.GetType() == typeof(PeriodicBehaviour))
            {
                base.OnInspectorGUI();
            }
            else
            {
                using (new EditorGUI.DisabledGroupScope(true))
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("m_Script"));

                List<string> excludingProperties = new List<string>
                {
                    "m_Script",
                    nameof(periodicBehaviour.Period).ToLower()
                };
                if (!periodicBehaviour.DrawPeriodEventInInspector)
                    excludingProperties.Add(nameof(periodicBehaviour.OnPeriodCallback));
                DrawPropertiesExcluding(serializedObject, excludingProperties.ToArray());

                if (periodicBehaviour.DrawPeriodFieldInInspector)
                    EditorGUILayout.PropertyField(
                        serializedObject.FindProperty(nameof(periodicBehaviour.Period).ToLower()));
            }

            if (periodicBehaviour.DrawPeriodProgressInInspector)
                EditorGUI.ProgressBar(EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight),
                    periodicBehaviour.PeriodProgress,
                    $"{periodicBehaviour.PeriodProgress * periodicBehaviour.Period:F}sec / "
                    + $"{periodicBehaviour.Period:F}sec ({periodicBehaviour.PeriodProgress * 100f:##0}%)");

            // Update progress bar in every frame.
            if (Application.isPlaying && periodicBehaviour.enabled)
                Repaint();

            serializedObject.ApplyModifiedProperties();
        }
    }
}