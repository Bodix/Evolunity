using System;
using UnityEngine;

namespace Bodix.Evolunity.Collections.LootTable
{
	[Serializable]
	public class LootTableEntry<T>
	{
		public T Item;
		[Range(0f, 1f)]
		public float Probability = 1f;
		[Min(0)]
		public int MinCount = 1;
		[Min(1)]
		public int MaxCount = 1;
	}
}