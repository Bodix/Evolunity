// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Reflection;

namespace Evolutex.Evolunity.Editor.Utilities
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