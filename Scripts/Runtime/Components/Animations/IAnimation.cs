// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Bodix.Evolunity.Components
{
	public interface IAnimation
	{
		// TODO:
		// Add "CancellationToken cancellationToken = default" to parameters.
		// Don't forget to cancel this token in OnDestroy. 
		// [#design]
		void Play(Action onStart = null, Action onComplete = null);

		// TODO:
		// Add Stop() method. [#design]

		// void Stop();
	}
}