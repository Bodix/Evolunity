// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Evolutex.Evolunity.Components.UI
{
	[AddComponentMenu("Evolunity/UI/Pagination")]
	public class Pagination : MonoBehaviour
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

		private void Awake()
		{
			Initialize();
		}

		private void OnDestroy()
		{
			foreach (Toggle toggle in _tabsToggles)
				if (toggle != null)
					toggle.onValueChanged.RemoveAllListeners();
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

			for (int i = 0; i < _tabsToggles.Count; i++)
			{
				// Cache the index for the lambda expression.
				int index = i;

				if (_toggleGroup != null)
					_tabsToggles[i].group = _toggleGroup;

				_tabsToggles[i].onValueChanged.AddListener(isOn => OnToggleStateChanged(isOn, index));

				// Initialize the page visibility based on the default toggle state.
				_pages[i].SetActive(_tabsToggles[i].isOn);

				if (_tabsToggles[i].isOn)
					PageChanged?.Invoke(index);
			}
		}

		private void OnToggleStateChanged(bool isOn, int pageIndex)
		{
			if (pageIndex < 0 || pageIndex >= _pages.Count)
				return;

			_pages[pageIndex].SetActive(isOn);

			if (isOn)
				PageChanged?.Invoke(pageIndex);
		}
	}
}