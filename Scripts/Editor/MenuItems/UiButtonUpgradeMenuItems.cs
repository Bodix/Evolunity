// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Bodix.Evolunity.Components.UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Bodix.Evolunity.Editor
{
	public static class UiButtonUpgradeMenuItems
	{
		[MenuItem("CONTEXT/UiButton/Upgrade to Observable Button")]
		private static void UpgradeFromUiButton(MenuCommand command)
		{
			UiButton uiButton = (UiButton)command.context;

			if (uiButton != null)
				PerformUpgrade(uiButton.Button);
		}

		[MenuItem("CONTEXT/Button/Upgrade to Observable Button")]
		private static void UpgradeFromButton(MenuCommand command)
		{
			Button oldButton = (Button)command.context;

			if (oldButton != null)
				PerformUpgrade(oldButton);
		}

		/// <summary>
		/// Replaces the standard <see cref="Button"/> with an <see cref="ObservableButton"/>
		/// while preserving all data.
		/// </summary>
		private static void PerformUpgrade(Button oldButton)
		{
			if (oldButton == null || oldButton is ObservableButton)
			{
				Debug.LogWarning("Button is already upgraded or missing.");

				return;
			}

			GameObject targetGo = oldButton.gameObject;

			bool interactable = oldButton.interactable;
			Selectable.Transition transition = oldButton.transition;
			Graphic targetGraphic = oldButton.targetGraphic;
			ColorBlock colors = oldButton.colors;
			SpriteState spriteState = oldButton.spriteState;
			AnimationTriggers animationTriggers = oldButton.animationTriggers;
			Navigation navigation = oldButton.navigation;
			Button.ButtonClickedEvent onClick = oldButton.onClick;

			Undo.DestroyObjectImmediate(oldButton);

			ObservableButton newButton = Undo.AddComponent<ObservableButton>(targetGo);

			newButton.interactable = interactable;
			newButton.transition = transition;
			newButton.targetGraphic = targetGraphic;
			newButton.colors = colors;
			newButton.spriteState = spriteState;
			newButton.animationTriggers = animationTriggers;
			newButton.navigation = navigation;
			newButton.onClick = onClick;

			UiButton uiButton = targetGo.GetComponent<UiButton>();

			if (uiButton != null)
			{
				SerializedObject uiButtonObj = new SerializedObject(uiButton);
				SerializedProperty buttonProp = uiButtonObj.FindProperty("button");

				if (buttonProp != null)
				{
					buttonProp.objectReferenceValue = newButton;
					uiButtonObj.ApplyModifiedProperties();
				}
			}

			Debug.Log($"Successfully upgraded button on {targetGo.name}.", targetGo);
		}
	}
}