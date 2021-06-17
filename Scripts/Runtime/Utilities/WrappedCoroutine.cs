// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections;
using UnityEngine;

namespace Evolutex.Evolunity.Utilities
{
    // TODO:
    // 1. Test stopping null coroutine.
    // 2. Add completion callback.
    // 3. Implement IDisposable and check the disposing.

    public class WrappedCoroutine
    {
        private readonly IEnumerator routine;
        
        public static implicit operator Coroutine(WrappedCoroutine wrapper) => wrapper.Coroutine;

        public MonoBehaviour Owner { get; }
        public Coroutine Coroutine { get; private set; }
        public bool IsRunning => Coroutine != null;

        public WrappedCoroutine(MonoBehaviour owner, IEnumerator routine)
        {
            this.routine = routine;
            
            Owner = owner;
        }

        public WrappedCoroutine Start()
        {
            if (IsRunning)
                throw new InvalidOperationException("The coroutine is already started");
            
            Coroutine = Owner.StartCoroutine(Process());

            return this;
        }

        public void Stop()
        {
            Owner.StopCoroutine(Coroutine);

            Coroutine = null;
        }

        private IEnumerator Process()
        {
            yield return routine;

            Coroutine = null;
        }
    }
}