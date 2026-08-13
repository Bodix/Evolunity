using System.Collections.Generic;
using Bodix.Evolunity.Services;

namespace Bodix.Evolunity.Collections
{
	public static class LootTableExtensions
	{
		/// <summary>
		/// Generates loot and resolves items through the ConfigService to ensure single instance references.
		/// </summary>
		public static List<LootResult<T>> GenerateResolvedLoot<T>(this LootTable<T> table,
			ConfigService configService, LootContext context = null) where T : DataAsset
		{
			List<LootResult<T>> rawLoot = table.GenerateLoot(context);

			if (rawLoot == null)
				return null;

			List<LootResult<T>> resolvedLoot = new List<LootResult<T>>(rawLoot.Count);

			foreach (LootResult<T> result in rawLoot)
				if (result.Item != null && !string.IsNullOrEmpty(result.Item.Id))
					resolvedLoot.Add(new LootResult<T>(
						configService.GetConfig<T>(result.Item.Id) ?? result.Item, result.Count));
				else
					resolvedLoot.Add(result);

			return resolvedLoot;
		}
	}
}