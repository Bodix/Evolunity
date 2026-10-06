using System;
using PerfectCore.PerfectFoundation;

namespace Bodix.Evolunity.Collections
{
	/// <summary>
	/// Obsolete: pass any object to <see cref="LootTable{T}.GenerateLoot"/>. Conditions check the context type themselves.
	/// </summary>
	[Obsolete("Pass any object to LootTable.GenerateLoot. Conditions check the context type themselves.")]
	public class LootContext
	{
	}

	/// <summary>
	/// Obsolete: implement <see cref="ICondition"/> or derive from <see cref="Condition{TContext}"/> from Perfect Foundation.
	/// </summary>
	[Serializable]
	[Obsolete("Implement ICondition or derive from Condition<TContext> from Perfect Foundation.")]
	public abstract class LootCondition : Condition<LootContext>
	{
	}
}