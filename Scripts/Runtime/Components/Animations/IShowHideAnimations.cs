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