using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

// https://forum.unity.com/threads/expand-collapse-all-components-in-a-gameobject-refresh-issue.517866/
namespace Bodix.Evolunity.Editor
{
	public static class ComponentMenuItems
	{
		[MenuItem("CONTEXT/Component/Collapse All")]
		public static void CollapseAll(MenuCommand command)
		{
			GameObject gameObject = (command.context as Component).gameObject;
			Component[] components = gameObject.GetComponents<Component>();
			foreach (Component component in components)
				InternalEditorUtility.SetIsInspectorExpanded(component, false);

			ActiveEditorTracker.sharedTracker.ForceRebuild();
		}

		[MenuItem("CONTEXT/Component/Expand All")]
		public static void ExpandAll(MenuCommand command)
		{
			GameObject gameObject = (command.context as Component).gameObject;
			Component[] components = gameObject.GetComponents<Component>();
			foreach (Component component in components)
				InternalEditorUtility.SetIsInspectorExpanded(component, true);

			ActiveEditorTracker.sharedTracker.ForceRebuild();
		}
	}
}