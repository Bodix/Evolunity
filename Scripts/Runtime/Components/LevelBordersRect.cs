using System.Collections.Generic;
using System.Linq;
using Evolutex.Evolunity.Extensions;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
	/// <summary>
	/// Useful to use together with <see cref="Unity.Cinemachine.CinemachineTargetGroup"/>.
	/// </summary>
	public class LevelBordersRect : MonoBehaviour
	{
		[Header("Points")]
		[SerializeField]
		private Transform _topLeftCorner;
		[SerializeField]
		private Transform _topRightCorner;
		[SerializeField]
		private Transform _bottomLeftCorner;
		[SerializeField]
		private Transform _bottomRightCorner;

		[Header("Offsets")]
		[SerializeField]
		private float _leftOffset = 1f;
		[SerializeField]
		private float _rightOffset = 1f;
		[SerializeField]
		private float _topOffset = 1f;
		[SerializeField]
		private float _bottomOffset = 1f;

		[Header("Gizmos")]
		[SerializeField]
		private Color _gizmosColor = Color.magenta;

		public Transform TopLeftCorner => _topLeftCorner;
		public Transform TopRightCorner => _topRightCorner;
		public Transform BottomLeftCorner => _bottomLeftCorner;
		public Transform BottomRightCorner => _bottomRightCorner;

		public void EncapsulateFromScratch(List<LevelBordersRectEntry> objects)
		{
			if (objects == null || objects.Count == 0)
			{
				Debug.LogWarning(nameof(EncapsulateFromScratch) + ": no objects provided", this);

				return;
			}

			float minX = float.MaxValue;
			float maxX = float.MinValue;
			float minZ = float.MaxValue;
			float maxZ = float.MinValue;

			foreach (LevelBordersRectEntry obj in objects)
			{
				if (obj.Transform == null)
					continue;

				Vector3 pos = obj.Transform.position;
				float halfWidth = obj.Size.x * 0.5f;
				float halfDepth = obj.Size.y * 0.5f;

				minX = Mathf.Min(minX, pos.x - halfWidth);
				maxX = Mathf.Max(maxX, pos.x + halfWidth);
				minZ = Mathf.Min(minZ, pos.z - halfDepth);
				maxZ = Mathf.Max(maxZ, pos.z + halfDepth);
			}

			_topLeftCorner.position = new Vector3(minX - _leftOffset, 0, maxZ + _topOffset);
			_topRightCorner.position = new Vector3(maxX + _rightOffset, 0, maxZ + _topOffset);
			_bottomLeftCorner.position = new Vector3(minX - _leftOffset, 0, minZ - _bottomOffset);
			_bottomRightCorner.position = new Vector3(maxX + _rightOffset, 0, minZ - _bottomOffset);
		}

		public void EncapsulateFromScratch(Transform[] transforms, bool useTransformsGlobalScale = false)
		{
			List<LevelBordersRectEntry> list = transforms
				.Select(x => new LevelBordersRectEntry(x, useTransformsGlobalScale ? x.lossyScale.XZ() : Vector2.zero))
				.ToList();

			EncapsulateFromScratch(list);
		}

		public void EncapsulateAdditive(Transform transform, Vector2 size)
		{
			if (transform == null)
			{
				Debug.LogWarning(nameof(EncapsulateAdditive) + ": transform is null", this);

				return;
			}

			Vector3 pos = transform.position;
			float halfWidth = size.x * 0.5f;
			float halfDepth = size.y * 0.5f;

			float objLeft = pos.x - halfWidth;
			float objRight = pos.x + halfWidth;
			float objTop = pos.z + halfDepth;
			float objBottom = pos.z - halfDepth;

			float currentLeft = Mathf.Min(_topLeftCorner.position.x, _bottomLeftCorner.position.x);
			float currentRight = Mathf.Max(_topRightCorner.position.x, _bottomRightCorner.position.x);
			float currentTop = Mathf.Max(_topLeftCorner.position.z, _topRightCorner.position.z);
			float currentBottom = Mathf.Min(_bottomLeftCorner.position.z, _bottomRightCorner.position.z);

			bool changed = false;
			if (objLeft < currentLeft)
			{
				currentLeft = objLeft - _leftOffset;
				changed = true;
			}

			if (objRight > currentRight)
			{
				currentRight = objRight + _rightOffset;
				changed = true;
			}

			if (objTop > currentTop)
			{
				currentTop = objTop + _topOffset;
				changed = true;
			}

			if (objBottom < currentBottom)
			{
				currentBottom = objBottom - _bottomOffset;
				changed = true;
			}

			if (changed)
			{
				_topLeftCorner.position = new Vector3(currentLeft, 0, currentTop);
				_topRightCorner.position = new Vector3(currentRight, 0, currentTop);
				_bottomLeftCorner.position = new Vector3(currentLeft, 0, currentBottom);
				_bottomRightCorner.position = new Vector3(currentRight, 0, currentBottom);
			}
		}

		public void EncapsulateAdditive(Transform transform, bool useTransformGlobalScale = false)
		{
			EncapsulateAdditive(transform, useTransformGlobalScale ? transform.lossyScale.XZ() : Vector2.zero);
		}

		private void OnDrawGizmos()
		{
			if (!_topLeftCorner || !_topRightCorner || !_bottomLeftCorner || !_bottomRightCorner)
			{
				Debug.LogError("One or more level borders points are not assigned", this);

				return;
			}

			Gizmos.color = _gizmosColor;
			Gizmos.DrawLine(_topLeftCorner.position, _topRightCorner.position);
			Gizmos.DrawLine(_topRightCorner.position, _bottomRightCorner.position);
			Gizmos.DrawLine(_bottomRightCorner.position, _bottomLeftCorner.position);
			Gizmos.DrawLine(_bottomLeftCorner.position, _topLeftCorner.position);
		}
	}

	[System.Serializable]
	public struct LevelBordersRectEntry
	{
		public Transform Transform;
		public Vector2 Size;

		public LevelBordersRectEntry(Transform transform, Vector2 size)
		{
			Transform = transform;
			Size = size;
		}
	}
}