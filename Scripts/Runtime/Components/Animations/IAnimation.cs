// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Components.Animations
{
	public interface IAnimation
	{
		void Play(Action onStart = null, Action onComplete = null);

		// TODO:
		// Add Stop() method. [#design]
		// void Stop();
	}
}