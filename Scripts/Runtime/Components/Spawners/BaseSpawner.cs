// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    public abstract class BaseSpawner<T> : PeriodicBehaviour where T : UnityEngine.Object
    {
        [SerializeField]
        private SpawnMethod spawnMethod = SpawnMethod.Start;

        [Space]
        public T Prefab;
        public uint Amount = 1;
        public Transform Parent;

        private readonly List<T> buffer = new List<T>();

        public event Action<List<T>> Spawned;

        public override bool DrawPeriodFieldInInspector => spawnMethod == SpawnMethod.Periodic;
        public override bool DrawPeriodProgressInInspector => spawnMethod == SpawnMethod.Periodic;

        private void Reset()
        {
            Parent = transform;
        }

        private void Start()
        {
            if (spawnMethod == SpawnMethod.Start)
                Spawn();
        }

        protected override void Update()
        {
            if (spawnMethod == SpawnMethod.Update)
                Spawn();
            else
                base.Update();
        }

        protected override void OnPeriod()
        {
            if (spawnMethod == SpawnMethod.Periodic)
                Spawn();
        }

        public void Spawn()
        {
            buffer.Clear();

            for (int i = 0; i < Amount; i++)
                buffer.Add(GetClone());

            Spawned?.Invoke(buffer);
        }

        public abstract T GetClone();

        public enum SpawnMethod
        {
            Manual,
            Start,
            Update,
            Periodic
        }
    }
}