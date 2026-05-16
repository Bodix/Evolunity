// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using Bodix.Evolunity.Attributes;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	// TODO:
	// - UiConditionalButton
	// - UiQuantitySelector
	// - UiPopup / UiDialog
	// - UiToast / UiNotification
	// - UiToggle
	// - UiInteractionBlocker
	// - UiTooltip
	// - UiWindow (not UiScreen, UiScreen can be only one at time, UiWindow can be multiple)
	// - UiSlot / UiItemCard ???
	// - UiBadge ???
	// [#design]

	public class UiElement : MonoBehaviour
	{
		[SerializeReference, TypeSelector]
		public IShowHideAnimations Animations;
		// We should force this message to prevent possible errors, due to non-obviousness.
		// [Foldout("Debug")]
		// public bool LogWarnings = true;

		public event Action Showing;
		public event Action Hiding;
		public event Action Shown;
		public event Action Hidden;

		public virtual bool IsShown => State == UiElementState.Shown;
		public virtual bool IsHidden => State == UiElementState.Hidden;
		public IAnimation ShowAnimation => Animations?.ShowAnimation;
		public IAnimation HideAnimation => Animations?.HideAnimation;
		public UiElementState State { get; private set; } = UiElementState.Hidden;
		public bool IsInTransition => State == UiElementState.Showing || State == UiElementState.Hiding;
		public bool IsShownAndActiveInHierarchy => IsShown && gameObject.activeInHierarchy;

		protected virtual void Awake()
		{
			State = gameObject.activeSelf
				? UiElementState.Shown
				: UiElementState.Hidden;
		}

		[ContextMenu(nameof(Show))]
		public void Show()
		{
			Show(false, null);
		}

		[ContextMenu(nameof(Hide))]
		public void Hide()
		{
			Hide(false, null);
		}

		[ContextMenu(nameof(ShowInstantly))]
		public void ShowInstantly()
		{
			Show(true, null);
		}

		[ContextMenu(nameof(HideInstantly))]
		public void HideInstantly()
		{
			Hide(true, null);
		}

		public void Show(Action onComplete)
		{
			Show(false, onComplete);
		}

		public void Hide(Action onComplete)
		{
			Hide(false, onComplete);
		}

		protected virtual void Show(bool instantly, Action onComplete)
		{
			if (State == UiElementState.Showing || State == UiElementState.Shown /*&& LogWarnings*/)
			{
				Debug.LogWarning("Trying to show the UiElement when it is already shown. " +
					"Animation and callbacks won't be invoked");

				return;
			}

			State = UiElementState.Showing;

			if (ShowAnimation == null || instantly)
			{
				OnShowAnimationStart();
				OnShowAnimationComplete(onComplete);
			}
			else
			{
				ShowAnimation.Play(
					OnShowAnimationStart,
					() => OnShowAnimationComplete(onComplete));
			}
		}

		protected virtual void Hide(bool instantly, Action onComplete)
		{
			if (State == UiElementState.Hiding || State == UiElementState.Hidden /*&& LogWarnings*/)
			{
				Debug.LogWarning("Trying to hide the UiElement when it is already hidden. " +
					"Animation and callbacks won't be invoked");

				return;
			}

			State = UiElementState.Hiding;

			if (HideAnimation == null || instantly)
			{
				OnHideAnimationStart();
				OnHideAnimationComplete(onComplete);
			}
			else
			{
				HideAnimation.Play(
					OnHideAnimationStart,
					() => OnHideAnimationComplete(onComplete));
			}
		}

		protected virtual void OnShowAnimationStart()
		{
			gameObject.SetActive(true);

			Showing?.Invoke();
		}

		protected virtual void OnShowAnimationComplete(Action onComplete)
		{
			State = UiElementState.Shown;

			onComplete?.Invoke();
			Shown?.Invoke();
		}

		protected virtual void OnHideAnimationStart()
		{
			Hiding?.Invoke();
		}

		protected virtual void OnHideAnimationComplete(Action onComplete)
		{
			State = UiElementState.Hidden;
			gameObject.SetActive(false);

			onComplete?.Invoke();
			Hidden?.Invoke();
		}
	}
}