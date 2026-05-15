// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Components.Animations
{
	[Serializable]
	public struct TwoAnimationBehaviours : IShowHideAnimations
	{
		public AnimationBehaviour ShowBehaviour;
		public AnimationBehaviour HideBehaviour;

		public IAnimation ShowAnimation => ShowBehaviour;
		public IAnimation HideAnimation => HideBehaviour;

		public void PlayShow(Action onStart = null, Action onComplete = null)
		{
			ShowAnimation.Play(onStart, onComplete);
		}

		public void PlayHide(Action onStart = null, Action onComplete = null)
		{
			HideAnimation.Play(onStart, onComplete);
		}
	}
}