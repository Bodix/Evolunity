using System;

namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Context for loot generation, used for conditional drops in the future.
	/// </summary>
	public class LootContext
	{
		// Future properties like player level, time of day, active quests, etc.
	}

	/// <summary>
	/// Base class for all loot conditions.
	/// </summary>
	[Serializable]
	public abstract class LootCondition
	{
		/// <summary>
		/// Evaluates if the condition is met based on the provided context.
		/// </summary>
		public abstract bool IsMet(LootContext context);
	}
}