// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Bodix.Evolunity.Services
{
	public interface IBackNavigationService
	{
		/// <summary>
		/// Invoked when there are no handlers left to consume the back action.
		/// </summary>
		event Action QuitRequested;

		void Register(IBackNavigationHandler handler);

		void Unregister(IBackNavigationHandler handler);
	}
}