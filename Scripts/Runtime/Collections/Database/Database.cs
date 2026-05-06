// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using UnityEngine;

namespace Evolutex.Evolunity.Collections
{
	public abstract class Database<T> : ScriptableObject where T : DatabaseEntry
	{
		[SerializeField]
		protected List<T> Items = new List<T>();

		private Dictionary<string, T> _itemsById;

		public void Initialize()
		{
			_itemsById = new Dictionary<string, T>(Items.Count);

			foreach (T item in Items)
			{
				if (item == null)
					continue;

				if (_itemsById.ContainsKey(item.Id))
					Debug.LogError($"[{nameof(Database<T>)}] Duplicate ID found: {item.Id} on object {item.name}.");
				else
					_itemsById.Add(item.Id, item);
			}
		}

		public T GetById(string id)
		{
			if (_itemsById == null)
				Initialize();

			// ReSharper disable once PossibleNullReferenceException
			if (_itemsById.TryGetValue(id, out T result))
				return result;

			Debug.LogWarning($"[{nameof(Database<T>)}] Item with ID {id} not found!");
			return null;
		}
	}
}