using System.Collections.Generic;
using System.Text;
using Bodix.Evolunity.Attributes;
using NaughtyAttributes;
using UnityEngine;

// TODO:
// 1. Nested Tables: When an item in a table is... another loot table.
// For example, a “Chest” drops a “Bag of Gold” (a table), which in turn generates gold.
//
// 2. Conditional Drops: An item drops only if the player is above level 10,
// or only at night, or only if a specific quest has been accepted.
//
// 3. Pity System (Guarantee): A mechanic from gacha games.
// If a player has killed a boss 99 times and hasn’t received a rare sword, the chance becomes 100% on the 100th attempt.

namespace Bodix.Evolunity.Collections
{
	public abstract class LootTable<T, TEntry> : ScriptableObject where TEntry : LootTableEntry<T>, new()
	{
		[SerializeReference, TypeSelector]
		protected List<TEntry> items = new List<TEntry>();

		private void OnValidate()
		{
			foreach (TEntry item in items)
			{
				// Unity serialization bypasses field initializers when adding a new element to an empty list.
				// This heuristic detects a newly created default element and initializes it properly.
				if (item.Probability == 0f && item.MinCount == 0 && item.MaxCount == 0)
				{
					item.Probability = 1f;
					item.MinCount = 1;
					item.MaxCount = 1;
				}
			}
		}

		public List<LootResult<T>> GenerateLoot()
		{
			if (items == null)
			{
				Debug.LogError("The elements list is null. Cannot generate loot.");

				return null;
			}

			List<LootResult<T>> results = new List<LootResult<T>>();

			foreach (TEntry item in items)
			{
				if (item == null)
				{
					Debug.LogError("Encountered a null element in the loot table.");

					return null;
				}

				if (item.Item == null)
				{
					Debug.LogError("An item reference is missing in the loot table.");

					return null;
				}

				if (item.Probability < 0f || item.Probability > 1f)
				{
					Debug.LogError("Probability must be between 0 and 1.");

					return null;
				}

				if (item.MinCount < 0 || item.MaxCount < item.MinCount)
				{
					Debug.LogError("Invalid min or max count configuration.");

					return null;
				}

				float roll = Random.Range(0f, 1f);
				if (roll <= item.Probability)
				{
					int count = Random.Range(item.MinCount, item.MaxCount + 1);
					if (count > 0)
						results.Add(new LootResult<T>(item.Item, count));
				}
			}

			return results;
		}

		[Button("Test Generate Loot")]
		protected void TestGenerateLoot()
		{
			List<LootResult<T>> droppedLoot = GenerateLoot();

			if (droppedLoot == null)
			{
				Debug.LogError("Failed to generate loot during editor test. Test aborted.");

				return;
			}

			if (droppedLoot.Count == 0)
			{
				Debug.Log("Loot generated successfully, but no items dropped based on probabilities.");

				return;
			}

			StringBuilder logBuilder = new StringBuilder("Loot generated:\n");

			foreach (LootResult<T> result in droppedLoot)
				logBuilder.AppendLine($"- {result.Count}x of {result.Item}");

			Debug.Log(logBuilder.ToString());
		}
	}
}