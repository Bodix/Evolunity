// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using Evolutex.Evolunity.Extensions;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    // TODO:
    // Make a covering of the corners.
    
    /// <summary>
    /// Instantiates four <see cref="BoxCollider2D"/>'s around a given <see cref="Rect"/>.
    /// <para />
    /// <para />If enabled:
    /// <para /> - If the object has a <see cref="RectTransform"/>, this component will create
    /// borders for the RectTransform's <see cref="RectTransform.rect"/> in the Start() method.
    /// <para /> - If the object does not have a <see cref="RectTransform"/>, this component will create
    /// borders for the MainCamera's <see cref="UnityEngine.Camera.pixelRect"/> in the Start() method.
    /// <para />
    /// <para />In both cases, the boundaries will be child of the object on which the component resides.
    /// </summary>
    [AddComponentMenu("Evolunity/Rect Borders")]
    public class RectBorders : MonoBehaviour
    {
        public float Thickness = 1f;
        [Min(0.01f)]
        [Tooltip("The distance from point of view of perspective camera. " +
                 "There will no effect if the camera is orthographic")]
        public float PerspectiveZOffset = 1f;
        public Camera Camera;

        private BoxCollider2D topBorder;
        private BoxCollider2D bottomBorder;
        private BoxCollider2D leftBorder;
        private BoxCollider2D rightBorder;

        private void Start()
        {
            if (!Camera)
                Camera = Camera.main;

            Setup(GetComponent<RectTransform>());
        }

        // private void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.green;
        //     if (topBorder)
        //         Gizmos.DrawSphere(topBorder.transform.position, thickness / 10f);
        //     if (bottomBorder)
        //         Gizmos.DrawSphere(bottomBorder.transform.position, thickness / 10f);
        //     if (leftBorder)
        //         Gizmos.DrawSphere(leftBorder.transform.position, thickness / 10f);
        //     if (rightBorder)
        //         Gizmos.DrawSphere(rightBorder.transform.position, thickness / 10f);
        // }

        private void Setup(RectTransform rectTransform = null)
        {
            Rect rect = rectTransform
                ? rectTransform.GetWorldRect()
                : Camera.pixelRect;
            bool convertToWorldSpace = !rectTransform;

            Create();
            SetPositions(rect, convertToWorldSpace);
            SetSizes(rect, convertToWorldSpace);
        }

        private void Create()
        {
            topBorder = new GameObject("Top Border").AddComponent<BoxCollider2D>();
            bottomBorder = new GameObject("Bottom Border").AddComponent<BoxCollider2D>();
            leftBorder = new GameObject("Left Border").AddComponent<BoxCollider2D>();
            rightBorder = new GameObject("Right Border").AddComponent<BoxCollider2D>();

            topBorder.transform.SetParent(transform);
            bottomBorder.transform.SetParent(transform);
            leftBorder.transform.SetParent(transform);
            rightBorder.transform.SetParent(transform);

            topBorder.gameObject.layer = gameObject.layer;
            bottomBorder.gameObject.layer = gameObject.layer;
            leftBorder.gameObject.layer = gameObject.layer;
            rightBorder.gameObject.layer = gameObject.layer;
        }

        private void SetPositions(Rect screenRect, bool convertToWorldSpace)
        {
            Vector3 topBorderPosition = new Vector3(screenRect.x + screenRect.width / 2f, screenRect.yMax);
            Vector3 bottomBorderPosition = new Vector3(screenRect.x + screenRect.width / 2f, screenRect.y);
            Vector3 leftBorderPosition = new Vector3(screenRect.x, screenRect.y + screenRect.height / 2f);
            Vector3 rightBorderPosition = new Vector3(screenRect.xMax, screenRect.y + screenRect.height / 2f);

            if (convertToWorldSpace)
            {
                topBorderPosition = Camera.ScreenToWorldPoint(!Camera.orthographic
                    ? topBorderPosition.WithZ(PerspectiveZOffset)
                    : topBorderPosition);
                bottomBorderPosition = Camera.ScreenToWorldPoint(!Camera.orthographic
                    ? bottomBorderPosition.WithZ(PerspectiveZOffset)
                    : bottomBorderPosition);
                leftBorderPosition = Camera.ScreenToWorldPoint(!Camera.orthographic
                    ? leftBorderPosition.WithZ(PerspectiveZOffset)
                    : leftBorderPosition);
                rightBorderPosition = Camera.ScreenToWorldPoint(!Camera.orthographic
                    ? rightBorderPosition.WithZ(PerspectiveZOffset)
                    : rightBorderPosition);
            }

            topBorder.transform.position = topBorderPosition.AddY(Thickness / 2f);
            bottomBorder.transform.position = bottomBorderPosition.AddY(-Thickness / 2f);
            leftBorder.transform.position = leftBorderPosition.AddX(-Thickness / 2f);
            rightBorder.transform.position = rightBorderPosition.AddX(Thickness / 2f);
        }

        private void SetSizes(Rect screenRect, bool convertToWorldSpace)
        {
            float width = screenRect.width;
            float height = screenRect.height;

            if (convertToWorldSpace)
            {
                Vector3 rectMin = !Camera.orthographic
                    ? screenRect.min.ToVector3().WithZ(PerspectiveZOffset)
                    : screenRect.min.ToVector3();
                Vector3 rectMax = !Camera.orthographic
                    ? screenRect.max.ToVector3().WithZ(PerspectiveZOffset)
                    : screenRect.max.ToVector3();
            
                width = Vector2.Distance(
                    Camera.ScreenToWorldPoint(rectMax.WithY(0)),
                    Camera.ScreenToWorldPoint(rectMin));
                height = Vector2.Distance(
                    Camera.ScreenToWorldPoint(rectMax.WithX(0)),
                    Camera.ScreenToWorldPoint(rectMin));
            }

            topBorder.size = new Vector2(width, Thickness);
            bottomBorder.size = new Vector2(width, Thickness);
            leftBorder.size = new Vector2(Thickness, height);
            rightBorder.size = new Vector2(Thickness, height);
        }
    }
}