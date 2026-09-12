using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Generates items from a weighted pool of entries.
	/// </summary>
	[Serializable]
	public abstract class WeightedPoolDrop<T, TEntry> : LootDrop<T> where TEntry : WeightedPoolEntry<T>
	{
		[Min(0)]
		public int Rolls = 1;

		[Min(0f)]
		public float EmptyRollWeight = 0f;

		public List<TEntry> Pool = new List<TEntry>();

		public override bool IsValid()
		{
			if (!base.IsValid())
				return false;

			if (Rolls < 0 || EmptyRollWeight < 0f)
			{
				Debug.LogError($"[{nameof(WeightedPoolDrop<T, TEntry>)}] Invalid rolls or empty weight configuration.");

				return false;
			}

			float totalWeight = EmptyRollWeight;
			foreach (TEntry entry in Pool)
			{
				if (entry == null || entry.Item == null || entry.Weight < 0f || entry.MinCount < 0 || entry.MaxCount < entry.MinCount)
				{
					Debug.LogError($"[{nameof(WeightedPoolDrop<T, TEntry>)}] Invalid entry configuration encountered in the pool.");

					return false;
				}

				totalWeight += entry.Weight;
			}

			if (Rolls > 0 && totalWeight <= 0f)
			{
				Debug.LogError($"[{nameof(WeightedPoolDrop<T, TEntry>)}] Total weight of the weighted pool is 0. Cannot generate weighted loot.");

				return false;
			}

			return true;
		}

		public override bool TryGenerate(List<LootResult<T>> results, LootContext context)
		{
			for (int i = 0; i < Rolls; i++)
			{
				TEntry entry = WeightedPool.Pick(Pool, EmptyRollWeight);
				if (entry == null)
					continue;

				int count = entry.RandomCount;
				if (count > 0)
					results.Add(new LootResult<T>(entry.Item, count));
			}

			return true;
		}

		public override void OnValidate()
		{
			base.OnValidate();

			if (Pool == null)
				return;

			// Fixes newly added nested entries inside the weighted pool.
			foreach (TEntry entry in Pool)
				entry?.ResetIfEmpty();
		}
	}
}
