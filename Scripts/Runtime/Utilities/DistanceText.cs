// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Globalization;

namespace Bodix.Evolunity.Utilities
{
	/// <summary>
	/// Distance as text: meters up to a kilometer, then kilometers, e.g. for markers on a HUD.
	/// </summary>
	public static class DistanceText
	{
		/// <summary>
		/// Rounds the distance to the step it is shown with, so a marker updates its text only when the text changes.
		/// </summary>
		public static int Round(float meters)
		{
			if (meters < 1000f)
				return (int)(meters / 10f) * 10;

			if (meters < 10000f)
				return (int)(meters / 100f) * 100;

			return (int)(meters / 1000f) * 1000;
		}

		/// <param name="meters">Distance rounded by <see cref="Round" />.</param>
		public static string Format(int meters)
		{
			if (meters < 1000)
				return string.Format(CultureInfo.InvariantCulture, "{0} m", meters);

			if (meters < 10000)
				return string.Format(CultureInfo.InvariantCulture, "{0:0.0} km", meters / 1000f);

			return string.Format(CultureInfo.InvariantCulture, "{0} km", meters / 1000);
		}
	}
}
