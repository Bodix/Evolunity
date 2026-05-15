using System;

namespace Evolutex.Evolunity.Components.Animations
{
	public interface IAnimation
	{
		void Play(Action onStart = null, Action onComplete = null);
	}
}