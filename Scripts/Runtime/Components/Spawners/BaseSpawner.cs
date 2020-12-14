/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Evolunity.Components.Spawners
{
    public abstract class BaseSpawner<T> : PeriodicBehaviour where T : UnityEngine.Object
    {
        [SerializeField]
        private SpawnMethod spawnMethod = SpawnMethod.Start;

        [Space]
        public T Prefab;
        public uint Amount = 1;
        [SerializeField]
        private Transform parent;

        private readonly List<T> buffer = new List<T>();

        public event Action<List<T>> Spawned;

        public Transform Parent
        {
            get => parent ? parent : transform;
            set => parent = value;
        }

        public override bool DrawPeriodFieldInInspector => spawnMethod == SpawnMethod.Periodic;
        public override bool DrawPeriodProgressInInspector => spawnMethod == SpawnMethod.Periodic;

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

        public virtual T GetClone()
        {
            return Instantiate(Prefab, Parent);
        }

        public enum SpawnMethod
        {
            Manual,
            Start,
            Update,
            Periodic
        }
    }
}