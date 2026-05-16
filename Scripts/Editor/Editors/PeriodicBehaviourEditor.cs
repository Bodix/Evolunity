// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using Bodix.Evolunity.Components;
using NaughtyAttributes.Editor;
using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Editor.Editors
{
	[CustomEditor(typeof(PeriodicBehaviour), true)]
	public class PeriodicBehaviourEditor : NaughtyInspector
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			PeriodicBehaviour periodicBehaviour = (PeriodicBehaviour)target;
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

				// Replaced DrawPropertiesExcluding with manual property iteration
				// to ensure NaughtyAttributes logic is applied to each field.
				SerializedProperty iterator = serializedObject.GetIterator();
				bool enterChildren = true;
				while (iterator.NextVisible(enterChildren))
				{
					enterChildren = false;
					if (excludingProperties.Contains(iterator.name))
						continue;

					NaughtyEditorGUI.PropertyField_Layout(iterator, true);
				}

				if (periodicBehaviour.DrawPeriodFieldInInspector)
				{
					SerializedProperty periodProp = serializedObject.FindProperty(nameof(periodicBehaviour.Period).ToLower());
					NaughtyEditorGUI.PropertyField_Layout(periodProp, true);
				}
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