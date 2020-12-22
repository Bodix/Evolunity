// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Evolutex.Evolunity.Collections
{
    public class WeightQueue<T> : IEnumerable<T>
    {
        private readonly Queue<T> queue;

        /// <summary>
        /// Fills the queue with items, with the amount of each item determined by its weight.
        /// </summary>
        public WeightQueue(IEnumerable<T> items, Func<T, float> weightSelector, int count)
        {
            queue = new Queue<T>(count);

            float weightSum = items.Select(weightSelector).Sum();

            foreach (T item in items)
                for (int i = 0; i < Mathf.Round(count * (weightSelector(item) / weightSum)); i++)
                    queue.Enqueue(item);
        }

        public void Enqueue(T item) => queue.Enqueue(item);

        public T Dequeue() => queue.Dequeue();

        public T Peek() => queue.Peek();

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) queue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
