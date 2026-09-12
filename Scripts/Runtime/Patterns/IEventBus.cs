// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Bodix.Evolunity.Patterns
{
	public interface IEventBus
	{
		void Subscribe<T>(Action<T> handler);
		void Unsubscribe<T>(Action<T> handler);
		void Publish<T>(T message);
	}
}