using System;

namespace Bodix.Evolunity.Components.UI
{
	public class UiConfirmationDialogPayload : IEquatable<UiConfirmationDialogPayload>
	{
		public string Title;
		public string Message;
		public string AcceptButtonText;
		public string DeclineButtonText;
		public bool HideOnBackgroundClick = false;

		public bool Equals(UiConfirmationDialogPayload other)
		{
			if (other == null)
				return false;

			return Title == other.Title
				&& Message == other.Message
				&& AcceptButtonText == other.AcceptButtonText
				&& DeclineButtonText == other.DeclineButtonText;
		}
	}
}