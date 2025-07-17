// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using NaughtyAttributes;
using Evolutex.Evolunity.Components.Physics;

namespace Evolutex.Evolunity.Components.Scenes
{
    [RequireComponent(typeof(BoxTrigger))]
    public class SceneChangeTrigger : MonoBehaviour
    {
        [SerializeField, Scene]
        private string _scene;
        [SerializeField, Tag]
        private string _allowedTag = "Player";
        [SerializeField]
        private SceneTransition _sceneTransition;

        private BoxTrigger _boxTrigger;

        // https://docs.unity3d.com/2023.2/Documentation/ScriptReference/TagHandle.html
#if UNITY_2023_2_OR_NEWER
        private TagHandle _tagHandle;
#endif

        public BoxTrigger BoxTrigger => _boxTrigger;

        private void Awake()
        {
#if UNITY_2023_2_OR_NEWER
            _tagHandle = TagHandle.GetExistingTag(_allowedTag);
#endif

            _boxTrigger = GetComponent<BoxTrigger>();
            _boxTrigger.TriggerEnter += TriggerSceneChange;
        }

        private void OnDestroy()
        {
            _boxTrigger.TriggerEnter -= TriggerSceneChange;
        }

        private void TriggerSceneChange(Collider col)
        {
#if UNITY_2023_2_OR_NEWER
            if (col.CompareTag(_tagHandle))
#else
            if (col.CompareTag(_allowedTag))
#endif
                _sceneTransition.LoadSceneAfterAnimation(_scene);
        }
    }
}