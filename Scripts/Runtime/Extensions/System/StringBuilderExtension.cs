// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Linq;
using System.Text;

namespace Evolutex.Evolunity.Extensions
{
    public static class StringBuilderExtension
    {
        public static void Append(this StringBuilder stringBuilder, params string[] values)
        {
            foreach (string value in values)
            {
                stringBuilder.Append(value);
            }
        }
        
        public static void Append(this StringBuilder stringBuilder, params object[] values)
        {
            foreach (object value in values)
            {
                stringBuilder.Append(value);
            }
        }
        
        public static void Insert(this StringBuilder stringBuilder, int index, params string[] values)
        {
            foreach (string value in Enumerable.Reverse(values))
            {
                stringBuilder.Insert(index, value);
            }
        }
        
        public static void Insert(this StringBuilder stringBuilder, int index, params object[] values)
        {
            foreach (object value in Enumerable.Reverse(values))
            {
                stringBuilder.Insert(index, value);
            }
        }
    }
}