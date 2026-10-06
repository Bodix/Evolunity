using System.Collections.Generic;
using System.Text;
using PerfectCore.PerfectFoundation;
using PerfectCore.PerfectFoundation.NaughtyAttributes;
using UnityEngine;

// TODO:
// Pity System (Guarantee): A mechanic from gacha games.
// If a player has killed a boss 99 times and hasn’t received a rare sword, the chance becomes 100% on the 100th attempt.

namespace Bodix.Evolunity.Collections
{
	public abstract class LootTable<T> : ScriptableObject
	{
		// SerializeReference only tells Unity how to store the field, not whether to store it,
		// so a non-public field still needs SerializeField on top of it.
		[SerializeField, SerializeReference, TypeSelector, ReorderableList]
		protected List<LootDrop> drops = new List<LootDrop>();

		private void OnValidate()
		{
			if (drops == null)
				return;

			foreach (LootDrop drop in drops)
				drop?.OnValidate();
		}

		/// <summary>
		/// Rolls every drop from top to bottom. The context is any object that drop conditions need,
		/// such as the player or the place in the world. Nested tables get the same context.
		/// Returns null when the table is broken. The error is already logged.
		/// </summary>
		public List<LootResult<T>> GenerateLoot(object context = null)
		{
			if (drops == null)
			{
				Debug.LogError("The elements list is null. Cannot generate loot.");

				return null;
			}

			List<LootResult<T>> results = new List<LootResult<T>>();

			foreach (LootDrop drop in drops)
			{
				if (drop == null || !drop.IsValid())
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
			// There is no context here, so conditions get null.
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
				sb.AppendLine($"- {loot}");

			Debug.Log(sb.ToString());
		}
	}
}