/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Evolunity.Components.Input
{
    [AddComponentMenu("UI/Input Reader")]
    [RequireComponent(typeof(Graphic))]
    public sealed class InputReader : MonoBehaviour, IPointerClickHandler, IDragHandler
    {
        // TO DO: Rotate.
        
        public event Action<Vector2> Drag;
        public event Action<Vector2> DoubleDrag;
        public event Action<float> Zoom;
        public event Action<Vector2> Click;

#if UNITY_EDITOR || !(UNITY_IOS && UNITY_ANDROID)
        private void Update()
        {
            if (UnityEngine.Input.mouseScrollDelta.y != 0)
                Zoom?.Invoke(UnityEngine.Input.mouseScrollDelta.y / 10);
        }
#endif

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
#if UNITY_EDITOR || !(UNITY_IOS || UNITY_ANDROID)
            if (eventData.button == PointerEventData.InputButton.Left)
                Drag?.Invoke(eventData.delta);
            else if (eventData.button == PointerEventData.InputButton.Right)
                DoubleDrag?.Invoke(eventData.delta);
#else
            if (UnityEngine.Input.touchCount == 2)
            {
                DoubleDrag?.Invoke(eventData.delta);
                Pinch();
            }
            else if (UnityEngine.Input.touchCount == 1)
                Drag?.Invoke(eventData.delta);
#endif
        }

        private void Pinch()
        {
            Touch firstTouch = UnityEngine.Input.GetTouch(0);
            Touch secondTouch = UnityEngine.Input.GetTouch(1);

            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            float prevTouchDelta = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
            float touchDelta = (firstTouch.position - secondTouch.position).magnitude;

            Zoom?.Invoke((touchDelta - prevTouchDelta) / 100);
        }
    }
}