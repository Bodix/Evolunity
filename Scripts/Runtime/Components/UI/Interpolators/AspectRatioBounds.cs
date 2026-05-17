using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	/// <summary>
	/// Contains screen aspect ratio boundaries. Used primarily for UI interpolation.
	/// </summary>
	[CreateAssetMenu(fileName = "AspectRatioBounds", menuName = "UI/Aspect Ratio Bounds")]
	public class AspectRatioBounds : ScriptableObject
	{
		[Tooltip("The widest supported aspect ratio.")]
		public float WidestRatio = 7f / 3f;

		[Tooltip("The narrowest supported aspect ratio.")]
		public float NarrowestRatio = 4f / 3f;
	}
}