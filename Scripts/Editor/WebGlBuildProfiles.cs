// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEditor;
using UnityEngine;
#if UNITY_2021_2_OR_NEWER
using UnityEditor.Build;
#endif

namespace Bodix.Evolunity.Editor
{
	public static class WebGLBuildProfiles
	{
		public static event Action OnDevelopmentProfileApplied;
		public static event Action OnReleaseProfileApplied;
		public static event Action OnDebugProfileApplied;

		[MenuItem("Build/WebGL Profiles/Set Development (Fast Build)")]
		public static void SetDevelopmentProfile()
		{
			EditorUserBuildSettings.development = true;
			PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
			PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None;

#if UNITY_2021_2_OR_NEWER
			PlayerSettings.SetManagedStrippingLevel(NamedBuildTarget.WebGL, ManagedStrippingLevel.Minimal);
#else
			PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.WebGL, ManagedStrippingLevel.Low);
#endif

#pragma warning disable 0618
#if UNITY_2021_2_OR_NEWER
			PlayerSettings.SetIl2CppCompilerConfiguration(NamedBuildTarget.WebGL, Il2CppCompilerConfiguration.Debug);
#else
			PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.WebGL, Il2CppCompilerConfiguration.Debug);
#endif
#pragma warning restore 0618

			OnDevelopmentProfileApplied?.Invoke();
			Debug.Log("\"Development\" WebGL profile applied.");
		}

		[MenuItem("Build/WebGL Profiles/Set Release (Optimized Build Size)")]
		public static void SetReleaseProfile()
		{
			EditorUserBuildSettings.development = false;
			PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Brotli;
			PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.None;

#if UNITY_2021_2_OR_NEWER
			PlayerSettings.SetManagedStrippingLevel(NamedBuildTarget.WebGL, ManagedStrippingLevel.High);
#else
			PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.WebGL, ManagedStrippingLevel.High);
#endif

#pragma warning disable 0618
#if UNITY_2021_2_OR_NEWER
			PlayerSettings.SetIl2CppCompilerConfiguration(NamedBuildTarget.WebGL, Il2CppCompilerConfiguration.Master);
#else
			PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.WebGL, Il2CppCompilerConfiguration.Master);
#endif
#pragma warning restore 0618

			OnReleaseProfileApplied?.Invoke();
			Debug.Log("\"Release\" WebGL profile applied.");
		}

		[MenuItem("Build/WebGL Profiles/Set Debug (Slow Build)")]
		public static void SetDeepDebugProfile()
		{
			EditorUserBuildSettings.development = true;
			PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
			PlayerSettings.WebGL.exceptionSupport = WebGLExceptionSupport.FullWithStacktrace;

#if UNITY_2021_2_OR_NEWER
			PlayerSettings.SetManagedStrippingLevel(NamedBuildTarget.WebGL, ManagedStrippingLevel.Minimal);
#else
			PlayerSettings.SetManagedStrippingLevel(BuildTargetGroup.WebGL, ManagedStrippingLevel.Low);
#endif

#pragma warning disable 0618
#if UNITY_2021_2_OR_NEWER
			PlayerSettings.SetIl2CppCompilerConfiguration(NamedBuildTarget.WebGL, Il2CppCompilerConfiguration.Debug);
#else
			PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.WebGL, Il2CppCompilerConfiguration.Debug);
#endif
#pragma warning restore 0618

			OnDebugProfileApplied?.Invoke();

			Debug.Log("\"Debug\" WebGL profile applied. WARNING: Build will take longer!");
		}
	}
}