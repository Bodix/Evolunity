using System;
using UnityEngine;

namespace Bodix.Evolunity.Collections
{
	// [CreateAssetMenu(fileName = "GameObject Loot Table", menuName = "Configs/GameObject Loot Table", order = 0)]
	public class GameObjectLootTable : LootTable<GameObject, GameObjectLootEntry>
	{
	}

	[Serializable]
	public class GameObjectLootEntry : LootTableEntry<GameObject>
	{
	}
}