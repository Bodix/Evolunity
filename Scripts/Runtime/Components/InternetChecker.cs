// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using Evolutex.Evolunity.Extensions;
using Evolutex.Evolunity.Utilities;
using UnityEngine;
using UnityEngine.Networking;

namespace Evolutex.Evolunity.Components
{
    [AddComponentMenu("Evolunity/Internet Checker")]
    public class InternetChecker : MonoBehaviour
    {
        [Min(1)]
        public int InitializationTimeout = 1;
        /// <summary>
        /// Interval between pings in seconds when connected.
        /// </summary>
        [Min(1)]
        public int ConnectedPingInterval = 1;
        /// <summary>
        /// Interval between pings in seconds when disconnected.
        /// </summary>
        [Min(1)]
        public int DisconnectedPingInterval = 1;

        // Google:
        // https://clients3.google.com/generate_204
        // https://www.gstatic.com/generate_204
        // https://connectivitycheck.gstatic.com/generate_204
        // Cloudflare:
        // https://1.1.1.1/generate_204

        /// <summary>
        /// Dont use "https://google.com" URL because Google <a href="https://stackoverflow.com/a/77422720/8614296">may block your IP</a>.
        /// Ideally the PingUrl should be your game service, because firewall may block access to some endpoints.
        /// The player may be connected to an intranet or captive portal.
        /// </summary>
        public string PingUrl = "https://clients3.google.com/generate_204";
        public bool Logs;

        public bool IsInitialized { get; private set; }
        public bool IsConnected { get; private set; }

        public event Action Initialized;
        public event Action InternetConnected;
        public event Action InternetDisconnected;

        private void Awake()
        {
            StartCoroutine(Initialize());
        }

        private void OnEnable()
        {
            StartCoroutine(CheckConnection());
        }

        private IEnumerator Initialize()
        {
            if (!Validate.InternetReachability())
                OnInitialized(false);
            else
                yield return ValidatedRequest(Request(InitializationTimeout, OnInitialized));
        }

        private IEnumerator CheckConnection()
        {
            yield return new WaitUntil(() => IsInitialized);

            while (enabled)
            {
                if (!Validate.InternetReachability() && IsConnected)
                    OnDisconnected();

                if (Validate.InternetReachability())
                    yield return ValidatedRequest(Ping(IsConnected ? ConnectedPingInterval : DisconnectedPingInterval));

                yield return null;
            }
        }

        private IEnumerator ValidatedRequest(IEnumerator requestEnumerator)
        {
            if (Validate.Url(PingUrl))
            {
                yield return requestEnumerator;
            }
            else
            {
                enabled = false;

                Debug.LogError("Invalid ping URL. " + nameof(InternetChecker) + " has been disabled", this);
            }
        }

        private IEnumerator Ping(int interval)
        {
            float requestTime = Time.time;
            yield return Request(interval, isSuccessful =>
            {
                if (isSuccessful && !IsConnected)
                    OnConnected();
                else if (!isSuccessful && IsConnected)
                    OnDisconnected();
            });
            float responseTime = Time.time;
            float requestDuration = responseTime - requestTime;

            float remainingInterval = interval - requestDuration;
            if (remainingInterval > 0)
                yield return new WaitForSeconds(remainingInterval);
        }

        private IEnumerator Request(int timeout, Action<bool> onComplete)
        {
            using (UnityWebRequest request = UnityWebRequest.Head(PingUrl))
            {
                request.timeout = timeout;

                yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
                bool isSuccessful = request.result == UnityWebRequest.Result.Success;
#else
                bool isSuccessful = !request.IsError();
#endif

                onComplete?.Invoke(isSuccessful);
            }
        }

        private void OnInitialized(bool isConnected)
        {
            IsConnected = isConnected;
            IsInitialized = true;

            Initialized?.Invoke();
        }

        private void OnConnected()
        {
            IsConnected = true;

            if (Logs)
                Debug.LogWarning("Connected to the internet", this);

            InternetConnected?.Invoke();
        }

        private void OnDisconnected()
        {
            IsConnected = false;

            if (Logs)
                Debug.LogWarning("Disconnected from the internet", this);

            InternetDisconnected?.Invoke();
        }
    }
}