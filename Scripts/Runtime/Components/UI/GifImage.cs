// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/GIF Image")]
	[RequireComponent(typeof(Image))]
	public class GifImage : MonoBehaviour
	{
		[Min(0.1f)]
		public float FramesPerSecond = 30f;

		[SerializeField]
		private Sprite[] sprites;

		[HideInInspector]
		[SerializeField]
		private Image image;

		public Sprite[] Sprites => sprites;
		public bool IsPlaying { get; private set; }
		public bool IsContainsSprites => sprites != null && sprites.Length > 0;
		private bool IsInitialized => image;

		private float _frameTimer;
		private int _currentFrameIndex;

		public void OnValidate()
		{
			InitializeIfRequired();
		}

		private void Awake()
		{
			InitializeIfRequired();
		}

		private void Start()
		{
			if (IsContainsSprites)
				Play();
		}

		private void OnEnable()
		{
			// If it was playing before being disabled, resume it visually.
			if (IsPlaying && IsContainsSprites)
				image.sprite = sprites[_currentFrameIndex];
		}

		private void OnDisable()
		{
			// Optional: We don't change IsPlaying flag here so it resumes automatically in OnEnable.
			// But we stop the Update logic natively because Unity doesn't call Update on disabled components.
		}

		private void Update()
		{
			if (!IsPlaying || !IsContainsSprites)
				return;

			_frameTimer += Time.deltaTime;
			float timePerFrame = 1f / FramesPerSecond;

			if (_frameTimer >= timePerFrame)
			{
				_frameTimer -= timePerFrame;

				_currentFrameIndex = (_currentFrameIndex + 1) % sprites.Length;

				image.sprite = sprites[_currentFrameIndex];
			}
		}

		public void SetGif(Sprite[] newSprites)
		{
			sprites = newSprites;

			ResetAnimation();
		}

		public void Play()
		{
			if (!IsContainsSprites)
			{
				Debug.LogWarning("Unable to play gif animation without sprites.", this);

				return;
			}

			IsPlaying = true;
		}

		public void Stop()
		{
			if (!IsPlaying)
			{
				Debug.LogWarning("Trying to stop a gif animation that is not playing.", this);

				return;
			}

			IsPlaying = false;
			ResetAnimation();
		}

		private void InitializeIfRequired()
		{
			if (!IsInitialized)
				image = GetComponent<Image>();
		}

		private void ResetAnimation()
		{
			_frameTimer = 0f;
			_currentFrameIndex = 0;

			if (IsContainsSprites && IsInitialized)
				image.sprite = sprites[0];
		}
	}
}