// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using NaughtyAttributes;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public class TransformCopier : MonoBehaviour
	{
		[ShowIf(nameof(ShowTargetField))]
		[SerializeField] private Transform _target;
		public TransformCopierUpdateType UpdateType = TransformCopierUpdateType.LateUpdate;

		[BoxGroup("Position")]
		public bool CopyPosition = false;
		[BoxGroup("Position")]
		[EnableIf(nameof(CopyPosition))]
		public AxisMask PositionAxes = new AxisMask(true, true, true);

		[BoxGroup("Rotation")]
		public bool CopyRotation = false;
		[BoxGroup("Rotation")]
		[EnableIf(nameof(CopyRotation))]
		public AxisMask RotationAxes = new AxisMask(true, true, true);

		[BoxGroup("Scale")]
		public bool CopyScale = false;
		[BoxGroup("Scale")]
		[EnableIf(nameof(CopyScale))]
		public AxisMask ScaleAxes = new AxisMask(true, true, true);

		// Property for NaughtyAttributes to check if the target field should be shown in the Inspector.
		// Child classes can override this to return false if they strictly use Dependency Injection.
		protected virtual bool ShowTargetField => true;

		public void SetTarget(Transform newTarget)
		{
			_target = newTarget;
		}

		private void Update()
		{
			if (UpdateType == TransformCopierUpdateType.Update)
				ApplyTransform();
		}

		private void LateUpdate()
		{
			if (UpdateType == TransformCopierUpdateType.LateUpdate)
				ApplyTransform();
		}

		private void FixedUpdate()
		{
			if (UpdateType == TransformCopierUpdateType.FixedUpdate)
				ApplyTransform();
		}

		private void ApplyTransform()
		{
			if (_target == null)
				return;

			if (CopyPosition)
			{
				Vector3 pos = transform.position;
				Vector3 targetPos = _target.position;

				pos.x = PositionAxes.x ? targetPos.x : pos.x;
				pos.y = PositionAxes.y ? targetPos.y : pos.y;
				pos.z = PositionAxes.z ? targetPos.z : pos.z;

				transform.position = pos;
			}

			if (CopyRotation)
			{
				Vector3 rot = transform.eulerAngles;
				Vector3 targetRot = _target.eulerAngles;

				rot.x = RotationAxes.x ? targetRot.x : rot.x;
				rot.y = RotationAxes.y ? targetRot.y : rot.y;
				rot.z = RotationAxes.z ? targetRot.z : rot.z;

				transform.eulerAngles = rot;
			}

			if (CopyScale)
			{
				Vector3 scale = transform.localScale;
				Vector3 targetScale = _target.localScale;

				scale.x = ScaleAxes.x ? targetScale.x : scale.x;
				scale.y = ScaleAxes.y ? targetScale.y : scale.y;
				scale.z = ScaleAxes.z ? targetScale.z : scale.z;

				transform.localScale = scale;
			}
		}

		public enum TransformCopierUpdateType
		{
			Update,
			LateUpdate,
			FixedUpdate
		}

		[Serializable]
		public struct AxisMask
		{
			public bool x;
			public bool y;
			public bool z;

			public AxisMask(bool x, bool y, bool z)
			{
				this.x = x;
				this.y = y;
				this.z = z;
			}
		}
	}
}