// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Evolutex.Evolunity.Components.Scenes
{
    /// <summary>
    /// Use it in Start() method. Dont use it in Awake() method.
    /// The reason is that SceneManager.activeSceneChanged event invokes after Awake(), before Start().
    /// </summary>
    [AddComponentMenu("Evolunity/Scene Watcher")]
    public class SceneWatcher : MonoBehaviour
    {
        public bool Logs;

        public const int UndefinedScene = -1;
        public int PreviousScene { get; private set; } = UndefinedScene;
        public int CurrentScene { get; private set; } = UndefinedScene;
        public string PreviousSceneName => PreviousScene != UndefinedScene
            ? Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(PreviousScene))
            : string.Empty;
        public string CurrentSceneName => CurrentScene != UndefinedScene
            ? Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(CurrentScene))
            : string.Empty;

        private void Awake()
        {
            // Invokes after Awake(), before Start().
            SceneManager.activeSceneChanged += (prevScene, nextScene) =>
            {
                // `prevScene` is empty if you just invoke `SceneManager.LoadScene()`.
                // To make `prevScene` working as expected you should load new scene additive,
                // then manually change active scene, then unload previous scene, as described here:
                // https://discussions.unity.com/t/how-come-scenemanager-activescenechanged-have-two-parameters/639093/2
                // A lot simpler approach will be to handle previous and current scenes manually:

                PreviousScene = CurrentScene;
                CurrentScene = nextScene.buildIndex;

                if (Logs)
                    Debug.Log(
                        "Previous scene: " + PreviousSceneName + "\n" +
                        "Current scene: " + CurrentSceneName);
            };
        }
    }
}