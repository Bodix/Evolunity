// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    // TODO:
    // 1. Password (regex).
    // 2. Email (regex).
    
    public static class Validate
    {
        public static bool InternetConnection()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }
}