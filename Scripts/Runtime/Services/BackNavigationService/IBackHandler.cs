// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

namespace Bodix.Evolunity.Services
{
	/// <summary>
	/// Defines an interface for handling back navigation events.
	/// </summary>
	public interface IBackHandler
	{
		/// <summary>
		/// Invoked when the back action is performed.
		/// Returns true if the action was consumed, stopping further propagation.
		/// </summary>
		bool OnBackPressed();
	}
}