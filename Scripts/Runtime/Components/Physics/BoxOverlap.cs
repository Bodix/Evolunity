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
    // 1. Create SphereOverlap.
    // 2. Handle QueryTriggerInteraction option.
    // 3. Make read only if controlled from other script (e. g. from ConicalOverlap).

    [AddComponentMenu("Evolunity/Physics/Box Overlap")]
    public class BoxOverlap : MonoBehaviour
    {
        public OverlapPoseOrigin PoseOrigin;
        [ShowIf(nameof(IsOverridePoseTransform))]
        [SerializeField]
        private Transform _poseTransform;
        [ShowIf(nameof(IsCustomPose))]
        [SerializeField]
        private Vector3 _center;
        [ShowIf(nameof(IsCustomPose))]
        [SerializeField]
        private Vector3 _rotation;
        public Vector3 HalfExtents = Vector3.one;
        public LayerMask Layers = UnityEngine.Physics.AllLayers;

        [Header("Gizmos")]
        [SerializeField]
        public bool GizmosEnabled = true;
        [SerializeField]
        public Color GizmosDefaultColor = Color.white;
        [SerializeField]
        public Color GizmosExecutedColor = Color.green;
        [SerializeField]
        public float GizmosFadeTime = 2f;

        private readonly Collider[] _collidersBuffer = new Collider[512];
        private float _gizmosColorValue;

        public Vector3 Center => Pose.position;
        public Quaternion Rotation => Pose.rotation;
        public Pose Pose
        {
            get
            {
                switch (PoseOrigin)
                {
                    case OverlapPoseOrigin.SelfTransform:
                        return new Pose(transform.position, transform.rotation);
                    case OverlapPoseOrigin.OverrideTransform:
                        return _poseTransform
                            ? new Pose(_poseTransform.position, _poseTransform.rotation)
                            : new Pose(transform.position, transform.rotation);
                    case OverlapPoseOrigin.Custom:
                        return new Pose(_center, Quaternion.Euler(_rotation));
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        private bool IsOverridePoseTransform => PoseOrigin == OverlapPoseOrigin.OverrideTransform;
        private bool IsCustomPose => PoseOrigin == OverlapPoseOrigin.Custom;

        [Button("Execute")]
        public void ExecuteAndLog()
        {
            Execute(out IEnumerable<Collider> colliders);

            Debug.Log(colliders.AsString(x => x.name), this);
        }

        public int Execute(out IEnumerable<Collider> colliders)
        {
            int collidersCount = UnityEngine.Physics.OverlapBoxNonAlloc(
                Center, HalfExtents, _collidersBuffer, Rotation, Layers);
            colliders = _collidersBuffer.Take(collidersCount).Where(x => x != null);

            _gizmosColorValue = 1;

            return collidersCount;
        }

        public void SetPoseTransform(Transform t, bool changePoseOrigin = true)
        {
            _poseTransform = t;

            if (changePoseOrigin)
                PoseOrigin = OverlapPoseOrigin.OverrideTransform;
        }

        public void SetCustomPose(Vector3 position, Quaternion rotation, bool changePoseOrigin = true)
        {
            _center = position;
            _rotation = rotation.eulerAngles;

            if (changePoseOrigin)
                PoseOrigin = OverlapPoseOrigin.Custom;
        }

        private void OnDrawGizmos()
        {
            if (!GizmosEnabled)
                return;

            Gizmos.matrix = Matrix4x4.TRS(Center, Rotation, Vector3.one);
            using (new GizmosColorScope(Color.Lerp(GizmosDefaultColor, GizmosExecutedColor,
                Mathf.SmoothDamp(_gizmosColorValue, 0, ref _gizmosColorValue, GizmosFadeTime))))
                Gizmos.DrawCube(Vector3.zero, HalfExtents * 2);
        }
    }
}