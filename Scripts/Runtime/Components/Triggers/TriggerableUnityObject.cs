// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[Serializable]
	public struct TriggerableUnityObject : ITriggerable
	{
		[SerializeField, InterfaceType(typeof(ITriggerable))]
		private UnityEngine.Object _object;

		public void Trigger()
		{
			// ReSharper disable once SuspiciousTypeConversion.Global
			((ITriggerable)_object).Trigger();
		}
	}
}