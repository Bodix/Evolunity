using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public abstract class RangeSensor<TTarget> : PeriodicBehaviour where TTarget : Component
	{
		public float Range = 10;

		private readonly HashSet<TTarget> _visibleTargets = new HashSet<TTarget>();

		public event Action<TTarget> TargetFound;
		public event Action<TTarget> TargetLost;

		public IReadOnlyCollection<TTarget> VisibleTargets => _visibleTargets;
		protected abstract IEnumerable<TTarget> GetPossibleTargets { get; }

		protected override void OnPeriod()
		{
			Check();
		}

		public void Check()
		{
			foreach (TTarget target in GetPossibleTargets)
				if (target && (target.transform.position - transform.position).magnitude <= Range)
					if (_visibleTargets.Add(target))
						TargetFound?.Invoke(target);

			foreach (TTarget target in _visibleTargets.ToArray())
				if (!target || (target.transform.position - transform.position).magnitude > Range)
					if (_visibleTargets.Remove(target))
						TargetLost?.Invoke(target);
		}
	}
}