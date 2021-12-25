using System.Collections.Generic;
using System.Linq;
using Evolutex.Evolunity.Extensions;
using Evolutex.Evolunity.Utilities.Gizmos;
using NaughtyAttributes;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Overlaps
{
    // TODO:
    // 1. Create SphereOverlap and CapsuleOverlap.
    // 2. Handle QueryTriggerInteraction option.

    public class BoxOverlap : MonoBehaviour
    {
        [SerializeField]
        private OverlapOriginTransform _originTransform;
        [ShowIf(nameof(OverrideOrigin))]
        public Transform Origin;
        public Vector3 HalfExtents = Vector3.one;
        public LayerMask Layers = Physics.AllLayers;

        [Header("Gizmos")]
        [SerializeField]
        protected bool GizmosEnabled = true;
        [SerializeField]
        private Color GizmosDefaultColor = Color.white;
        [SerializeField]
        private Color GizmosExecutedColor = Color.green;
        [SerializeField]
        private float GizmosFadeTime = 2f;

        private readonly Collider[] _collidersBuffer = new Collider[512];
        private float _gizmosColorValue;

        private Transform GetOrigin => Origin ? Origin : transform;
        private bool OverrideOrigin => _originTransform == OverlapOriginTransform.Override;

        [Button("Execute")]
        public void Execute()
        {
            Execute(out IEnumerable<Collider> colliders);

            Debug.Log(colliders.AsString(x => x.name), this);
        }

        public int Execute(out IEnumerable<Collider> colliders)
        {
            int collidersCount = Physics.OverlapBoxNonAlloc(
                GetOrigin.position, HalfExtents, _collidersBuffer, GetOrigin.rotation, Layers);
            colliders = _collidersBuffer.Take(collidersCount).Where(x => x != null);

            _gizmosColorValue = 1;

            return collidersCount;
        }

        private void OnDrawGizmos()
        {
            if (!GizmosEnabled)
                return;

            using (new GizmosColorScope(Color.Lerp(GizmosDefaultColor, GizmosExecutedColor,
                Mathf.SmoothDamp(_gizmosColorValue, 0, ref _gizmosColorValue, GizmosFadeTime))))
                Gizmos.DrawCube(GetOrigin.transform.position, HalfExtents * 2);
        }

        private enum OverlapOriginTransform
        {
            Self,
            Override
        }
    }
}