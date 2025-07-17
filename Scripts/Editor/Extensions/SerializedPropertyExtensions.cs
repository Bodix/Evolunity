// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Reflection;
using UnityEditor;

namespace Evolutex.Evolunity.Editor.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static Type GetManagedReferenceFieldType(this SerializedProperty serializedProperty)
        {
            return GetManagedReferenceType(serializedProperty.managedReferenceFieldTypename);
        }

        public static Type GetManagedReferenceValueType(this SerializedProperty serializedProperty)
        {
            return GetManagedReferenceType(serializedProperty.managedReferenceFullTypename);
        }

        public static object CreateManagedReferenceValue(this SerializedProperty serializedProperty, Type type)
        {
            object obj = type != null ? Activator.CreateInstance(type) : null;
            serializedProperty.managedReferenceValue = obj;

            return obj;
        }

        private static Type GetManagedReferenceType(string managedReferenceTypename)
        {
            if (string.IsNullOrEmpty(managedReferenceTypename))
                return null;

            int splitIndex = managedReferenceTypename.IndexOf(' ');
            Assembly assembly = Assembly.Load(managedReferenceTypename.Substring(0, splitIndex));

            return assembly.GetType(managedReferenceTypename.Substring(splitIndex + 1));
        }
    }
}