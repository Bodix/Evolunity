// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

namespace Evolutex.Evolunity.Utilities
{
    public static class Angle
    {
        /// <summary>
        /// Normalize angle within [-360, 360].
        /// </summary>
        public static float Normalize(float angle)
        {
            if (angle == -360 || angle == 360)
                return angle;
            
            return angle % 360;
        }
    }
}