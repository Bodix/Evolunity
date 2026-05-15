// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Components.Animations
{
	public interface IShowHideAnimations
	{
		IAnimation ShowAnimation { get; }
		IAnimation HideAnimation { get; }

		void PlayShow(Action onStart = null, Action onComplete = null);
		void PlayHide(Action onStart = null, Action onComplete = null);
	}
}