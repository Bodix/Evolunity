// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Bodix.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Tabs Group")]
	public class UiTabsGroup : UiElement
	{
		[Serializable]
		public class PageChangeHandler : UnityEvent<int>
		{
		}

		// Defines an event to handle the change of the "any tab is active" state.
		[Serializable]
		public class HasActiveTabChangeHandler : UnityEvent<bool>
		{
		}

		[Tooltip("The group that manages the toggles.")]
		[SerializeField]
		protected ToggleGroup toggleGroup;
		[Tooltip("List of toggles representing the page tabs.")]
		[SerializeField]
		protected List<UiToggle> tabsToggles;
		[Tooltip("List of GameObjects representing the actual pages.")]
		[SerializeField]
		protected List<GameObject> pages;

		public PageChangeHandler PageChanged;
		public HasActiveTabChangeHandler HasActiveTabChanged;

		private List<UnityAction<bool>> _toggleListeners;

		private bool _wasAnyTabActive;

		protected override void Awake()
		{
			base.Awake();

			Initialize();
		}

		private void OnDestroy()
		{
			UnsubscribeToggleListeners();
		}

		public void SetPage(int pageIndex)
		{
			if (pageIndex < 0 || pageIndex >= tabsToggles.Count)
			{
				Debug.LogWarning("Invalid page index.");

				return;
			}

			// Setting isOn to true will automatically trigger the listener and disable others via ToggleGroup.
			tabsToggles[pageIndex].Toggle.isOn = true;
		}

		private void Initialize()
		{
			if (tabsToggles.Count != pages.Count)
			{
				Debug.LogError("Pagination Setup Failed: The number of toggles must match the number of pages.");

				return;
			}

			if (toggleGroup == null)
				Debug.LogWarning("ToggleGroup is not assigned. Toggles may not work as radio buttons.");

			_toggleListeners = new List<UnityAction<bool>>(tabsToggles.Count);

			for (int i = 0; i < tabsToggles.Count; i++)
			{
				if (toggleGroup != null)
					tabsToggles[i].Toggle.group = toggleGroup;

				SubscribeToggleListener(i);

				// Initialize the page visibility based on the default toggle state.
				if (pages[i] != null)
					pages[i].SetActive(tabsToggles[i].Toggle.isOn);

				if (tabsToggles[i].Toggle.isOn)
					PageChanged?.Invoke(i);
			}

			// Initialize the tracking variable without invoking the event to avoid unexpected behavior on Awake.
			_wasAnyTabActive = IsAnyTabActive();
		}

		private void SubscribeToggleListener(int index)
		{
			UnityAction<bool> listener = isOn => OnToggleStateChanged(isOn, index);
			_toggleListeners.Add(listener);
			tabsToggles[index].Toggle.onValueChanged.AddListener(listener);
		}

		private void OnToggleStateChanged(bool isOn, int pageIndex)
		{
			if (pageIndex < 0 || pageIndex >= pages.Count)
				return;

			if (pages[pageIndex] != null)
				pages[pageIndex].SetActive(isOn);

			if (isOn)
				PageChanged?.Invoke(pageIndex);

			CheckAnyTabActiveState();
		}
		
		private void CheckAnyTabActiveState()
		{
			bool isAnyTabActive = IsAnyTabActive();
			if (isAnyTabActive != _wasAnyTabActive)
			{
				_wasAnyTabActive = isAnyTabActive;
				HasActiveTabChanged?.Invoke(isAnyTabActive);
			}
		}

		/// <summary>
		/// Helper method to determine if at least one tab is currently active.
		/// </summary>
		private bool IsAnyTabActive()
		{
			foreach (UiToggle toggle in tabsToggles)
				if (toggle.Toggle.isOn)
					return true;

			return false;
		}

		private void UnsubscribeToggleListeners()
		{
			if (_toggleListeners == null)
				return;

			for (int i = 0; i < tabsToggles.Count; i++)
				if (tabsToggles[i] != null && i < _toggleListeners.Count)
					// Unsubscribes ONLY our specific listeners to avoid breaking other scripts.
					tabsToggles[i].Toggle.onValueChanged.RemoveListener(_toggleListeners[i]);
		}
	}
}