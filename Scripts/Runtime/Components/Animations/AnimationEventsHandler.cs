// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System;
using UnityEngine;

namespace Evolutex.Evolunity.Components.Animations
{
    [AddComponentMenu("Evolunity/Animations/Animation Events Handler")]
    [RequireComponent(typeof(Animator))]
    public class AnimationEventsHandler : MonoBehaviour
    {
        public event Action Start;
        public event Action End;
    
        public void OnStart()
        {
            Start?.Invoke();
        }

        public void OnEnd()
        {
            End?.Invoke();
        }
    }
}
