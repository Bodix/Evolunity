// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class ByteArrayExtensions
    {
        public static Texture2D ToTexture(this byte[] bytes, bool markNonReadable = false)
        {
            Texture2D texture = new Texture2D(0, 0);
            
            texture.LoadImage(bytes, markNonReadable);

            return texture;
        }

        public static Sprite ToSprite(this byte[] bytes, bool markNonReadable = false)
        {
            return bytes.ToTexture(markNonReadable).ToSprite();
        }
    }
}