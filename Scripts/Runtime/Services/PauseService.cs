using System;
using UnityEngine;

namespace Bodix.Evolunity.Services
{
	public sealed class PauseService
	{
		public bool IsPaused { get; private set; }

		public event Action<bool> PauseStateChanged;

		public void SetPause(bool isPaused)
		{
			if (IsPaused == isPaused)
				return;

			Time.timeScale = isPaused ? 0f : 1f;
			IsPaused = isPaused;

			PauseStateChanged?.Invoke(isPaused);
		}

		public void TogglePause()
		{
			SetPause(!IsPaused);
		}
	}
}