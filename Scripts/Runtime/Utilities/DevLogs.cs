// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define DEV_LOGS
#else
#undef DEV_LOGS
#endif

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    /// <summary>
    /// Works only when "Development Build" option is enabled (in "Build Settings") or in Unity Editor.
    /// </summary>
    public static class DevLogs
    {
        private const string DefineSymbol = "DEV_LOGS";

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void Log(object message)
        {
            Debug.Log(message);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void Log(object message, UnityEngine.Object context)
        {
            Debug.Log(message, context);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogFormat(string format, params object[] args)
        {
            Debug.LogFormat(format, args);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
        {
            Debug.LogFormat(context, format, args);
        }


        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogWarning(object message, UnityEngine.Object context)
        {
            Debug.LogWarning(message, context);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogWarningFormat(string format, params object[] args)
        {
            Debug.LogWarningFormat(format, args);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
        {
            Debug.LogWarningFormat(context, format, args);
        }


        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogError(object message)
        {
            Debug.LogError(message);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogError(object message, UnityEngine.Object context)
        {
            Debug.LogError(message, context);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogErrorFormat(string format, params object[] args)
        {
            Debug.LogErrorFormat(format, args);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
        {
            Debug.LogErrorFormat(context, format, args);
        }


        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogAssertion(object message)
        {
            Debug.LogAssertion(message);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogAssertion(object message, UnityEngine.Object context)
        {
            Debug.LogAssertion(message, context);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogAssertionFormat(string format, params object[] args)
        {
            Debug.LogAssertionFormat(format, args);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogAssertionFormat(UnityEngine.Object context, string format, params object[] args)
        {
            Debug.LogAssertionFormat(context, format, args);
        }


        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }

        [System.Diagnostics.Conditional(DefineSymbol)]
        public static void LogException(Exception exception, UnityEngine.Object context)
        {
            Debug.LogException(exception, context);
        }
    }
}