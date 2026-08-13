using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	[Serializable]
	public abstract class LootDrop
	{
		[Range(0f, 1f)]
		public float Probability = 1f;

		// Uncomment when it will be needed.
		// [SerializeReference]
		[SerializeReference, HideInInspector]
		public LootCondition Condition;

		public virtual bool IsValid()
		{
			if (Probability < 0f || Probability > 1f)
			{
				Debug.LogError("[LootDrop] Probability must be between 0 and 1.");

				return false;
			}

			return true;
		}

		/// <summary>
		/// Called by the parent loot table to handle default values and validation.
		/// </summary>
		public virtual void OnValidate()
		{
		}
	}

	public abstract class LootDrop<T> : LootDrop
	{
		/// <summary>
		/// Generates the specific internal loot safely and adds it to the results list.
		/// </summary>
		public abstract bool TryGenerate(List<LootResult<T>> results, LootContext context);
	}
}