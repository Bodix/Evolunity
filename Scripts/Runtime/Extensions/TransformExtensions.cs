// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using System.Linq;
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

        public static List<Transform> GetChildren(this Transform transform)
        {
            return transform.Cast<Transform>().ToList();
        }
        
        public static List<Transform> GetChildrenRecursive(this Transform transform, List<Transform> childrenList = null)
        {
            if (childrenList == null) 
                childrenList = new List<Transform>();

            foreach (Transform child in transform)
            {
                childrenList.Add(child);
                
                child.GetChildrenRecursive(childrenList);
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

        public static RectTransform ToRectTransform(this Transform transform)
        {
            return (RectTransform) transform;
        }

        public static Rect ToScreenRect(this RectTransform rectTransform, Camera camera = null)
        {
            if (!camera)
                camera = Camera.main;

            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            for (int i = 0; i < corners.Length; i++)
                corners[i] = camera.WorldToScreenPoint(corners[i]);

            Vector3 position = new Vector3(corners[1].x, Screen.height - corners[1].y);
            Vector3 size = new Vector3(Screen.width - corners[0].x, Screen.height - corners[0].y) - position;

            return new Rect(position, size);
        }
    }
}