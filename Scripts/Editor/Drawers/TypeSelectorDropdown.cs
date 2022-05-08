// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if UNITY_2019_1_OR_NEWER

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace Evolutex.Evolunity.Editor.Drawers
{
    public class TypeSelectorDropdown : AdvancedDropdown
    {
        private readonly IEnumerable<Type> _types;

        public TypeSelectorDropdown(IEnumerable<Type> types) : base(new AdvancedDropdownState())
        {
            _types = types;

            minimumSize = new Vector2(minimumSize.x, EditorGUIUtility.singleLineHeight * 16 + 2);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Weekdays");

            var firstHalf = new AdvancedDropdownItem("First half");
            var secondHalf = new AdvancedDropdownItem("Second half");
            var weekend = new AdvancedDropdownItem("Weekend");

            firstHalf.AddChild(new AdvancedDropdownItem("Monday"));
            firstHalf.AddChild(new AdvancedDropdownItem("Tuesday"));
            secondHalf.AddChild(new AdvancedDropdownItem("Wednesday"));
            secondHalf.AddChild(new AdvancedDropdownItem("Thursday"));
            weekend.AddChild(new AdvancedDropdownItem("Friday"));
            weekend.AddChild(new AdvancedDropdownItem("Saturday"));
            weekend.AddChild(new AdvancedDropdownItem("Sunday"));

            root.AddChild(firstHalf);
            root.AddChild(secondHalf);
            root.AddChild(weekend);

            return root;
        }
    }
}

#endif