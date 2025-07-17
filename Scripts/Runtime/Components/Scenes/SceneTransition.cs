// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using UnityEngine;
using UnityEngine.SceneManagement;
using Evolutex.Evolunity.Components.Animations;

namespace Evolutex.Evolunity.Components.Scenes
{
    [AddComponentMenu("Evolunity/Scenes/Scene Transition")]
    public class SceneTransition : MonoBehaviour
    {
        public InOutBehaviour InOutAnimation = null;
        public bool OutAnimationOnStart = true;

        private void Start()
        {
            if (OutAnimationOnStart)
                InOutAnimation.PlayOut();
        }

        public void LoadSceneAfterAnimation(string sceneName)
        {
            InOutAnimation.PlayInCoroutine(() => SceneManager.LoadScene(sceneName));
        }
        
        public void LoadSceneAfterAnimation(int sceneBuildIndex)
        {
            InOutAnimation.PlayInCoroutine(() => SceneManager.LoadScene(sceneBuildIndex));
        }
    }
}