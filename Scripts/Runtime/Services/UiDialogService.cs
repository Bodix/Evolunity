// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using System.Linq;
using Bodix.Evolunity.Components.UI;
using UnityEngine;

namespace Bodix.Evolunity.Services
{
	public class UiDialogService : MonoBehaviour
	{
		[SerializeField]
		protected UiConfirmationDialog confirmationDialogPrefab;
		[SerializeField]
		protected Transform dialogsParent;

		private readonly Queue<UiDialogRequest> _dialogQueue = new Queue<UiDialogRequest>();
		private UiDialogRequest _activeRequest;

		public void ShowConfirmationDialog(UiConfirmationDialogPayload payload, Action<UiConfirmationDialog.Result> onResult)
		{
			if (IsDuplicateRequest(payload))
			{
				Debug.Log("An attempt to open a duplicate dialog box was blocked.");

				return;
			}

			UiDialogRequest request = new UiDialogRequest
			{
				Payload = payload,
				OnResultCallback = onResult
			};
			_dialogQueue.Enqueue(request);

			ProcessQueue();
		}

		private bool IsDuplicateRequest(UiConfirmationDialogPayload payload)
		{
			if (_activeRequest != null && _activeRequest.Payload.Equals(payload))
				return true;

			return _dialogQueue.Any(request => request.Payload.Equals(payload));
		}

		private void ProcessQueue()
		{
			if (_activeRequest != null || _dialogQueue.Count == 0)
				return;

			_activeRequest = _dialogQueue.Dequeue();

			UiConfirmationDialog dialog = Instantiate(confirmationDialogPrefab, dialogsParent);

			dialog.Show(_activeRequest.Payload, result =>
			{
				_activeRequest.OnResultCallback?.Invoke(result);

				Destroy(dialog.gameObject);

				_activeRequest = null;
				ProcessQueue();
			});
		}

		private class UiDialogRequest
		{
			public UiConfirmationDialogPayload Payload;
			public Action<UiConfirmationDialog.Result> OnResultCallback;
		}
	}
}