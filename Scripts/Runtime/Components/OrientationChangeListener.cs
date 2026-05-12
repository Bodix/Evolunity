using UnityEngine;
using UnityEngine.Events;

namespace Evolutex.Evolunity.Components
{
	public class OrientationChangeListener : MonoBehaviour
	{
		[System.Serializable]
		public class OrientationChangeEvent : UnityEvent<bool>
		{
		}

		/// <summary>
		/// Event fires when orientation changes. True = Landscape, False = Portrait.
		/// </summary>
		public OrientationChangeEvent OrientationChanged;

		private bool _isLandscape;
		private Vector2 _lastResolution;

		private void Start()
		{
			CheckOrientation();
		}

		private void Update()
		{
			// Check if resolution changed (more reliable than Screen.orientation on WebGL).
			if (_lastResolution.x != Screen.width || _lastResolution.y != Screen.height)
				CheckOrientation();
		}

		private void CheckOrientation()
		{
			_lastResolution = new Vector2(Screen.width, Screen.height);
			bool isLandscape = Screen.width > Screen.height;

			if (_isLandscape != isLandscape)
			{
				_isLandscape = isLandscape;
				
				OrientationChanged.Invoke(_isLandscape);
			}
		}
	}
}