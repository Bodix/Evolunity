// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using Evolutex.Evolunity.Attributes;
using Evolutex.Evolunity.Components.Animations;
using UnityEngine;

namespace Evolutex.Evolunity.Components.UI
{
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

		public IAnimation ShowAnimation => Animations?.ShowAnimation;
		public IAnimation HideAnimation => Animations?.HideAnimation;
		public virtual bool IsShown => gameObject.activeSelf;
		public bool IsShownAndActiveInHierarchy => IsShown && gameObject.activeInHierarchy;

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
			if (IsShown /*&& LogWarnings*/)
			{
				Debug.LogWarning("Trying to show the UiElement when it is already shown. " +
					"Animation and callbacks won't be invoked");

				return;
			}

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
			if (!IsShown /*&& LogWarnings*/)
			{
				Debug.LogWarning("Trying to hide the UiElement when it is already hidden. " +
					"Animation and callbacks won't be invoked");

				return;
			}

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
			onComplete?.Invoke();
			Shown?.Invoke();
		}

		protected virtual void OnHideAnimationStart()
		{
			Hiding?.Invoke();
		}

		protected virtual void OnHideAnimationComplete(Action onComplete)
		{
			gameObject.SetActive(false);

			onComplete?.Invoke();
			Hidden?.Invoke();
		}
	}
}