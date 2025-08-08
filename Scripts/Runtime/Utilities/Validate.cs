// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Net;
using Evolutex.Evolunity.Extensions;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    // TODO:
    // 1. Password (regex).
    // 2. Email (regex).

    public static class Validate
    {
        public static bool InternetReachability()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }

        /// <summary>
        /// Avoid use UriKind.Relative or UriKind.RelativeOrAbsolute because most of the strings are valid URLs.
        /// For example, "asdasd", "/", and "?????" are valid in this case.
        /// Also see <a href="https://mathiasbynens.be/demo/url-regex/">Regex versions</a>
        /// </summary>
        public static bool Url(string url, UriKind urlKind = UriKind.Absolute)
        {
            return !url.IsEmpty() // Uri.IsWellFormedUriString with UriKind.Relative or UriKind.RelativeOrAbsolute returns "true" for empty string ("").
                && (Uri.IsWellFormedUriString(url, urlKind) || IPAddress(url));
        }

        public static bool IPAddress(string ipAddress)
        {
            return System.Net.IPAddress.TryParse(ipAddress, out IPAddress _);
        }
    }
}