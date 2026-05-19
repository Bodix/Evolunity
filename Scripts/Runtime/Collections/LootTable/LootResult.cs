namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Represents the final generated item and its quantity.
	/// </summary>
	public readonly struct LootResult<T>
	{
		public readonly T Item;
		public readonly int Count;

		public LootResult(T item, int count)
		{
			Item = item;
			Count = count;
		}
	}
}