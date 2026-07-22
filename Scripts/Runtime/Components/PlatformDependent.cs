// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[AddComponentMenu("Evolunity/Platform Dependent")]
	[DisallowMultipleComponent]
	public sealed class PlatformDependent : MonoBehaviour
	{
		[SerializeField]
		private DisableMethod disableMethod = DisableMethod.Destroy;
		[SerializeField]
		private bool PC = true;
		[SerializeField]
		private bool iOS = true;
		[SerializeField]
		private bool android = true;
		[SerializeField]
		private bool webGL = true;
		[SerializeField]
		private bool other = true;

		[SerializeField]
		private bool log = true;

		private bool _isDisable;

		private void Awake()
		{
			_isDisable = !IsCurrentPlatformEnabled();

			if (_isDisable)
				switch (disableMethod)
				{
					case DisableMethod.Destroy:
						Destroy(gameObject);
						break;
					case DisableMethod.Disable:
						gameObject.SetActive(false);
						break;
				}
		}

		private bool IsCurrentPlatformEnabled()
		{
			// We use a switch statement for a cleaner and more readable platform check.
			switch (Application.platform)
			{
				case RuntimePlatform.WindowsPlayer:
				case RuntimePlatform.WindowsEditor:
				case RuntimePlatform.OSXPlayer:
				case RuntimePlatform.OSXEditor:
				case RuntimePlatform.LinuxPlayer:
				case RuntimePlatform.LinuxEditor:
					return PC;
				case RuntimePlatform.IPhonePlayer:
					return iOS;
				case RuntimePlatform.Android:
					return android;
				case RuntimePlatform.WebGLPlayer:
					return webGL;
				default:
					return other;
			}
		}

		private void OnDisable()
		{
			if (_isDisable && disableMethod == DisableMethod.Disable && log)
				Debug.Log("The GameObject \"" + gameObject.name + "\" was disabled by the PlatformDependent component",
					this);
		}

		private void OnDestroy()
		{
			if (_isDisable && disableMethod == DisableMethod.Destroy && log)
				Debug.Log("The GameObject \"" + gameObject.name + "\" was destroyed by the PlatformDependent component");
		}
	}
}