using NaughtyAttributes;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[ExecuteAlways]
	public class GlobalScaleConstraint : MonoBehaviour
	{
		public Vector3 TargetGlobalScale = Vector3.one;
		[SerializeField]
		[Tooltip("Update scale every frame. Enable this if the parent " +
			"scales dynamically during gameplay (e.g., animations).")]
		private bool updateEveryFrame = false;

		private void OnValidate()
		{
			UpdateScale();
		}

		private void Start()
		{
			UpdateScale();
		}

		private void LateUpdate()
		{
			if (!updateEveryFrame)
				return;

			UpdateScale();
		}

		[Button("Update Scale")]
		public void UpdateScale()
		{
			if (transform.parent == null)
			{
				UpdateScaleIfChanged(TargetGlobalScale);

				return;
			}

			Vector3 parentScale = transform.parent.lossyScale;

			// Prevent division by zero to avoid infinity values and errors.
			if (Mathf.Approximately(parentScale.x, 0f) ||
			    Mathf.Approximately(parentScale.y, 0f) ||
			    Mathf.Approximately(parentScale.z, 0f))
				return;

			Vector3 requiredLocalScale = new Vector3(
				TargetGlobalScale.x / parentScale.x,
				TargetGlobalScale.y / parentScale.y,
				TargetGlobalScale.z / parentScale.z
			);

			UpdateScaleIfChanged(requiredLocalScale);
		}

		private void UpdateScaleIfChanged(Vector3 newScale)
		{
			// Apply new scale only if it differs from the current one to prevent dirtying the transform.
			if (transform.localScale != newScale)
				transform.localScale = newScale;
		}
	}
}