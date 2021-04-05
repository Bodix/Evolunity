﻿// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Extensions
{
    public static class ToggleGroupExtensions
    {
        private static FieldInfo m_TogglesField;
        
        // https://forum.unity.com/threads/how-to-get-reference-of-all-toggles-from-togglegroup.463534/
        // https://github.com/Unity-Technologies/uGUI/blob/2019.1/UnityEngine.UI/UI/Core/ToggleGroup.cs
        public static List<Toggle> GetToggles(this ToggleGroup toggleGroup)
        {
            if (m_TogglesField == null)
            {
                m_TogglesField = typeof(ToggleGroup)
                    .GetField("m_Toggles", BindingFlags.Instance | BindingFlags.NonPublic);
                if (m_TogglesField == null)
                    throw new Exception(
                        "UnityEngine.UI.ToggleGroup source code must have changed in latest version " +
                        "and is no longer compatible with this version of code.");
            }

            return m_TogglesField.GetValue(toggleGroup) as List<Toggle>;
        }
    }
}