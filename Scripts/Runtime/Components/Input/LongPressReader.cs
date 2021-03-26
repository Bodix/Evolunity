// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components
{
    [AddComponentMenu("Evolunity/Input/Long Press Reader")]
    [RequireComponent(typeof(Graphic))]
    public sealed class LongPressReader : MonoBehaviour,
        IPointerDownHandler,
        IPointerUpHandler,
        IBeginDragHandler
    {
        public event Action LongPress;

        public float Delay = 0.3f;
        public bool Continuous = false;

        private Coroutine holdCoroutine;
        private bool isHold;

        #region Callbacks

        public void OnPointerDown(PointerEventData eventData) => StartHold();

        public void OnPointerUp(PointerEventData eventData) => StopHold();

        public void OnBeginDrag(PointerEventData eventData) => StopHold();

        #endregion

        private void StartHold()
        {
            holdCoroutine = Continuous ? StartCoroutine(ContinuousHold()) : StartCoroutine(Hold());
            isHold = true;
        }

        private IEnumerator Hold()
        {
            yield return new WaitForSeconds(Delay);

            if (isHold)
                LongPress?.Invoke();
        }

        private IEnumerator ContinuousHold()
        {
            yield return new WaitForSeconds(Delay);

            while (isHold)
            {
                yield return null;

                LongPress?.Invoke();
            }
        }

        private void StopHold()
        {
            StopCoroutine(holdCoroutine);
            isHold = false;
        }
    }
}