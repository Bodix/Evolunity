// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.IO;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Evolutex.Evolunity.Collections
{
	public abstract class DatabaseEntry : ScriptableObject
	{
		[Tooltip("Unique string ID in 'namespace:name' format")]
		[SerializeField, ReadOnly] private string _id;

		public string Id => _id;

#if UNITY_EDITOR
		protected virtual void OnValidate()
		{
			string expectedId = name.Replace(" ", "_").ToLower();
			string assetPath = AssetDatabase.GetAssetPath(this);

			if (!string.IsNullOrEmpty(assetPath))
			{
				// ReSharper disable once AssignNullToNotNullAttribute
				string parentFolder = new DirectoryInfo(Path.GetDirectoryName(assetPath)).Name.ToLower();

				expectedId = $"{parentFolder}:{expectedId}";
			}

			if (_id != expectedId)
			{
				_id = expectedId;

				EditorUtility.SetDirty(this);
			}
		}
#endif
	}
}