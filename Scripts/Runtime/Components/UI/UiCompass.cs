// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	/// <summary>
	/// Turns its image so that the top of the image points to the world north, the +Z axis, as the camera sees it,
	/// e.g. the card of a compass on a HUD.
	/// </summary>
	[AddComponentMenu("Evolunity/UI/UI Compass")]
	public class UiCompass : MonoBehaviour
	{
		[Tooltip("Optional. The main camera when empty.")]
		[SerializeField]
		private Camera _camera;

		private void LateUpdate()
		{
			Camera view = _camera != null ? _camera : Camera.main;
			if (view == null)
				return;

			// The way up the screen on the ground: the forward of a tilted camera, the up of a camera that looks straight down.
			Vector3 screenUp = view.transform.forward + view.transform.up;
			float yaw = Mathf.Atan2(screenUp.x, screenUp.z) * Mathf.Rad2Deg;
			transform.localRotation = Quaternion.Euler(0f, 0f, yaw);
		}
	}
}
