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

    [RequireComponent(typeof(BoxOverlap))]
    [AddComponentMenu("Evolunity/Conical Overlap")]
    public class ConicalOverlap : MonoBehaviour
    {
        [InfoBox("ConicalOverlap uses BoxOverlap. It will override its settings")]
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
        public float Distance = 1;
        public float Angle = 90;
        public LayerMask Layers = UnityEngine.Physics.AllLayers;
        [SerializeField, HideInInspector]
        private BoxOverlap _boxOverlap;

        [Header("Gizmos")]
        [SerializeField]
        public bool GizmosEnabled = true;
        [SerializeField]
        public Color GizmosDefaultColor = Color.white;
        [SerializeField]
        public Color GizmosExecutedColor = Color.green;
        [SerializeField]
        public float GizmosFadeTime = 2f;

        private float _gizmosColorValue;

        public Vector3 ApexPosition => Pose.position;
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

        private void OnValidate()
        {
            _boxOverlap = GetComponent<BoxOverlap>();
            SetupBoxOverlap();
        }

        [Button("Execute")]
        public void ExecuteAndLog()
        {
            Execute(out IEnumerable<Collider> colliders);

            Debug.Log(colliders.AsString(x => x.name), this);
        }

        public int Execute(out IEnumerable<Collider> colliders)
        {
            SetupBoxOverlap();
            _boxOverlap.Execute(out colliders);
            colliders = colliders.Where(x =>
            {
                Vector3 forwardDir = _boxOverlap.Pose.forward;
                Vector3 colliderDir = (x.transform.position - _boxOverlap.Center).normalized;
                float dot = Vector3.Dot(forwardDir, colliderDir);

                return dot >= Mathf.Cos(Angle / 2);
            });

            _gizmosColorValue = 1;

            return colliders.Count();
        }

        public void SetPoseTransform(Transform newTransform, bool changePoseOrigin = true)
        {
            _poseTransform = newTransform;

            if (changePoseOrigin)
                PoseOrigin = OverlapPoseOrigin.OverrideTransform;

            SetupBoxOverlap();
        }

        public void SetCustomPose(Vector3 position, Quaternion rotation, bool changePoseOrigin = true)
        {
            _center = position;
            _rotation = rotation.eulerAngles;

            if (changePoseOrigin)
                PoseOrigin = OverlapPoseOrigin.Custom;

            SetupBoxOverlap();
        }

        private void SetupBoxOverlap()
        {
            _boxOverlap.GizmosEnabled = false;
            _boxOverlap.Layers = Layers;
            float boxSize = Distance * Mathf.Tan(Angle / 2 * Mathf.Deg2Rad);
            float halfDistance = Distance / 2;
            _boxOverlap.HalfExtents = new Vector3(boxSize, boxSize, halfDistance);
            _boxOverlap.SetCustomPose(ApexPosition + Rotation * Vector3.forward * halfDistance, Rotation);
        }

        public void OnDrawGizmos()
        {
            if (!GizmosEnabled)
                return;

            // TODO: Make cone with spherical base.
            Gizmos.matrix = Matrix4x4.TRS(ApexPosition, Rotation, Vector3.one);
            GizmosExtend.DrawCone(
                Vector3.zero,
                Vector3.forward * Distance,
                Color.Lerp(GizmosDefaultColor, GizmosExecutedColor,
                    Mathf.SmoothDamp(_gizmosColorValue, 0, ref _gizmosColorValue, GizmosFadeTime)),
                Angle / 2);
        }
    }
}