// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using NaughtyAttributes;
using UnityEngine;
using Evolutex.Evolunity.Components.Scenes;

namespace Evolutex.Evolunity.Components.Triggers
{
    [AddComponentMenu("Evolunity/Triggers/Scene Change Trigger")]
    public class SceneChangeTrigger : AbstractTrigger
    {
        [SerializeField, Scene]
        private string _scene;
        [SerializeField]
        private SceneTransition _sceneTransition;

        protected override void EnterTrigger(Collider other)
        {
            _sceneTransition.LoadSceneAfterAnimation(_scene);

            InvokeTriggeredEvent();
        }
    }
}