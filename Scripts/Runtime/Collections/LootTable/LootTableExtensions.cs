// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedType.Global

using System.Collections.Generic;
using Bodix.Evolunity.Services;

namespace Bodix.Evolunity.Collections
{
	public static class LootTableExtensions
	{
		// This method is commented out because it acts as an anti-pattern that masks the root cause of asset duplication.
		// Resolving instances at runtime fixes the logic but leaves the original duplicate "phantom" assets in memory, causing silent memory leaks.
		// It also creates unnecessary allocations and CPU overhead during loot generation.
		// The correct solution is to fix the asset loading pipeline and dependency graph.
		// Ensure that built-in scenes or prefabs do not contain hard references to dynamically loaded assets (e.g., Addressables).
		// If a scene or prefab contains references to Addressables, it must also be loaded via Addressables.
		// Use a build preprocessor script to detect and prevent cross-boundary hard references at compile time (Fail Fast principle).
		/*
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
		*/
	}
}