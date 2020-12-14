// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Evolutex.Evolunity.Editor
{
    public class Define
    {
        public const string DEVELOPMENT = "DEVELOPMENT";
        
        public static void Set(string define, bool set)
        {
            string definesString =
                PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> defines = definesString.Split(';').ToList();

            if (set)
            {
                if (!defines.Contains(define))
                    defines.Add(define);
            }
            else
                defines.Remove(define);

            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                EditorUserBuildSettings.selectedBuildTargetGroup, 
                string.Join(";", defines));
        }
    }
}