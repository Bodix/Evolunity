// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved.

using TMPro;
using UnityEngine;

namespace Bodix.Evolunity.Editor.Generators
{
	/// <summary>
	/// Project-specific settings for the sprite tags generator.
	/// </summary>
	[CreateAssetMenu(fileName = "SpriteTagsGeneratorSettings", menuName = "Evolunity/Sprite Tags Generator Settings")]
	public class SpriteTagsGeneratorSettings : ScriptableObject
	{
		public string OutputPath = "Assets/Game/Scripts/Generated/SpriteTags.cs";
		public string GeneratedNamespace = "ProjectNamespace.Generated";
		public string ClassName = "SpriteTags";
		
		[Tooltip("Target TMP_SpriteAsset to generate tags from. If empty, it will use the selected asset in the Project window.")]
		public TMP_SpriteAsset TargetAsset;
	}
}