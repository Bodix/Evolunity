// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using System.Linq;
using Bodix.Evolunity.Collections;
using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Editor.Generators
{
	public static class DataAssetsIdGenerator
	{
		[MenuItem("Tools/Evolunity/Generate/Generate All DataAssets IDs")]
		public static void GenerateAll()
		{
			string[] guids = AssetDatabase.FindAssets($"t:{nameof(DataAsset)}");

			ProcessGuids(guids);
		}

		[MenuItem("Assets/Generate DataAssets IDs", false, 40)]
		public static void GenerateSelected()
		{
			List<string> targetGuids = new List<string>();

			foreach (string selectedGuid in Selection.assetGUIDs)
			{
				string path = AssetDatabase.GUIDToAssetPath(selectedGuid);

				if (AssetDatabase.IsValidFolder(path))
				{
					string[] folderGuids = AssetDatabase.FindAssets($"t:{nameof(DataAsset)}", new[] { path });

					targetGuids.AddRange(folderGuids);
				}
				else
				{
					DataAsset asset = AssetDatabase.LoadAssetAtPath<DataAsset>(path);

					if (asset != null)
						targetGuids.Add(selectedGuid);
				}
			}

			// Remove duplicates if folders and files were selected together.
			ProcessGuids(targetGuids.Distinct().ToArray());
		}

		private static void ProcessGuids(string[] guids)
		{
			int updatedCount = 0;

			foreach (string guid in guids)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				DataAsset asset = AssetDatabase.LoadAssetAtPath<DataAsset>(path);

				if (asset != null)
				{
					// Generate a new ID and mark the asset as dirty.
					asset.GenerateId();
					updatedCount++;
				}
			}

			AssetDatabase.SaveAssets();
			Debug.Log($"[{nameof(DataAssetsIdGenerator)}] Regenerated IDs for {updatedCount} assets.");
		}
	}
}