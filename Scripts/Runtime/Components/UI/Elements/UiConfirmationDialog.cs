using System;
using UnityEngine;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
{
	public class UiConfirmationDialog : UiElement
	{
		[SerializeField]
		protected Button AcceptButton;
		[SerializeField]
		protected Button DeclineButton;

		protected Action<Result> ResultCallback;

		public event Action Accepted;
		public event Action Declined;

		protected virtual void Awake()
		{
			AcceptButton.onClick.AddListener(Accept);
			DeclineButton.onClick.AddListener(Decline);
		}

		protected virtual void OnDestroy()
		{
			if (AcceptButton != null)
				AcceptButton.onClick.RemoveListener(Accept);

			if (DeclineButton != null)
				DeclineButton.onClick.RemoveListener(Decline);
		}

		public void Show(Action<Result> resultCallback, Action onShowComplete = null, bool instantly = false)
		{
			SetResultCallback(resultCallback);

			base.Show(instantly, onShowComplete);
		}

		public void Hide(Result result, Action onHideComplete = null, bool instantly = false)
		{
			InvokeAndClearResultCallback(result);

			base.Hide(instantly, onHideComplete);
		}

		protected sealed override void Show(bool instantly, Action onComplete)
		{
			Debug.LogWarning("Trying to show " + nameof(UiConfirmationDialog) + " without result callback. " +
				"Use the overload of \"Show()\" method with \"resultCallback\" parameter instead.");

			Show(null, onComplete, instantly);
		}

		protected sealed override void Hide(bool instantly, Action onComplete)
		{
			Hide(Result.Hide, onComplete, instantly);
		}

		protected void Accept()
		{
			Accepted?.Invoke();

			Hide(Result.Accept);
		}

		protected void Decline()
		{
			Declined?.Invoke();

			Hide(Result.Decline);
		}

		protected void SetResultCallback(Action<Result> resultCallback)
		{
			ResultCallback = resultCallback;
		}

		protected void InvokeAndClearResultCallback(Result result)
		{
			ResultCallback?.Invoke(result);
			ResultCallback = null;
		}

		public enum Result
		{
			Accept,
			Decline,
			Hide
		}
	}
}