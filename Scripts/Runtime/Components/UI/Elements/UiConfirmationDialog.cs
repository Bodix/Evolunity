// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using TMPro;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Confirmation Dialog")]
	public class UiConfirmationDialog : UiElement
	{
		[Header("Texts")]
		[SerializeField]
		protected TMP_Text titleText;
		[SerializeField]
		protected TMP_Text messageText;

		[Header("Buttons")]
		[SerializeField]
		protected UiIconTextButton acceptButton;
		[SerializeField]
		protected UiIconTextButton declineButton;
		[SerializeField]
		protected UiButton backgroundButton;

		protected Action<Result> ResultCallback;
		private bool _hideOnBackgroundClick;

		public event Action Accepted;
		public event Action Declined;

		public TMP_Text TitleText => titleText;
		public TMP_Text MessageText => messageText;
		public UiButton AcceptButton => acceptButton;
		public UiButton DeclineButton => declineButton;
		public UiButton BackgroundButton => backgroundButton;

		protected override void Awake()
		{
			// Disable the object before base.Awake so its initial state becomes Hidden
			// to suppress warning from base class and activate animations when Show method will be invoked.
			gameObject.SetActive(false);

			base.Awake();

			acceptButton.Button.onClick.AddListener(Accept);
			declineButton.Button.onClick.AddListener(Decline);
			backgroundButton.Button.onClick.AddListener(HideByBackgroundClick);
		}

		protected virtual void OnDestroy()
		{
			if (acceptButton != null)
				acceptButton.Button.onClick.RemoveListener(Accept);

			if (declineButton != null)
				declineButton.Button.onClick.RemoveListener(Decline);

			if (backgroundButton != null)
				backgroundButton.Button.onClick.RemoveListener(HideByBackgroundClick);
		}

		public void Show(UiConfirmationDialogPayload payload, Action<Result> resultCallback,
			Action onShowComplete = null, bool instantly = false)
		{
			ApplyPayload(payload);
			SetResultCallback(resultCallback);

			base.Show(instantly, onShowComplete);
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

		protected virtual void ApplyPayload(UiConfirmationDialogPayload payload)
		{
			if (payload == null)
				return;

			_hideOnBackgroundClick = payload.HideOnBackgroundClick;

			if (titleText != null && !string.IsNullOrEmpty(payload.Title))
				titleText.text = payload.Title;

			if (messageText != null && !string.IsNullOrEmpty(payload.Message))
				messageText.text = payload.Message;

			if (acceptButton.Text != null && !string.IsNullOrEmpty(payload.AcceptButtonText))
				acceptButton.Text.text = payload.AcceptButtonText;

			if (declineButton.Text != null && !string.IsNullOrEmpty(payload.DeclineButtonText))
				declineButton.Text.text = payload.DeclineButtonText;
		}

		private void HideByBackgroundClick()
		{
			if (_hideOnBackgroundClick)
				Hide(Result.Hide);
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