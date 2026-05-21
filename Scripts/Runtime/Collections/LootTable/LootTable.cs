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
	public abstract class LootTable<T> : ScriptableObject
	{
		[SerializeReference, TypeSelector]
		protected List<LootDrop> drops = new List<LootDrop>();

		public List<LootResult<T>> GenerateLoot(LootContext context = null)
		{
			if (drops == null)
			{
				Debug.LogError("The elements list is null. Cannot generate loot.");

				return null;
			}

			List<LootResult<T>> results = new List<LootResult<T>>();

			foreach (LootDrop drop in drops)
			{
				if (drop == null || !drop.IsValidBase())
				{
					Debug.LogError("Encountered an invalid element in the loot table.");

					return null;
				}

				// Evaluates condition via the context.
				if (drop.Condition != null && !drop.Condition.IsMet(context))
					continue;

				// Evaluates base node probability.
				float roll = Random.Range(0f, 1f);
				if (roll > drop.Probability)
					continue;

				// Safely casts and generates the specific internal loot.
				if (drop is LootDrop<T> typedDrop)
				{
					if (!typedDrop.TryGenerate(results, context))
						return null;
				}
				else
				{
					Debug.LogError($"Invalid drop type encountered. Expected LootDrop<{typeof(T).Name}>.");

					return null;
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
				Debug.Log("Generated Loot: None (Empty drop).");

				return;
			}

			StringBuilder sb = new StringBuilder("Generated Loot:\n");
			foreach (LootResult<T> loot in droppedLoot)
				sb.AppendLine($"- {loot.Item.ToString()} (x{loot.Count})");

			Debug.Log(sb.ToString());
		}
	}
}