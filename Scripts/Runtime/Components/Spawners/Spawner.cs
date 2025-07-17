// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    public class Spawner<T> : BaseSpawner<T> where T : Object
    {
        public bool UseSpawnerPosition = true;
        public bool UseSpawnerRotation = true;

        public override T GetClone()
        {
            return Instantiate(
                Prefab,
                UseSpawnerPosition ? transform.position : Vector3.zero,
                UseSpawnerRotation ? transform.rotation : Quaternion.identity,
                Parent);
        }
    }

    [AddComponentMenu("Evolunity/Spawners/Spawner")]
    public class Spawner : Spawner<GameObject>
    {
    }
}