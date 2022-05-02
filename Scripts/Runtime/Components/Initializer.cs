// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Linq;
using UnityEngine;

namespace Evolutex.Evolunity.Components
{
    /// <summary>
    /// Used to initialize objects, mostly disabled on scene by default.
    /// </summary>
    [DefaultExecutionOrder(-1)]
    [AddComponentMenu("Evolunity/Initializer")]
    public class Initializer : MonoBehaviour
    {
        [SerializeField]
        private InitializeMethod _initializeMethod;
        [SerializeField, InterfaceType(typeof(IInitializable))]
        private Object[] _initializables;

        private void Awake()
        {
            if (_initializeMethod == InitializeMethod.Awake)
                Initialize();
        }

        private void Start()
        {
            if (_initializeMethod == InitializeMethod.Start)
                Initialize();
        }

        private void OnValidate()
        {
            enabled = true;
        }

        private void Initialize()
        {
            if (_initializables.Any(x => !x))
            {
                Debug.LogError("Initializer contains null", this);
                
                return;
            }

            foreach (IInitializable initializable in _initializables)
                initializable.Initialize();
        }

        private enum InitializeMethod
        {
            Awake,
            Start
        }
    }

    public interface IInitializable
    {
        void Initialize();
    }
}