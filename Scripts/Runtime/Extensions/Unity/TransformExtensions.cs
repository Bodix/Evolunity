// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using Evolutex.Evolunity.Structs;
using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class TransformExtensions
    {
        public static void SetX(this Transform transform, float value)
        {
            transform.position = transform.position.WithX(value);
        }

        public static void SetLocalX(this Transform transform, float value)
        {
            transform.localPosition = transform.localPosition.WithX(value);
        }

        public static void SetY(this Transform transform, float value)
        {
            transform.position = transform.position.WithY(value);
        }

        public static void SetLocalY(this Transform transform, float value)
        {
            transform.localPosition = transform.localPosition.WithY(value);
        }

        public static void SetZ(this Transform transform, float value)
        {
            transform.position = transform.position.WithZ(value);
        }

        public static void SetLocalZ(this Transform transform, float value)
        {
            transform.localPosition = transform.localPosition.WithZ(value);
        }

        public static void Reset(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static void SetParentAndAlign(this Transform transform, Transform parent, bool keepLocalTransform = true)
        {
            Vector3 localPosition = transform.localPosition;
            Quaternion localRotation = transform.localRotation;

            transform.SetParent(parent);

            if (keepLocalTransform)
            {
                transform.localPosition = localPosition;
                transform.localRotation = localRotation;
            }
            else
            {
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
            }
        }

        public static TransformData GetData(this Transform transform)
        {
            return new TransformData(transform.position, transform.rotation, transform.localScale);
        }

        public static void SetData(this Transform transform, TransformData data)
        {
            transform.position = data.Position;
            transform.rotation = data.Rotation;
            transform.localScale = data.LocalScale;
        }

        // https://forum.unity.com/threads/solved-how-to-get-rotation-value-that-is-in-the-inspector.460310/#post-4564687
        public static Vector3 GetInspectorRotation(this Transform transform)
        {
            Vector3 angle = transform.eulerAngles;

            float x = angle.x;
            float y = angle.y;
            float z = angle.z;

            if (Vector3.Dot(transform.up, Vector3.up) >= 0f)
            {
                if (angle.x >= 0f && angle.x <= 90f)
                    x = angle.x;

                if (angle.x >= 270f && angle.x <= 360f)
                    x = angle.x - 360f;
            }

            if (Vector3.Dot(transform.up, Vector3.up) < 0f)
            {
                if (angle.x >= 0f && angle.x <= 90f)
                    x = 180 - angle.x;

                if (angle.x >= 270f && angle.x <= 360f)
                    x = 180 - angle.x;
            }

            if (angle.y > 180)
                y = angle.y - 360f;

            if (angle.z > 180)
                z = angle.z - 360f;

            return new Vector3(x, y, z);
        }

        public static List<Transform> GetChildren(this Transform transform)
        {
            return transform.Cast<Transform>().ToList();
        }

        public static List<Transform> GetChildrenRecursively(this Transform transform,
            List<Transform> childrenList = null)
        {
            if (childrenList == null)
                childrenList = new List<Transform>();

            foreach (Transform child in transform)
            {
                childrenList.Add(child);

                child.GetChildrenRecursively(childrenList);
            }

            return childrenList;
        }

        public static void DestroyChildren(this Transform transform)
        {
            for (int i = 0; i < transform.childCount; i++)
                Object.Destroy(transform.GetChild(i).gameObject);

            // Update transform.childCount in the current frame.
            transform.DetachChildren();
        }

        public static void DestroyChildrenImmediate(this Transform transform,
            bool allowDestroyingAssets = false, bool activeOnly = false)
        {
            Transform[] children = transform.GetComponentsInChildren<Transform>();

            foreach (Transform child in children)
            {
                if (child == transform)
                    continue;

                if (activeOnly && !child.gameObject.activeSelf)
                    continue;

                Object.DestroyImmediate(child.gameObject, allowDestroyingAssets);
            }
        }

        public static RectTransform ToRectTransform(this Transform transform)
        {
            return (RectTransform)transform;
        }
    }
}