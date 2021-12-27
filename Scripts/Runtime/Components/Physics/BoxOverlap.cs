// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using System.Linq;
using Evolutex.Evolunity.Extensions;
using Evolutex.Evolunity.Utilities.Gizmos;
using NaughtyAttributes;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Physics
{
    // TODO:
    // 1. Add set accessor to properties.
    // 2. Create SphereOverlap and CapsuleOverlap.
    // 3. Handle QueryTriggerInteraction option.

    [AddComponentMenu("Evolunity/Box Overlap")]
    public class BoxOverlap : MonoBehaviour
    {
        [SerializeField]
        private OverlapPoseOrigin _poseOrigin;
        [ShowIf(nameof(IsOverrideOriginTransform))]
        public Transform PoseTransform;
        [ShowIf(nameof(IsCustomPose))]
        [SerializeField]
        private Vector3 _center;
        public Vector3 HalfExtents = Vector3.one;
        [ShowIf(nameof(IsCustomPose))]
        [SerializeField]
        private Quaternion _orientation = Quaternion.identity;
        public LayerMask Layers = UnityEngine.Physics.AllLayers;

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

        public Vector3 Center => Pose.position;
        public Quaternion Orientation => Pose.rotation;
        public Pose Pose
        {
            get
            {
                switch (_poseOrigin)
                {
                    case OverlapPoseOrigin.SelfTransform:
                        return new Pose(transform.position, transform.rotation);
                    case OverlapPoseOrigin.OverrideTransform:
                        return PoseTransform
                            ? new Pose(PoseTransform.position, PoseTransform.rotation)
                            : new Pose(transform.position, transform.rotation);
                    case OverlapPoseOrigin.Custom:
                        return new Pose(_center, _orientation);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        private bool IsOverrideOriginTransform => _poseOrigin == OverlapPoseOrigin.OverrideTransform;
        private bool IsCustomPose => _poseOrigin == OverlapPoseOrigin.Custom;

        [Button("Execute")]
        private void Execute()
        {
            Execute(out IEnumerable<Collider> colliders);

            Debug.Log(colliders.AsString(x => x.name), this);
        }

        public int Execute(out IEnumerable<Collider> colliders)
        {
            int collidersCount = UnityEngine.Physics.OverlapBoxNonAlloc(
                Pose.position, HalfExtents, _collidersBuffer, Pose.rotation, Layers);
            colliders = _collidersBuffer.Take(collidersCount).Where(x => x != null);

            _gizmosColorValue = 1;

            return collidersCount;
        }

        private void OnDrawGizmos()
        {
            if (!GizmosEnabled)
                return;

            Gizmos.matrix = Matrix4x4.TRS(Pose.position, Pose.rotation, Vector3.one);
            using (new GizmosColorScope(Color.Lerp(GizmosDefaultColor, GizmosExecutedColor,
                Mathf.SmoothDamp(_gizmosColorValue, 0, ref _gizmosColorValue, GizmosFadeTime))))
                Gizmos.DrawCube(Vector3.zero, HalfExtents * 2);
        }

        private enum OverlapPoseOrigin
        {
            SelfTransform,
            OverrideTransform,
            Custom
        }
    }
}