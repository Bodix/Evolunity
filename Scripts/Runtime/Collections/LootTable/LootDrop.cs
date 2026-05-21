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

		public virtual bool IsValidBase()
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

	/// <summary>
	/// Generates a single item with an absolute probability.
	/// </summary>
	[Serializable]
	public abstract class ItemDrop<T> : LootDrop<T>
	{
		public T Item;

		[Min(0)]
		public int MinCount = 1;

		[Min(1)]
		public int MaxCount = 1;

		public override bool IsValidBase()
		{
			if (!base.IsValidBase())
				return false;

			if (Item == null)
			{
				Debug.LogError("[ItemDrop] Item reference is missing.");

				return false;
			}

			if (MinCount < 0 || MaxCount < MinCount)
			{
				Debug.LogError($"[ItemDrop] Invalid min or max count configuration for item {Item}.");

				return false;
			}

			return true;
		}

		public override bool TryGenerate(List<LootResult<T>> results, LootContext context)
		{
			int count = UnityEngine.Random.Range(MinCount, MaxCount + 1);
			if (count > 0)
				results.Add(new LootResult<T>(Item, count));

			return true;
		}

		public override void OnValidate()
		{
			base.OnValidate();

			// Fixes newly added elements in the inspector.
			if (MinCount == 0 && MaxCount == 0)
			{
				MinCount = 1;
				MaxCount = 1;
			}
		}
	}

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

		public override bool IsValidBase()
		{
			if (!base.IsValidBase())
				return false;

			if (Rolls < 0 || EmptyRollWeight < 0f)
			{
				Debug.LogError("[WeightedPoolDrop] Invalid rolls or empty weight configuration.");

				return false;
			}

			float totalWeight = EmptyRollWeight;
			foreach (TEntry entry in Pool)
			{
				if (entry == null || entry.Item == null || entry.Weight < 0f || entry.MinCount < 0 || entry.MaxCount < entry.MinCount)
				{
					Debug.LogError("[WeightedPoolDrop] Invalid entry configuration encountered in the pool.");

					return false;
				}

				totalWeight += entry.Weight;
			}

			if (Rolls > 0 && totalWeight <= 0f)
			{
				Debug.LogError("[WeightedPoolDrop] Total weight of the weighted pool is 0. Cannot generate weighted loot.");

				return false;
			}

			return true;
		}

		public override bool TryGenerate(List<LootResult<T>> results, LootContext context)
		{
			if (Pool == null || Pool.Count == 0 || Rolls <= 0)
				return true;

			float totalWeight = EmptyRollWeight;
			foreach (TEntry entry in Pool)
				totalWeight += entry.Weight;

			for (int i = 0; i < Rolls; i++)
			{
				float roll = UnityEngine.Random.Range(0f, totalWeight);
				if (roll < EmptyRollWeight)
					continue;

				float currentWeight = EmptyRollWeight;
				foreach (TEntry entry in Pool)
				{
					currentWeight += entry.Weight;
					if (roll <= currentWeight)
					{
						int count = UnityEngine.Random.Range(entry.MinCount, entry.MaxCount + 1);
						if (count > 0)
							results.Add(new LootResult<T>(entry.Item, count));

						break;
					}
				}
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
			{
				if (entry != null && entry.Weight == 0f && entry.MinCount == 0 && entry.MaxCount == 0)
				{
					entry.Weight = 1f;
					entry.MinCount = 1;
					entry.MaxCount = 1;
				}
			}
		}
	}

	/// <summary>
	/// A single entry within a weighted pool.
	/// </summary>
	[Serializable]
	public abstract class WeightedPoolEntry<T>
	{
		public T Item;

		[Min(0f)]
		public float Weight = 1f;

		[Min(0)]
		public int MinCount = 1;

		[Min(1)]
		public int MaxCount = 1;
	}
}