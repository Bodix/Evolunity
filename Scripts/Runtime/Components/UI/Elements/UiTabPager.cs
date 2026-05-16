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
	[AddComponentMenu("Evolunity/UI/Tab Pager")]
	public class UiTabPager : UiElement
	{
		[Serializable]
		public class PageChangeHandler : UnityEvent<int>
		{
		}

		[Tooltip("The group that manages the toggles.")]
		[SerializeField] private ToggleGroup _toggleGroup;

		[Tooltip("List of toggles representing the page tabs.")]
		[SerializeField] private List<Toggle> _tabsToggles;

		[Tooltip("List of GameObjects representing the actual pages.")]
		[SerializeField] private List<GameObject> _pages;

		public PageChangeHandler PageChanged;

		private List<UnityAction<bool>> _toggleListeners;

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
			if (pageIndex < 0 || pageIndex >= _tabsToggles.Count)
			{
				Debug.LogWarning("Invalid page index.");

				return;
			}

			// Setting isOn to true will automatically trigger the listener and disable others via ToggleGroup.
			_tabsToggles[pageIndex].isOn = true;
		}

		private void Initialize()
		{
			if (_tabsToggles.Count != _pages.Count)
			{
				Debug.LogError("Pagination Setup Failed: The number of toggles must match the number of pages.");

				return;
			}

			if (_toggleGroup == null)
				Debug.LogWarning("ToggleGroup is not assigned. Toggles may not work as radio buttons.");

			_toggleListeners = new List<UnityAction<bool>>(_tabsToggles.Count);

			for (int i = 0; i < _tabsToggles.Count; i++)
			{
				if (_toggleGroup != null)
					_tabsToggles[i].group = _toggleGroup;

				SubscribeToggleListener(i);

				// Initialize the page visibility based on the default toggle state.
				if (_pages[i] != null)
					_pages[i].SetActive(_tabsToggles[i].isOn);

				if (_tabsToggles[i].isOn)
					PageChanged?.Invoke(i);
			}
		}

		private void SubscribeToggleListener(int index)
		{
			UnityAction<bool> listener = isOn => OnToggleStateChanged(isOn, index);
			_toggleListeners.Add(listener);
			_tabsToggles[index].onValueChanged.AddListener(listener);
		}

		private void OnToggleStateChanged(bool isOn, int pageIndex)
		{
			if (pageIndex < 0 || pageIndex >= _pages.Count)
				return;

			if (_pages[pageIndex] != null)
				_pages[pageIndex].SetActive(isOn);

			if (isOn)
				PageChanged?.Invoke(pageIndex);
		}


		private void UnsubscribeToggleListeners()
		{
			if (_toggleListeners == null)
				return;

			for (int i = 0; i < _tabsToggles.Count; i++)
				if (_tabsToggles[i] != null && i < _toggleListeners.Count)
					// Unsubscribes ONLY our specific listeners to avoid breaking other scripts.
					_tabsToggles[i].onValueChanged.RemoveListener(_toggleListeners[i]);
		}
	}
}