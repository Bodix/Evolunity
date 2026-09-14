using System;
using PerfectCore;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	// [CreateAssetMenu(fileName = "GameObject Loot Table", menuName = "Loot Tables/GameObject Loot Table", order = 0)]
	public class GameObjectLootTable : LootTable<GameObject>
	{
	}

	[Serializable]
	[TypeSelectorName("GameObject")]
	public class GameObjectItemDrop : ItemDrop<GameObject>
	{
	}

	[Serializable]
	[TypeSelectorName("Nested GameObject Table")]
	public class GameObjectNestedTableDrop : NestedTableDrop<GameObject>
	{
	}

	[Serializable]
	[TypeSelectorName("Weighted GameObject Pool")]
	public class GameObjectWeightedPoolDrop : WeightedPoolDrop<GameObject, GameObjectWeightedEntry>
	{
	}

	[Serializable]
	public class GameObjectWeightedEntry : WeightedPoolEntry<GameObject>
	{
	}
}