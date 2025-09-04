using System;
using NaughtyAttributes;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    public delegate void TimerUpdateHandler(float deltaTime);
    
    // TODO: Add check for enabled GO in Start method. Also, review Awake logic after this.
    // TODO: Consider removing the _onStart callback.

    public class Timer : MonoBehaviour
    {
        public TimerUpdateMethod UpdateMethod = TimerUpdateMethod.Update;

        private const float NotStartedTime = -1;
        private Action _onStart;
        private TimerUpdateHandler _onUpdate;
        private Action _onStop;
        private Action _onComplete;

        public event Action Started;
        public event TimerUpdateHandler Updated;
        public event Action Stopped;
        public event Action Completed;

        [ShowNativeProperty]
        public float RemainingTime { get; private set; } = NotStartedTime;
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        [ShowNativeProperty]
        public bool IsStarted => RemainingTime != NotStartedTime;
        public bool IsPaused => IsStarted && !enabled;

        private void Awake()
        {
            if (!IsStarted)
                enabled = false;
        }

        private void OnEnable()
        {
            if (!IsStarted)
            {
                Debug.LogError("Can't enable timer that is not started");

                enabled = false;
            }
        }

        /// <summary>
        /// ReSharper disable once Unity.RedundantEventFunction
        /// Used to remove the following error:
        /// Script error (Timer): Start() can not take parameters.
        /// </summary>
        private void Start()
        {
        }

        private void OnValidate()
        {
            if (!IsStarted)
                enabled = false;
        }

        public void Update()
        {
            if (UpdateMethod != TimerUpdateMethod.Update)
                return;

            Update(Time.deltaTime);
        }

        public void FixedUpdate()
        {
            if (UpdateMethod != TimerUpdateMethod.FixedUpdate)
                return;

            Update(Time.fixedDeltaTime);
        }

        public void Start(float timeInSeconds,
            Action onStart = null, TimerUpdateHandler onUpdate = null, Action onStop = null, Action onComplete = null)
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("Can't start timer not in playmode");

                return;
            }

            SetupAndEnable(timeInSeconds, onStart, onUpdate, onStop, onComplete);
            _onStart?.Invoke();
            Started?.Invoke();
        }

        [Button("Start (60 sec)")]
        private void TestStart()
        {
            Start(60);
        }

        [HideIf(nameof(IsPaused))]
        [Button]
        public void Pause()
        {
            enabled = false;
        }

        [ShowIf(nameof(IsPaused))]
        [Button]
        public void Resume()
        {
            enabled = true;
        }

        [Button]
        public void Stop()
        {
            // Cache callback before cleaning in Terminate() method.
            Action onStop = _onStop;

            CleanupAndDisable();
            onStop?.Invoke();
            Stopped?.Invoke();
        }

        public void SetRemainingTime(float time)
        {
            if (!IsStarted)
            {
                Debug.LogError("Can't set remaining time on timer that is not started");

                return;
            }

            RemainingTime = time;
        }

        private void Update(float deltaTime)
        {
            RemainingTime -= deltaTime;
            _onUpdate?.Invoke(deltaTime);
            Updated?.Invoke(deltaTime);

            if (RemainingTime <= 0)
            {
                // Cache callback before cleaning in CleanupAndDisable() method.
                Action onComplete = _onComplete;

                CleanupAndDisable();
                onComplete?.Invoke();
                Completed?.Invoke();
            }
        }

        private void SetupAndEnable(float timeInSeconds,
            Action onStart, TimerUpdateHandler onUpdate, Action onStop, Action onComplete)
        {
            RemainingTime = timeInSeconds;
            enabled = true;

            _onStart = onStart;
            _onUpdate = onUpdate;
            _onStop = onStop;
            _onComplete = onComplete;
        }

        private void CleanupAndDisable()
        {
            RemainingTime = NotStartedTime;
            enabled = false;

            _onStart = null;
            _onUpdate = null;
            _onStop = null;
            _onComplete = null;
        }

        public enum TimerUpdateMethod
        {
            Update,
            FixedUpdate
        }
    }
}