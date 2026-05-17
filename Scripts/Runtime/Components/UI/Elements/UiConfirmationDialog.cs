// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Confirmation Dialog")]
	public class UiConfirmationDialog : UiElement
	{
		[SerializeField]
		protected UiButton acceptButton;
		[SerializeField]
		protected UiButton declineButton;

		protected Action<Result> ResultCallback;

		public event Action Accepted;
		public event Action Declined;

		public UiButton AcceptButton => acceptButton;
		public UiButton DeclineButton => declineButton;

		protected override void Awake()
		{
			base.Awake();

			acceptButton.Button.onClick.AddListener(Accept);
			declineButton.Button.onClick.AddListener(Decline);
		}

		protected virtual void OnDestroy()
		{
			if (acceptButton != null)
				acceptButton.Button.onClick.RemoveListener(Accept);

			if (declineButton != null)
				declineButton.Button.onClick.RemoveListener(Decline);
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