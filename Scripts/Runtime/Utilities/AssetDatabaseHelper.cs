using UnityEditor;

namespace Bodix.Evolunity.Utilities
{
	public static class AssetDatabaseHelper
	{
		public static T FindFirstAssetByType<T>() where T : UnityEngine.Object
		{
#if UNITY_EDITOR
			string typeName = typeof(T).Name;
			string[] guids = AssetDatabase.FindAssets("t:" + typeName);

			return guids.Length == 0
				? null
				: AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[0]));
#else
			UnityEngine.Debug.LogError($"Trying to use {nameof(AssetDatabaseHelper)} not in Unity Editor");
			return null;
#endif
		}
	}
}