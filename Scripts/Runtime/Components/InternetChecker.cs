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

        // Cloudflare:
        // https://1.1.1.1/generate_204
        // Google:
        // https://clients3.google.com/generate_204
        // https://www.gstatic.com/generate_204
        // https://connectivitycheck.gstatic.com/generate_204

        /// <summary>
        /// Dont use "https://google.com" URL because Google <a href="https://stackoverflow.com/a/77422720/8614296">may block your IP</a>.
        /// Ideally the PingUrl should be your game service, because firewall may block access to some endpoints.
        /// The player may be connected to an intranet or captive portal.
        /// </summary>
        public string PingUrl = "https://clients3.google.com/generate_204";
        public bool Logs;

        public bool IsConnected { get; private set; }

        public event Action InternetConnected;
        public event Action InternetDisconnected;

        private void OnEnable()
        {
            StartCoroutine(CheckConnection());
        }

        private IEnumerator CheckConnection()
        {
            while (enabled)
            {
                if (!Validate.InternetConnection() && IsConnected)
                    OnDisconnected();

                if (Validate.InternetConnection())
                    yield return Ping(IsConnected ? ConnectedPingInterval : DisconnectedPingInterval);

                yield return null;
            }
        }

        private IEnumerator Ping(int interval)
        {
            if (Validate.Url(PingUrl))
            {
                using (UnityWebRequest request = UnityWebRequest.Head(PingUrl))
                {
                    request.timeout = interval;

                    float requestTime = Time.time;
                    yield return request.SendWebRequest();
                    float responseTime = Time.time;
                    float requestDuration = responseTime - requestTime;

#if UNITY_2020_1_OR_NEWER
                    bool isSuccessful = request.result == UnityWebRequest.Result.Success;
#else
                    bool isSuccessful = !request.IsError();
#endif
                    if (isSuccessful && !IsConnected)
                        OnConnected();
                    else if (!isSuccessful && IsConnected)
                        OnDisconnected();

                    float remainingInterval = interval - requestDuration;
                    if (remainingInterval > 0)
                        yield return new WaitForSeconds(remainingInterval);
                }
            }
            else
            {
                enabled = false;

                Debug.LogError("Invalid ping URL. " + nameof(InternetChecker) + " has been disabled", this);
            }
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