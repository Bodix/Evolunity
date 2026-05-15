// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Evolutex.Evolunity.Components.UI
{
	public class TimerText : MonoBehaviour
	{
		public TimeUpdateMethod UpdateMethod;
		[SerializeField, ShowIf(nameof(IsTimersUpdate))]
		protected Timer _timer;
		[SerializeField]
		protected TextMeshProUGUI _text;

		public Timer Timer => _timer;
		public TextMeshProUGUI Text => _text;
		public bool IsSubscribedToTimer { get; private set; }
		public virtual TimerTextHandler TimerTextHandler { protected get; set; } =
			(text, time) => text.text = ToStringUtility.TimeToMinutesSeconds(time);
		public virtual TimeGetter TimeGetter { protected get; set; }
		private bool IsTimersUpdate => UpdateMethod == TimeUpdateMethod.TimerUpdate;

		protected virtual void OnEnable()
		{
			if (_timer)
				SubscribeToTimer(_timer);
		}

		protected virtual void OnDisable()
		{
			if (_timer)
				UnsubscribeFromTimer();
		}

		protected virtual void Update()
		{
			if (UpdateMethod != TimeUpdateMethod.InternalUpdateFromGetter)
				return;

			if (TimeGetter == null)
			{
				Debug.LogError("In order to use " + nameof(TimeUpdateMethod.InternalUpdateFromGetter) + ", " +
					"it is required that " + nameof(TimeGetter) + " is not null", this);

				return;
			}

			UpdateTimerText(TimeGetter.Invoke());
		}

		public void SubscribeToTimer(Timer timer)
		{
			if (!IsSubscribedToTimer)
			{
				_timer = timer;
				_timer.Updated += OnTimerUpdated;

				IsSubscribedToTimer = true;
				UpdateMethod = TimeUpdateMethod.TimerUpdate;
			}
			else
			{
				Debug.LogError("Already subscribed to timer: " + _timer, this);
			}
		}

		public void UnsubscribeFromTimer()
		{
			if (IsSubscribedToTimer)
			{
				_timer.Updated -= OnTimerUpdated;

				IsSubscribedToTimer = false;
			}
			else
			{
				Debug.LogError(nameof(TimerText) + " is not subscribed to any timer", this);
			}
		}

		private void OnTimerUpdated(float deltaTime)
		{
			if (UpdateMethod == TimeUpdateMethod.TimerUpdate)
				UpdateTimerText(_timer.RemainingTime);
		}

		private void UpdateTimerText(float time)
		{
			TimerTextHandler.Invoke(Text, time);
		}

		public enum TimeUpdateMethod
		{
			TimerUpdate,
			InternalUpdateFromGetter
		}
	}

	public delegate void TimerTextHandler(TextMeshProUGUI text, float time);

	public delegate float TimeGetter();

	public static class ToStringUtility
	{
		public static string TimeToMinutesSeconds(float time)
		{
			float minutes = Mathf.Max(Mathf.FloorToInt(time / 60), 0);
			float seconds = Mathf.Max(Mathf.FloorToInt(time % 60), 0);

			return $"{minutes:00}:{seconds:00}";
		}
	}
}