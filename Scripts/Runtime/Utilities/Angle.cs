// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

namespace Evolutex.Evolunity.Utilities
{
    public static class Angle
    {
        public static float Normalize(float angle)
        {
            return (360 + (Math.Sign(angle) * (Math.Abs(angle) % 360))) % 360;
        }
    }
}