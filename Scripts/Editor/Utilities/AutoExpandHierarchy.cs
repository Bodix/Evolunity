// Evolunity for Unity
// Copyright © 2020 Bogdan Nikolayev <bodix321@gmail.com>
// All Rights Reserved

using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bodix.Evolunity.Editor.Utilities
{
	// TODO: Add UI for adding GameObject's to expand.

	// Use it as follows:
	[InitializeOnLoad]
	public static class EditorBootstrap
	{
		static EditorBootstrap()
		{
			AutoExpandHierarchy.GameObjectsToExpand.Add("Logic");
			AutoExpandHierarchy.GameObjectsToExpand.Add("Environment");
			AutoExpandHierarchy.GameObjectsToExpand.Add("Player");
			AutoExpandHierarchy.GameObjectsToExpand.Add("UI");
		}
	}

	[InitializeOnLoad]
	public static class AutoExpandHierarchy
	{
		static AutoExpandHierarchy()
		{
			EditorSceneManager.sceneOpened += (scene, mode) => ExpandObjects(scene);
			SceneManager.sceneLoaded += (scene, mode) => ExpandObjects(scene);
		}

		public static List<string> GameObjectsToExpand { get; } = new List<string>();

		private static void ExpandObjects(Scene scene)
		{
			GameObjectsToExpand.ForEach(name =>
			{
				// GameObject gameObject = GameObject.Find(name);
				// FindObjectInScene is better than GameObject.Find because:
				// 1. It searches not only for active objects, but for inactive ones as well.
				// 2. It searches only within the specified scene, rather than searching across all scenes.
				GameObject gameObject = FindObjectInScene(scene, name);
				if (gameObject)
					SceneHierarchy.SetExpanded(gameObject, true);
			});
		}

		private static GameObject FindObjectInScene(Scene scene, string name)
		{
			// Iterate through all root objects in the loaded scene.
			GameObject[] rootObjects = scene.GetRootGameObjects();
			foreach (GameObject root in rootObjects)
			{
				if (root.name == name)
					return root;

				// Search through all children, including inactive ones.
				Transform[] children = root.GetComponentsInChildren<Transform>(true);
				foreach (Transform child in children)
					if (child.name == name)
						return child.gameObject;
			}

			return null;
		}
	}
}