// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Bodix.Evolunity.Attributes;
using NaughtyAttributes;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Triggers/Trigger")]
	public class Trigger : AbstractTrigger
	{
		[SerializeReference, TypeSelector, HideIf(nameof(HideTriggerableInInspector))]
		protected ITriggerable _triggerable;

		protected virtual bool HideTriggerableInInspector => false;

		protected override void EnterTrigger(Collider obj)
		{
			InvokeTrigger();
		}

		protected virtual void InvokeTrigger()
		{
			_triggerable.Trigger();

			InvokeTriggeredEvent();
		}
	}
}