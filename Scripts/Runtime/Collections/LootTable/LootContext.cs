using System;

namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Data that loot conditions check: the player level, the place in the world, the time of day and so on.
	/// It is empty. Derive your own context and pass it to <see cref="LootTable{T}.GenerateLoot"/>.
	/// </summary>
	public class LootContext
	{
	}

	/// <summary>
	/// Base class for all loot conditions. A public, non-abstract, [Serializable] derived class
	/// appears in the Condition dropdown of every drop.
	/// </summary>
	[Serializable]
	public abstract class LootCondition
	{
		/// <summary>
		/// Evaluates if the condition is met based on the provided context.
		/// The context can be null, for example from the "Test Generate Loot" button.
		/// </summary>
		public abstract bool IsMet(LootContext context);
	}
}