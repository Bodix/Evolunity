// Evolunity for Unity
// Copyright © 2026 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using Bodix.Evolunity.Attributes;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	public enum FoldState
	{
		Collapsed,
		Collapsing,
		Expanded,
		Expanding
	}

	public class UiFoldable : MonoBehaviour
	{
		[SerializeReference, TypeSelector]
		public IShowHideAnimations Animations;

		public event Action Expanding;
		public event Action Collapsing;
		public event Action Expanded;
		public event Action Collapsed;

		public FoldState State { get; private set; } = FoldState.Collapsed;

		public bool IsExpanded => State == FoldState.Expanded;
		public bool IsCollapsed => State == FoldState.Collapsed;
		public bool IsInTransition => State == FoldState.Expanding || State == FoldState.Collapsing;

		public IAnimation ExpandAnimation => Animations?.ShowAnimation;
		public IAnimation CollapseAnimation => Animations?.HideAnimation;

		protected virtual void Awake()
		{
			State = FoldState.Collapsed;
		}

		[ContextMenu(nameof(Toggle))]
		public void Toggle()
		{
			if (IsExpanded || State == FoldState.Expanding)
				Collapse();
			else
				Expand();
		}

		[ContextMenu(nameof(Expand))]
		public void Expand()
		{
			Expand(false, null);
		}

		[ContextMenu(nameof(Collapse))]
		public void Collapse()
		{
			Collapse(false, null);
		}

		public void Expand(Action onComplete)
		{
			Expand(false, onComplete);
		}

		public void Collapse(Action onComplete)
		{
			Collapse(false, onComplete);
		}

		protected virtual void Expand(bool instantly, Action onComplete)
		{
			if (State == FoldState.Expanding || State == FoldState.Expanded)
			{
				Debug.LogWarning("Trying to expand when it is already expanded or expanding. " +
					"Animation and callbacks won't be invoked.");

				return;
			}

			State = FoldState.Expanding;

			if (ExpandAnimation == null || instantly)
			{
				OnExpandAnimationStart();
				OnExpandAnimationComplete(onComplete);
			}
			else
			{
				ExpandAnimation.Play(
					OnExpandAnimationStart,
					() => OnExpandAnimationComplete(onComplete));
			}
		}

		protected virtual void Collapse(bool instantly, Action onComplete)
		{
			if (State == FoldState.Collapsing || State == FoldState.Collapsed)
			{
				Debug.LogWarning("Trying to collapse when it is already collapsed or collapsing. " +
					"Animation and callbacks won't be invoked.");

				return;
			}

			State = FoldState.Collapsing;

			if (CollapseAnimation == null || instantly)
			{
				OnCollapseAnimationStart();
				OnCollapseAnimationComplete(onComplete);
			}
			else
			{
				CollapseAnimation.Play(
					OnCollapseAnimationStart,
					() => OnCollapseAnimationComplete(onComplete));
			}
		}

		protected virtual void OnExpandAnimationStart()
		{
			if (!gameObject.activeSelf)
				gameObject.SetActive(true);

			Expanding?.Invoke();
		}

		protected virtual void OnExpandAnimationComplete(Action onComplete)
		{
			State = FoldState.Expanded;

			onComplete?.Invoke();
			Expanded?.Invoke();
		}

		protected virtual void OnCollapseAnimationStart()
		{
			Collapsing?.Invoke();
		}

		protected virtual void OnCollapseAnimationComplete(Action onComplete)
		{
			State = FoldState.Collapsed;

			onComplete?.Invoke();
			Collapsed?.Invoke();
		}
	}
}