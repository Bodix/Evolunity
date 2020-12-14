/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using UnityEngine;

namespace Evolunity
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [SerializeField]
        protected bool dontDestroyOnLoad = true;

        public static bool FindInstanceIfNull = false;
        public static bool CreateInstanceIfNull = false;

        private static readonly object lockObject = new object();
        private static readonly string debugPrefix = $"[Singleton<{typeof(T).Name}>] ";
        private static T instance;
        private static bool isDestroyed;

        public static T Instance
        {
            get
            {
                if (isDestroyed)
                {
                    Debug.LogWarning(debugPrefix + "The instance will not be returned because it is already destroyed");

                    return null;
                }

                lock (lockObject)
                {
                    if (instance == null)
                    {
                        if (FindInstanceIfNull)
                            instance = FindObjectOfType<T>();

                        if (instance != null)
                        {
                            Debug.Log(debugPrefix + "An instance was found on the scene");

                            return instance;
                        }
                        else if (CreateInstanceIfNull)
                        {
                            Debug.Log(debugPrefix + "An instance is needed on the scene " +
                                      "and no existing instances were found, so a new instance will be created");

                            return new GameObject($"{typeof(T).Name} (Singleton)").AddComponent<T>();
                        }
                    }

                    return instance;
                }
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = (T) this;

                if (dontDestroyOnLoad)
                {
                    transform.SetParent(null);

                    DontDestroyOnLoad(gameObject);
                }
            }
            else if (instance != this)
            {
                Debug.LogWarning(debugPrefix + "The instance is already exists, so this instance will be destroyed");

                Destroy(gameObject);
            }
        }

        protected virtual void OnApplicationQuit()
        {
            isDestroyed = true;
        }
    }
}