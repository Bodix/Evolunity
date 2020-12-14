/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using System.Reflection;

namespace Evolutex.Evolunity.Editor
{
    public static class EditorConsole
    {
        private static MethodInfo clearMethod;
        private static MethodInfo ClearMethod
        {
            get
            {
                if (clearMethod == null)
                {
                    Assembly editorAssembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
                    Type logEntriesType = editorAssembly.GetType("UnityEditor.LogEntries");
                    
                    clearMethod = logEntriesType.GetMethod("Clear");
                }

                return clearMethod;
            }
        }

        public static void Clear()
        {
            ClearMethod.Invoke(null, null);
        }
    }
}