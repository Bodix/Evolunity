// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Editor.Generators
{
	public static class SpriteTagsGenerator
	{
		[MenuItem("Tools/Evolunity/Generate/Generate SpriteTags.cs")]
		public static void Generate()
		{
			SpriteTagsGeneratorSettings settings = GetSettings();

			if (settings == null)
				return;

			// Priority 1: Asset from settings.
			TMP_SpriteAsset spriteAsset = settings.TargetAsset;

			// Priority 2: Selected asset in Project window.
			if (spriteAsset == null)
				spriteAsset = Selection.activeObject as TMP_SpriteAsset;

			// Priority 3: First found asset in the project.
			if (spriteAsset == null)
			{
				string[] guids = AssetDatabase.FindAssets($"t:{nameof(TMP_SpriteAsset)}");

				if (guids.Length == 0)
				{
					Debug.LogError($"[{nameof(SpriteTagsGenerator)}] No TMP Sprite Assets found and no TargetAsset specified in settings.");
					return;
				}

				string path = AssetDatabase.GUIDToAssetPath(guids[0]);
				spriteAsset = AssetDatabase.LoadAssetAtPath<TMP_SpriteAsset>(path);
			}

			GenerateFile(spriteAsset, settings);
		}

		private static SpriteTagsGeneratorSettings GetSettings()
		{
			string[] guids = AssetDatabase.FindAssets($"t:{nameof(SpriteTagsGeneratorSettings)}");

			if (guids.Length == 0)
			{
				Debug.LogError($"[{nameof(SpriteTagsGenerator)}] Settings not found. Please create '{nameof(SpriteTagsGeneratorSettings)}' asset in your project.");
				return null;
			}

			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			return AssetDatabase.LoadAssetAtPath<SpriteTagsGeneratorSettings>(path);
		}

		private static void GenerateFile(TMP_SpriteAsset spriteAsset, SpriteTagsGeneratorSettings settings)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("// Auto-generated code. Do not modify manually.");
			sb.AppendLine();
			sb.AppendLine($"namespace {settings.GeneratedNamespace}");
			sb.AppendLine("{");
			sb.AppendLine($"\tpublic static class {settings.ClassName}");
			sb.AppendLine("\t{");

			HashSet<string> usedFieldNames = new HashSet<string>();

			// Generate string constants for each sprite and ensure names are unique.
			foreach (TMP_SpriteCharacter character in spriteAsset.spriteCharacterTable)
			{
				string baseFieldName = FormatIdentifier(character.name);
				string fieldName = baseFieldName;
				int suffix = 1;

				// Prevent duplicate names after character stripping.
				while (usedFieldNames.Contains(fieldName))
				{
					fieldName = $"{baseFieldName}{suffix}";
					suffix++;
				}

				usedFieldNames.Add(fieldName);
				sb.AppendLine($"\t\tpublic const string {fieldName} = \"<sprite name=\\\"{character.name}\\\">\";");
			}

			sb.AppendLine("\t}");
			sb.Append("}");

			string directory = Path.GetDirectoryName(settings.OutputPath);

			if (directory != null && !Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			File.WriteAllText(settings.OutputPath, sb.ToString());
			AssetDatabase.Refresh();

			Debug.Log($"[{nameof(SpriteTagsGenerator)}] Generated {usedFieldNames.Count} sprite tags into {settings.ClassName} from '{spriteAsset.name}'.");
		}

		private static string FormatIdentifier(string name)
		{
			// Replace any non-alphanumeric characters with a space.
			string cleanName = Regex.Replace(name, @"[^a-zA-Z0-9_]", " ");
			string[] parts = cleanName.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);

			for (int i = 0; i < parts.Length; i++)
				parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1);

			string result = string.Join("", parts);

			// Ensure the identifier does not start with a digit.
			if (result.Length > 0 && char.IsDigit(result[0]))
				result = "_" + result;

			return result;
		}
	}
}