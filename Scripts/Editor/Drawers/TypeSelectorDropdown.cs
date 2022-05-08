// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

#if UNITY_2019_3_OR_NEWER

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Evolutex.Evolunity.Editor.Drawers
{
    public class TypeSelectorDropdown : AdvancedDropdown
    {
        private readonly IEnumerable<Type> _types;

        public TypeSelectorDropdown(IEnumerable<Type> types) : base(new AdvancedDropdownState())
        {
            _types = types;
        }

        public event Action<Type> TypeSelected;

        protected override AdvancedDropdownItem BuildRoot()
        {
            AdvancedDropdownItem root = new AdvancedDropdownItem("Select type");

            foreach (Type type in _types)
                root.AddChild(new TypeSelectorDropdownItem(type));

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            if (item is TypeSelectorDropdownItem typeSelectorItem)
                TypeSelected?.Invoke(typeSelectorItem.Type);
        }

        private class TypeSelectorDropdownItem : AdvancedDropdownItem
        {
            public TypeSelectorDropdownItem(Type type) : base(ObjectNames.NicifyVariableName(type.Name))
            {
                Type = type;
            }

            public Type Type { get; }
        }
    }
}

#endif