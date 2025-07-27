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
        /// Interval between pings in seconds.
        /// </summary>
        public int PingFrequency = 1;
        /// <summary>
        /// Dont use "google.com" URL because Google <a href="https://stackoverflow.com/a/77422720/8614296">may block your IP</a>.
        /// Ideally the PingUrl should be your game service, because firewall may block access to some endpoints.
        /// The player may be connected to an intranet or captive portal.
        /// </summary>
        public string PingUrl = "8.8.8.8";
        public bool Logs;

        public bool IsConnected { get; private set; }

        public event Action InternetConnected;
        public event Action InternetDisconnected;

        private void Awake()
        {
            IsConnected = Validate.InternetConnection();
        }

        private void OnEnable()
        {
            StartCoroutine(CheckConnection());
        }

        private IEnumerator CheckConnection()
        {
            while (enabled)
            {
                if (!Validate.InternetConnection() && IsConnected)
                {
                    IsConnected = false;

                    if (Logs)
                        Debug.LogWarning("Disconnected from the internet", this);

                    InternetDisconnected?.Invoke();
                }
                else if (Validate.InternetConnection() && !IsConnected)
                {
                    if (Validate.Url(PingUrl))
                    {
                        yield return Ping();
                    }
                    else
                    {
                        enabled = false;

                        Debug.LogError("Invalid ping URL. " + nameof(InternetChecker) + " has been disabled", this);
                    }
                }

                yield return null;
            }
        }

        private IEnumerator Ping()
        {
            using (UnityWebRequest request = UnityWebRequest.Head(PingUrl))
            {
                request.timeout = PingFrequency;

                yield return request.SendWebRequest();

                if (!request.IsError() && request.responseCode == 200)
                {
                    IsConnected = true;

                    if (Logs)
                        Debug.LogWarning("Connected to the internet", this);

                    InternetConnected?.Invoke();
                }
            }
        }
    }
}