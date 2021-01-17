// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class ByteArrayExtensions
    {
        public static Sprite ToSprite(this byte[] bytes)
        {
            Texture2D texture = new Texture2D(0, 0);
            texture.LoadImage(bytes);
            
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
                new Vector2(texture.width / 2f, texture.height / 2f));
        }
    }
}