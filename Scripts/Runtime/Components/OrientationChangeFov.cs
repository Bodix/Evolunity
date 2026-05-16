using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public class OrientationChangeFov : MonoBehaviour
	{
		public Camera Camera;
		public float PortraitFov = 90;
		public float LandscapeFov = 60;

		[SerializeField]
		private OrientationChangeListener _orientationListener;

		private void Awake()
		{
			_orientationListener.OrientationChanged.AddListener(ChangeCameraFov);
		}

		private void OnDestroy()
		{
			_orientationListener.OrientationChanged.RemoveListener(ChangeCameraFov);
		}

		private void ChangeCameraFov(bool isLandscape)
		{
			Camera.fieldOfView = isLandscape ? LandscapeFov : PortraitFov;
		}
	}
}