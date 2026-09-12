// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bodix.Evolunity.Patterns
{
	/// <summary>
	/// In-memory event bus. Subscribe, unsubscribe and nested publish are safe during dispatch.
	/// Thread-safe for multi-threaded access.
	/// An exception in one handler does not stop the others.
	/// </summary>
	public sealed class EventBus : IEventBus
	{
		private readonly Dictionary<Type, Delegate> _handlers = new Dictionary<Type, Delegate>();
		private readonly object _lock = new object();

		public void Subscribe<T>(Action<T> handler)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));

			lock (_lock)
			{
				Type eventType = typeof(T);

				if (_handlers.TryGetValue(eventType, out Delegate currentDelegate))
					_handlers[eventType] = Delegate.Combine(currentDelegate, handler);
				else
					_handlers[eventType] = handler;
			}
		}

		public void Unsubscribe<T>(Action<T> handler)
		{
			if (handler == null)
				throw new ArgumentNullException(nameof(handler));

			lock (_lock)
			{
				Type eventType = typeof(T);

				if (_handlers.TryGetValue(eventType, out Delegate currentDelegate))
				{
					Delegate newDelegate = Delegate.Remove(currentDelegate, handler);

					if (newDelegate == null)
						_handlers.Remove(eventType);
					else
						_handlers[eventType] = newDelegate;
				}
			}
		}

		public void Publish<T>(T message)
		{
			Delegate handlersToInvoke;

			lock (_lock)
			{
				if (!_handlers.TryGetValue(typeof(T), out handlersToInvoke))
					return;
			}

			// GetInvocationList returns an array of delegates that is safe to iterate over,
			// even if the original delegate chain is modified during iteration.
			Delegate[] invocationList = handlersToInvoke.GetInvocationList();

			for (int i = 0; i < invocationList.Length; i++)
			{
				try
				{
					((Action<T>)invocationList[i]).Invoke(message);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}

		public void Clear()
		{
			lock (_lock)
				_handlers.Clear();
		}
	}
}