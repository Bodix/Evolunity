using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	/// <summary>
	/// Contains screen aspect ratio boundaries. Used primarily for UI interpolation.
	/// </summary>
	// [CreateAssetMenu(fileName = nameof(AspectRatioBounds), menuName = "UI/Aspect Ratio Bounds")]
	public class AspectRatioBounds : ScriptableObject
	{
		[Tooltip("The widest supported aspect ratio. 2520:1080 (7:3) = 2.333333")]
		public float WidestRatio = 7f / 3f;

		[Tooltip("The narrowest supported aspect ratio. 2048:1536 (4:3) = 1.333333")]
		public float NarrowestRatio = 4f / 3f;
	}
}