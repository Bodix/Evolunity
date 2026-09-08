using System;

namespace Bodix.Evolunity.Patterns
{
	public interface IEventBus
	{
		void Subscribe<T>(Action<T> handler);
		void Unsubscribe<T>(Action<T> handler);
	}
}