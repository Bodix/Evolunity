// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Extensions
{
    // https://docs.unity3d.com/ru/2019.4/Manual/StyledText.html
    // https://github.com/zuizuii/RichText-Extension/blob/master/Assets/RichText%20Extension/Runtime/RichTextExtension.cs
    // https://github.com/HilamGhost/String-Extensions-For-Unity-Rich-Texts/blob/main/StringExtensionsForRichTexts.cs
    public static class RichTextExtensions
    {
        public static string Bold(this string text)
        {
            return $"<b>{text}</b>";
        }

        public static string Italic(this string text)
        {
            return $"<i>{text}</i>";
        }

        public static string Size(this string text, int size)
        {
            return $"<size={size}>{text}</size>";
        }

        public static string Color(this string text, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>";
        }

        public static string Color(this string text, RichTextColor color)
        {
            return $"<color={color}>{text}</color>";
        }

        public static string Color(this string text, string hexColor)
        {
            return $"<color={hexColor}>{text}</color>";
        }
    }

    // ReSharper disable InconsistentNaming
    public enum RichTextColor
    {
        aqua, // #00ffffff (same as cyan)
        black, // #000000ff
        blue, // #0000ffff
        brown, // #a52a2aff
        cyan, // #00ffffff (same as aqua)
        darkblue, // #0000a0ff
        fuchsia, // #ff00ffff (same as magenta)
        green, // #008000ff
        grey, // #808080ff
        lightblue, // #add8e6ff
        lime, // #00ff00ff
        magenta, // #ff00ffff (same as fuchsia)
        maroon, // #800000ff
        navy, // #000080ff
        olive, // #808000ff
        orange, // #ffa500ff
        purple, // #800080ff
        red, // #ff0000ff
        silver, // #c0c0c0ff
        teal, // #008080ff
        white, // #ffffffff
        yellow // #ffff00ff
    }
}