// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Type independent part of a weighted pool entry: how likely it is to be picked and how much of it is granted.
	/// </summary>
	[Serializable]
	public abstract class WeightedPoolEntry
	{
		[Min(0f)]
		public float Weight = 1f;

		[Min(0)]
		public int MinCount = 1;

		[Min(1)]
		public int MaxCount = 1;

		/// <summary>
		/// Rolls the amount this entry grants.
		/// </summary>
		public int RandomCount => Random.Range(MinCount, MaxCount + 1);

		/// <summary>
		/// Replaces the zeroes Unity puts into a freshly added list element with meaningful defaults.
		/// Call it from the owner's OnValidate.
		/// </summary>
		public void ResetIfEmpty()
		{
			if (Weight != 0f || MinCount != 0 || MaxCount != 0)
				return;

			Weight = 1f;
			MinCount = 1;
			MaxCount = 1;
		}
	}

	/// <summary>
	/// A single entry within a weighted pool.
	/// </summary>
	[Serializable]
	public abstract class WeightedPoolEntry<T> : WeightedPoolEntry
	{
		public T Item;
	}
}
