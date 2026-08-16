using System;
using UnityEngine;

namespace Bodix.Evolunity.Components.UI
{
	public class UiConfirmationDialogPayload : IEquatable<UiConfirmationDialogPayload>
	{
		public string Title;
		public string Message;
		public string AcceptButtonText;
		public string DeclineButtonText;
		public bool HideOnBackgroundClick = false;
		
		public bool ShowAcceptIcon = true;
		public bool ShowDeclineIcon = true;
		public Sprite AcceptIcon;
		public Sprite DeclineIcon;

		public bool Equals(UiConfirmationDialogPayload other)
		{
			if (other == null)
				return false;

			return Title == other.Title
				&& Message == other.Message
				&& AcceptButtonText == other.AcceptButtonText
				&& DeclineButtonText == other.DeclineButtonText
				&& HideOnBackgroundClick == other.HideOnBackgroundClick
				&& ShowAcceptIcon == other.ShowAcceptIcon
				&& ShowDeclineIcon == other.ShowDeclineIcon
				&& AcceptIcon == other.AcceptIcon
				&& DeclineIcon == other.DeclineIcon;
		}
	}
}