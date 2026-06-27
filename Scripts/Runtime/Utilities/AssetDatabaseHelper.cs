using UnityEditor;

namespace Bodix.Evolunity.Utilities
{
	public static class AssetDatabaseHelper
	{
		public static T FindFirstAssetByType<T>() where T : UnityEngine.Object
		{
			string typeName = typeof(T).Name;
			string[] guids = AssetDatabase.FindAssets("t:" + typeName);

			return guids.Length == 0
				? null
				: AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[0]));
		}
	}
}