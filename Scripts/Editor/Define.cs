/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

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