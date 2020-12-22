// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Evolutex.Evolunity.Components
{
    // TO DO:
    //
    // 1. Test saving time delta:
    // private void RestartTimer()
    // {
    //     timer -= Period;
    // }
    //
    // 2. Compare performance with PeriodicCoroutineBehaviour.
    
    [AddComponentMenu("Toolkit/Periodic Behaviour")]
    public class PeriodicBehaviour : MonoBehaviour
    {
        public UnityEvent OnPeriodCallback;
        
        [Space]
        
        [SerializeField]
        private float period = 1f;

        private float timer;

        public float Period
        {
            get => period;
            set
            {
                if (value < 0)
                    throw new ArgumentException("It is not possible to assign a period less than zero");

                period = value;
            }
        }

        public float PeriodProgress => Period > Time.deltaTime ? timer / Period : 1f;

        /// <summary>
        /// Use only in Unity Editor.
        /// </summary>
        public virtual bool DrawPeriodEventInInspector => GetType() == typeof(PeriodicBehaviour);

        /// <summary>
        /// Use only in Unity Editor.
        /// </summary>
        public virtual bool DrawPeriodFieldInInspector => true;

        /// <summary>
        /// Use only in Unity Editor.
        /// </summary>
        public virtual bool DrawPeriodProgressInInspector => true;

        private void OnValidate()
        {
            if (period < 0)
                period = 0;
        }

        protected virtual void Update()
        {
            if (Period == 0)
            {
                OnPeriodInternal();

                return;
            }

            timer += Time.deltaTime;

            if (timer > Period)
            {
                RestartTimer();

                OnPeriodInternal();
            }
        }

        protected virtual void OnPeriod() { }

        private void OnPeriodInternal()
        {
            OnPeriod();

            OnPeriodCallback?.Invoke();
        }
        
        [ContextMenu("Restart")]
        private void RestartTimer()
        {
            timer = 0;
        }
    }
}