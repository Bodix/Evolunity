// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    public static class TextureExtensions
    {
        public static Sprite ToSprite(this Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(texture.width / 2f, texture.height / 2f));
        }

        public static Sprite ToSprite(this Texture2D texture, float pixelsPerUnit)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(texture.width / 2f, texture.height / 2f), pixelsPerUnit);
        }

        public static Sprite ToSprite(this Texture2D texture, float pixelsPerUnit, uint extrude)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(texture.width / 2f, texture.height / 2f), pixelsPerUnit, extrude);
        }

        public static Sprite ToSprite(this Texture2D texture, float pixelsPerUnit, uint extrude,
            SpriteMeshType meshType)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(texture.width / 2f, texture.height / 2f), pixelsPerUnit, extrude,
                meshType);
        }

        public static Sprite ToSprite(this Texture2D texture, float pixelsPerUnit, uint extrude,
            SpriteMeshType meshType, Vector4 border)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(texture.width / 2f, texture.height / 2f), pixelsPerUnit, extrude,
                meshType, border);
        }

        public static Sprite ToSprite(this Texture2D texture, float pixelsPerUnit, uint extrude,
            SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(texture.width / 2f, texture.height / 2f), pixelsPerUnit, extrude,
                meshType, border, generateFallbackPhysicsShape);
        }
    }
}