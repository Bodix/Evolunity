// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Weighted random picking over a pool of <see cref="WeightedPoolEntry" />.
	/// </summary>
	public static class WeightedPool
	{
		/// <summary>
		/// Picks a single entry with a chance proportional to its weight.
		/// <paramref name="emptyWeight" /> takes part in the roll and makes the method return null when it wins.
		/// </summary>
		public static TEntry Pick<TEntry>(IReadOnlyList<TEntry> pool, float emptyWeight = 0f)
			where TEntry : WeightedPoolEntry
		{
			if (pool == null)
				return null;

			float totalWeight = emptyWeight;
			for (int i = 0; i < pool.Count; i++)
				if (pool[i] != null)
					totalWeight += pool[i].Weight;

			if (totalWeight <= 0f)
				return null;

			float roll = Random.Range(0f, totalWeight);
			if (roll < emptyWeight)
				return null;

			float passedWeight = emptyWeight;
			for (int i = 0; i < pool.Count; i++)
			{
				TEntry entry = pool[i];
				if (entry == null)
					continue;

				passedWeight += entry.Weight;
				if (roll <= passedWeight)
					return entry;
			}

			return null;
		}

		/// <summary>
		/// Picks up to <paramref name="count" /> entries, never picking the same entry twice.
		/// Returns fewer entries when the pool holds less than requested.
		/// </summary>
		public static void PickDistinct<TEntry>(IReadOnlyList<TEntry> pool, int count, List<TEntry> output)
			where TEntry : WeightedPoolEntry
		{
			if (pool == null || output == null || count <= 0)
				return;

			List<TEntry> candidates = new List<TEntry>(pool.Count);
			for (int i = 0; i < pool.Count; i++)
				if (pool[i] != null && pool[i].Weight > 0f)
					candidates.Add(pool[i]);

			count = Mathf.Min(count, candidates.Count);

			for (int i = 0; i < count; i++)
			{
				TEntry entry = Pick(candidates);
				if (entry == null)
					break;

				output.Add(entry);
				candidates.Remove(entry);
			}
		}
	}
}
