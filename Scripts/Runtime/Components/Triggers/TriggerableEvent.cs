// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Bodix.Evolunity.Components
{
	[Serializable]
	public struct TriggerableEvent : ITriggerable
	{
		[SerializeField, FormerlySerializedAs("Event")]
		private UnityEvent _event;

		public TriggerableEvent(UnityAction action)
		{
			UnityEvent @event = new UnityEvent();
			@event.AddListener(action);
			_event = @event;
		}

		public void Trigger()
		{
			_event.Invoke();
		}
	}
}