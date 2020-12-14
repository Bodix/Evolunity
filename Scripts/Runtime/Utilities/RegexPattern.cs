/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

namespace Evolunity.Utilities
{
    public static class RegexPattern {
        public const string EmptyOrWhiteSpace = @"^[A-Z\s]*$";
        public const string URL = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";
        public const string EmailAddress = @"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$";
        public const string HexCode = @"^#?([a-f0-9]{6}|[a-f0-9]{3})$";
        public const string IPAddress = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        public const string HTMLTag = @"^<([a-z]+)([^<]+)*(?:>(.*)<\/\1>|\s+\/>)$";
        
        public const string ExtractFromBrackets = @"\(([^)]*)\)";
        public const string ExtractFromSquareBrackets = @"\[([^\]]*)\]";
        public const string ExtractFromCurlyBrackets = @"\{([^\}]*)\}";
    }
}