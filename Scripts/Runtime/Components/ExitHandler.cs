using Bodix.Evolunity.Components.UI;
using Bodix.Evolunity.Services;
using UnityEngine;

namespace Bodix.Evolunity.Components
{
	public abstract class ExitHandler : MonoBehaviour, IBackNavigationHandler
	{
		protected abstract UiDialogService UiDialogService { get; }
		protected virtual string TitleText { get; } = "Exit";
		protected virtual string MessageText { get; } = "Are you sure you want to exit the game?";
		protected virtual string AcceptButtonText { get; set; } = "Yes, exit";
		protected virtual string DeclineButtonText { get; set; } = "No, stay";

		public bool OnBackPressed()
		{
			UiDialogService.ShowConfirmationDialog(new UiConfirmationDialogPayload
			{
				HideOnBackgroundClick = true,
				Title = TitleText,
				Message = MessageText,
				AcceptButtonText = AcceptButtonText,
				DeclineButtonText = DeclineButtonText,
			}, result =>
			{
				if (result == UiConfirmationDialog.Result.Accept)
					Application.Quit();
			});

			return true;
		}
	}
}