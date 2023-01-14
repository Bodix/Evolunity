// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;

namespace Evolutex.Evolunity.Utilities
{
    /// <summary>
    /// This is a bool predicate called <see cref="Condition"/>.
    /// <br/>The same as <see cref="Predicate{T}"/> but without input parameter.
    ///
    /// <para/>Often it's useful to use in methods with "Try-" prefix to reduce non-obviousness.
    ///
    /// <para/>More about predicates you can read
    /// <a href="https://en.wikipedia.org/wiki/Predication_(computer_architecture)">here</a> or 
    /// <a href="https://www.sciencedirect.com/topics/computer-science/boolean-predicate">here</a>.
    /// </summary>
    public delegate bool Condition();

    public static class ConditionExtensions
    {
        public static bool IsTrueOrNull(this Condition condition)
        {
            return condition?.Invoke() ?? true;
        }

        public static bool IsFalseOrNull(this Condition condition)
        {
            return !condition?.Invoke() ?? true;
        }

        public static bool IsTrue(this Condition condition)
        {
            return condition?.Invoke() ?? false;
        }

        public static bool IsFalse(this Condition condition)
        {
            return !condition?.Invoke() ?? false;
        }
    }
}