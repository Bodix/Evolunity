// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Bodix.Evolunity.Collections;
using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Editor.Generators
{
	public static class ConfigsGenerator
	{
		[MenuItem("Tools/Evolunity/Generate/Generate ConfigCatalog.cs")]
		public static void Generate()
		{
			ConfigsGeneratorSettings settings = GetSettings();

			if (settings == null)
				return;

			string[] guids = AssetDatabase.FindAssets($"t:{nameof(DataAsset)}");
			List<DataAsset> configs = new List<DataAsset>();

			foreach (string guid in guids)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				DataAsset asset = AssetDatabase.LoadAssetAtPath<DataAsset>(path);

				if (asset != null && !string.IsNullOrWhiteSpace(asset.Id))
					configs.Add(asset);
			}

			GenerateFile(configs, settings);
		}

		private static ConfigsGeneratorSettings GetSettings()
		{
			string[] guids = AssetDatabase.FindAssets($"t:{nameof(ConfigsGeneratorSettings)}");

			if (guids.Length == 0)
			{
				Debug.LogError($"[{nameof(ConfigsGenerator)}] Settings not found. Please create 'ConfigsGeneratorSettings' asset in your project.");

				return null;
			}

			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			return AssetDatabase.LoadAssetAtPath<ConfigsGeneratorSettings>(path);
		}

		private static void GenerateFile(List<DataAsset> configs, ConfigsGeneratorSettings settings)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("// Auto-generated code. Do not modify manually.");
			sb.AppendLine();
			sb.AppendLine($"using {settings.ServiceNamespace};");

			// Collect all unique namespaces from config types.
			IOrderedEnumerable<string> namespaces = configs
				.Select(c => c.GetType().Namespace)
				.Where(n => !string.IsNullOrEmpty(n))
				.Distinct()
				.OrderBy(n => n);

			foreach (string ns in namespaces)
				sb.AppendLine($"using {ns};");

			sb.AppendLine();
			sb.AppendLine($"namespace {settings.GeneratedNamespace}");
			sb.AppendLine("{");
			sb.AppendLine("\tpublic class ConfigCatalog");
			sb.AppendLine("\t{");

			HashSet<string> usedFieldNames = new HashSet<string>();
			List<(DataAsset config, string fieldName)> processedConfigs = new List<(DataAsset config, string fieldName)>();

			// Generate readonly fields for each config and ensure names are unique.
			foreach (DataAsset config in configs)
			{
				string typeName = config.GetType().Name;
				string baseFieldName = FormatIdentifier(config.name);
				string fieldName = baseFieldName;
				int suffix = 1;

				// Prevent duplicate names after character stripping.
				while (usedFieldNames.Contains(fieldName))
				{
					fieldName = $"{baseFieldName}{suffix}";
					suffix++;
				}

				usedFieldNames.Add(fieldName);
				processedConfigs.Add((config, fieldName));

				sb.AppendLine($"\t\tpublic readonly {typeName} {fieldName};");
			}

			sb.AppendLine();
			sb.AppendLine($"\t\tpublic ConfigCatalog({settings.ServiceClassName} configsService)");
			sb.AppendLine("\t\t{");

			// Generate constructor assignments.
			foreach (var item in processedConfigs)
			{
				string typeName = item.config.GetType().Name;
				sb.AppendLine($"\t\t\t{item.fieldName} = configsService.GetConfig<{typeName}>(\"{item.config.Id}\");");
			}

			sb.AppendLine("\t\t}");
			sb.AppendLine("\t}");
			sb.Append("}");

			string directory = Path.GetDirectoryName(settings.OutputPath);

			if (directory != null && !Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			File.WriteAllText(settings.OutputPath, sb.ToString());
			AssetDatabase.Refresh();

			Debug.Log($"[{nameof(ConfigsGenerator)}] Generated catalog for {configs.Count} configs.");
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