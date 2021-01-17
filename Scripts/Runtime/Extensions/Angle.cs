// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

namespace Evolutex.Evolunity.Extensions
{
    public static class Angle
    {
        public static float Normalize(float angle)
        {
            while (angle > 360)
                angle -= 360;
            
            while (angle < 0)
                angle += 360;
            
            return angle;
        }
    }
}