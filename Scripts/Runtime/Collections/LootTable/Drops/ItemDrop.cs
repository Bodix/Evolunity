using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	[Serializable]
	public abstract class ItemDrop<T> : LootDrop<T>
	{
		public T Item;

		[Min(0)]
		public int MinCount = 1;

		[Min(1)]
		public int MaxCount = 1;

		public override bool IsValid()
		{
			if (!base.IsValid())
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
}