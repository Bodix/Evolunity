using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	/// <summary>
	/// Base class for aspect ratio interpolation. 
	/// Calculates the interpolation factor based on screen aspect ratio.
	/// </summary>
	public abstract class AspectRatioInterpolator : MonoBehaviour
	{
		[SerializeField]
		protected AspectRatioBounds aspectRatioBounds;

		protected virtual void Awake()
		{
			ApplyInterpolation();
		}

		public void ApplyInterpolation()
		{
			if (aspectRatioBounds == null)
			{
				Debug.LogWarning("Ratio bounds are missing. Cannot interpolate.");

				return;
			}

			float currentAspectRatio = (float)Screen.width / Screen.height;
			float t = Mathf.InverseLerp(aspectRatioBounds.WidestRatio, aspectRatioBounds.NarrowestRatio, currentAspectRatio);

			InterpolateAndApply(t);
		}

		/// <summary>
		/// Applies interpolated values based on the calculated factor.
		/// </summary>
		/// <param name="t">Interpolation factor between 0 (widest) and 1 (narrowest).</param>
		protected abstract void InterpolateAndApply(float t);
	}
}