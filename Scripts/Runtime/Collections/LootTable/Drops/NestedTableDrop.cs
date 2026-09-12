using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Generates items from another nested loot table.
	/// </summary>
	[Serializable]
	public abstract class NestedTableDrop<T> : LootDrop<T>
	{
		public LootTable<T> Table;

		public override bool IsValid()
		{
			if (!base.IsValid())
				return false;

			if (Table == null)
			{
				Debug.LogError($"[{nameof(NestedTableDrop<T>)}] Table reference is missing.");

				return false;
			}

			return true;
		}

		public override bool TryGenerate(List<LootResult<T>> results, LootContext context)
		{
			if (Table == null)
				return false;

			List<LootResult<T>> nestedResults = Table.GenerateLoot(context);

			if (nestedResults == null)
				return false;

			results.AddRange(nestedResults);

			return true;
		}
	}
}
