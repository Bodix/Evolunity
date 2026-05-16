// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define DEV_LOGS
#else
#undef DEV_LOGS
#endif

using System;
using System.Diagnostics;

namespace Bodix.Evolunity.Utilities
{
	/// <summary>
	/// Works only when "Development Build" option is enabled (in "Build Settings") or in Unity Editor.
	/// </summary>
	public static class DevLogs
	{
		private const string DefineSymbol = "DEV_LOGS";

		[Conditional(DefineSymbol)]
		public static void Log(object message)
		{
			UnityEngine.Debug.Log(message);
		}

		[Conditional(DefineSymbol)]
		public static void Log(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.Log(message, context);
		}

		[Conditional(DefineSymbol)]
		public static void LogFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogFormat(format, args);
		}

		[Conditional(DefineSymbol)]
		public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
		{
			UnityEngine.Debug.LogFormat(context, format, args);
		}


		[Conditional(DefineSymbol)]
		public static void LogWarning(object message)
		{
			UnityEngine.Debug.LogWarning(message);
		}

		[Conditional(DefineSymbol)]
		public static void LogWarning(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogWarning(message, context);
		}

		[Conditional(DefineSymbol)]
		public static void LogWarningFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogWarningFormat(format, args);
		}

		[Conditional(DefineSymbol)]
		public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
		{
			UnityEngine.Debug.LogWarningFormat(context, format, args);
		}


		[Conditional(DefineSymbol)]
		public static void LogError(object message)
		{
			UnityEngine.Debug.LogError(message);
		}

		[Conditional(DefineSymbol)]
		public static void LogError(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogError(message, context);
		}

		[Conditional(DefineSymbol)]
		public static void LogErrorFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogErrorFormat(format, args);
		}

		[Conditional(DefineSymbol)]
		public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
		{
			UnityEngine.Debug.LogErrorFormat(context, format, args);
		}


		[Conditional(DefineSymbol)]
		public static void LogAssertion(object message)
		{
			UnityEngine.Debug.LogAssertion(message);
		}

		[Conditional(DefineSymbol)]
		public static void LogAssertion(object message, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogAssertion(message, context);
		}

		[Conditional(DefineSymbol)]
		public static void LogAssertionFormat(string format, params object[] args)
		{
			UnityEngine.Debug.LogAssertionFormat(format, args);
		}

		[Conditional(DefineSymbol)]
		public static void LogAssertionFormat(UnityEngine.Object context, string format, params object[] args)
		{
			UnityEngine.Debug.LogAssertionFormat(context, format, args);
		}


		[Conditional(DefineSymbol)]
		public static void LogException(Exception exception)
		{
			UnityEngine.Debug.LogException(exception);
		}

		[Conditional(DefineSymbol)]
		public static void LogException(Exception exception, UnityEngine.Object context)
		{
			UnityEngine.Debug.LogException(exception, context);
		}
	}
}