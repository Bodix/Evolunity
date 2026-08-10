using System;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public abstract class DirectionSensor<TTarget> : PeriodicBehaviour where TTarget : Component
	{
		public event Action<Vector3> DirectionChanged;

		protected abstract TTarget Target { get; }
		public Vector3 Direction { get; private set; }
		public bool HasTarget { get; private set; }

		protected override void OnPeriod()
		{
			Check();
		}

		public void Check()
		{
			Vector3 newDirection = Target
				? (Target.transform.position - transform.position).normalized
				: Vector3.zero;
			HasTarget = Target;

			if (Direction != newDirection)
			{
				Direction = newDirection;

				DirectionChanged?.Invoke(Direction);
			}
		}
	}
}