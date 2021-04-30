// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

// Updates by Bogdan Nikolayev:
// 
// 1. Previous singleton code changed to inheritance from SingletonBehaviour. 
// 2. Removed redundant wrapping to coroutine.
// 3. Added to component menu.
// 4. Refactored namings and access modifiers.
// 5. Improved formatting and XML comments.

/*
    Copyright 2015 Pim de Witte All Rights Reserved.

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

        http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
*/

// Author: Pim de Witte (pimdewitte.com) and contributors
// https://github.com/PimDeWitte/UnityMainThreadDispatcher

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    /// <summary>
    /// A thread-safe class that holds a queue with actions to execute on the next <see cref="Update"/> method.
    /// It can be used to execute functions in the main thread.
    /// </summary>
    [AddComponentMenu("Evolunity/Main Thread Dispatcher")]
    public class MainThreadDispatcher : SingletonBehaviour<MainThreadDispatcher>
    {
        private static readonly Queue<Action> actionQueue = new Queue<Action>();

        private void Update()
        {
            lock (actionQueue)
                while (actionQueue.Count > 0)
                    actionQueue.Dequeue().Invoke();
        }

        /// <summary>
        /// Locks the queue and adds the <see cref="Action"/> to the queue to be executed in the main thread.
        /// </summary>
        public void Enqueue(Action action)
        {
            lock (actionQueue)
                actionQueue.Enqueue(action);
        }

        /// <summary>
        /// Locks the queue and adds the <see cref="IEnumerator"/> to the queue to be executed in the main thread.
        /// </summary>
        public void EnqueueStartCoroutine(IEnumerator routine)
        {
            lock (actionQueue)
                actionQueue.Enqueue(() => StartCoroutine(routine));
        }

        /// <summary>
        /// Locks the queue and adds the <see cref="Action"/> to the queue to be executed in the main thread.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> that is completed when the action completes.
        /// </returns>
        public Task EnqueueAsync(Action action)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

            Enqueue(() =>
            {
                try
                {
                    action.Invoke();

                    taskCompletionSource.TrySetResult(true);
                }
                catch (Exception ex)
                {
                    taskCompletionSource.TrySetException(ex);
                }
            });

            return taskCompletionSource.Task;
        }
    }
}