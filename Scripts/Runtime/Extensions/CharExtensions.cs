/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

namespace Evolutex.Evolunity.Extensions
{
    public static class CharExtensions
    {
        public static bool IsDigit(this char @char) =>
            char.IsDigit(@char);

        public static bool IsLetter(this char @char) =>
            char.IsLetter(@char);

        public static bool IsLetterOrDigit(this char @char) =>
            char.IsLetterOrDigit(@char);

        public static bool IsSymbol(this char @char) =>
            char.IsSymbol(@char);

        public static bool IsWhiteSpace(this char @char) =>
            char.IsWhiteSpace(@char);

        public static bool IsLower(this char @char) =>
            char.IsLower(@char);

        public static bool IsUpper(this char @char) =>
            char.IsUpper(@char);

        public static char ToLower(this char @char) =>
            char.ToLower(@char);

        public static char ToUpper(this char @char) =>
            char.ToUpper(@char);
    }
}