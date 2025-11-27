// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Extensions
{
    public static class ArrayExtensions
    {
        public static T[] Copy<T>(this T[] array, int index, int length)
        {
            T[] newArray = new T[length];
            
            Array.Copy(array, index, newArray, 0, length);
            
            return newArray;
        }
        
        public static void SwapElements<T>(this T[] array, int firstIndex, int secondIndex)
        {
            (array[firstIndex], array[secondIndex]) = (array[secondIndex], array[firstIndex]);
        }
    }
}