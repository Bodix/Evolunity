/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Evolunity.Collections
{
    public class WeightQueue<T> : IEnumerable<T>
    {
        private readonly Queue<T> queue;

        public WeightQueue(IEnumerable<T> collection, Func<T, float> weightSelector, int count)
        {
            queue = new Queue<T>(count);

            float weightSum = collection.Select(weightSelector).Sum();

            foreach (T item in collection)
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
