// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved.

using System.IO;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Base class for all data assets with an auto-generated unique identifier.
	/// </summary>
	public abstract class DataAsset : ScriptableObject
	{
		[Tooltip("Unique string ID in 'namespace:name' format")]
		[SerializeField] 
		private string _id;

		public string Id => _id;

#if UNITY_EDITOR
		protected virtual void OnValidate()
		{
			if (string.IsNullOrWhiteSpace(_id))
			{
				GenerateId();
			}
		}

		[Button("Regenerate ID")]
		public void GenerateId()
		{
			string expectedName = name.Replace(" ", "_").ToLower();
			string assetPath = AssetDatabase.GetAssetPath(this);

			if (!string.IsNullOrEmpty(assetPath))
			{
				string parentFolder = Path.GetFileName(Path.GetDirectoryName(assetPath))?.ToLower();

				_id = $"{parentFolder}:{expectedName}";
			}
			else
			{
				_id = expectedName;
			}

			EditorUtility.SetDirty(this);
		}
#endif
	}
}