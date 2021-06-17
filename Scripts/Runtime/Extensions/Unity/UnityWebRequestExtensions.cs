// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Evolutex.Evolunity.Utilities;

namespace Evolutex.Evolunity.Extensions
{
    public static class UnityWebRequestExtensions
    {
        public delegate void ResponseHandler(UnityWebRequest response);

        public static WrappedCoroutine WaitForResponseInWrappedCoroutine(
            this UnityWebRequestAsyncOperation asyncOperation, MonoBehaviour monoBehaviour, ResponseHandler onComplete)
        {
            return monoBehaviour.StartWrappedCoroutine(WaitForResponse(asyncOperation, onComplete));
        }

        public static Coroutine WaitForResponseInCoroutine(
            this UnityWebRequestAsyncOperation asyncOperation, MonoBehaviour monoBehaviour, ResponseHandler onComplete)
        {
            return monoBehaviour.StartCoroutine(WaitForResponse(asyncOperation, onComplete));
        }

        private static IEnumerator WaitForResponse(UnityWebRequestAsyncOperation asyncOperation,
            ResponseHandler onComplete)
        {
            yield return asyncOperation;

            onComplete?.Invoke(asyncOperation.webRequest);
        }

        public static bool IsError(this UnityWebRequest webRequest)
        {
            return webRequest.isNetworkError || webRequest.isHttpError;
        }
    }
}