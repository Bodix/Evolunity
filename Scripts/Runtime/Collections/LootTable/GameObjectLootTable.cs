using System;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	// [CreateAssetMenu(fileName = "GameObject Loot Table", menuName = "Loot Tables/GameObject Loot Table", order = 0)]
	public class GameObjectLootTable : LootTable<GameObject>
	{
	}

	[Serializable]
	public class GameObjectItemDrop : ItemDrop<GameObject>
	{
	}

	[Serializable]
	public class GameObjectNestedTableDrop : NestedTableDrop<GameObject>
	{
	}

	[Serializable]
	public class GameObjectWeightedPoolDrop : WeightedPoolDrop<GameObject, GameObjectWeightedEntry>
	{
	}

	[Serializable]
	public class GameObjectWeightedEntry : WeightedPoolEntry<GameObject>
	{
	}
}