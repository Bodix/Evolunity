using System;
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

		public void ShowConfirmationDialog(UiConfirmationDialogPayload payload,
			Action<UiConfirmationDialog.Result> onResult)
		{
			UiConfirmationDialog dialog = Instantiate(confirmationDialogPrefab, dialogsParent);

			dialog.Show(payload, result =>
			{
				onResult?.Invoke(result);

				Destroy(dialog.gameObject);
			});
		}
	}
}