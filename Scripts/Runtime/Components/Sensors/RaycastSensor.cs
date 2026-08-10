using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public class RaycastSensor : PeriodicBehaviour
	{
		public float SensorLength = 10f;
		public Vector3 LocalDirection = Vector3.forward;
		public LayerMask LayerMask = 0;

		public bool IsHit { get; private set; }

		protected override void OnPeriod()
		{
			Check();
		}

		public void Check()
		{
			Vector3 worldDirection = transform.TransformDirection(LocalDirection.normalized);

			IsHit = Physics.Raycast(transform.position, worldDirection, SensorLength, LayerMask);
		}
	}
}