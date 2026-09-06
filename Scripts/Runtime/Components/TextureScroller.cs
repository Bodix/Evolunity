// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Bodix.Evolunity.Components
{
	[RequireComponent(typeof(Renderer))]
	public class TextureScroller : MonoBehaviour
	{
		[Tooltip("Speed of the texture scrolling.")]
		public Vector2 ScrollSpeed = new Vector2(1f, 0f);

		[Tooltip("The name of the texture property in the shader (e.g., _BaseMap or _MainTex).")]
		public string TexturePropertyName = "_BaseMap";

		private Renderer _renderer;
		private MaterialPropertyBlock _propertyBlock;
		private int _textureStId;
		private Vector2 _currentOffset;

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
			_propertyBlock = new MaterialPropertyBlock();
			_textureStId = Shader.PropertyToID(TexturePropertyName + "_ST");
		}

		private void Update()
		{
			_currentOffset += ScrollSpeed * Time.deltaTime;

			ApplyScroll();
		}

		private void OnEnable()
		{
			ApplyScroll();
		}

		private void ApplyScroll()
		{
			if (_renderer == null)
				return;

			_renderer.GetPropertyBlock(_propertyBlock);

			// Get base tiling from the shared material to preserve it.
			Vector4 baseSt = _renderer.sharedMaterial != null
				? _renderer.sharedMaterial.GetVector(_textureStId)
				: new Vector4(1f, 1f, 0f, 0f);

			// _ST vector is (tiling.x, tiling.y, offset.x, offset.y).
			Vector4 st = new Vector4(baseSt.x, baseSt.y, _currentOffset.x, _currentOffset.y);

			_propertyBlock.SetVector(_textureStId, st);
			_renderer.SetPropertyBlock(_propertyBlock);
		}
	}
}