// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Editor.Generators
{
	/// <summary>
	/// Project-specific settings for the config catalog generator.
	/// </summary>
	[CreateAssetMenu(fileName = "ConfigsGeneratorSettings", menuName = "Evolunity/Configs Generator Settings")]
	public class ConfigsGeneratorSettings : ScriptableObject
	{
		public string OutputPath = "Assets/Game/Scripts/Generated/ConfigCatalog.cs";
		public string GeneratedNamespace = "ProjectNamespace.Generated";
		public string ServiceNamespace = "ProjectNamespace.Services";
		public string ServiceClassName = "ConfigService";
	}
}