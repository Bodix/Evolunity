using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Collections.Editor
{
	/// <summary>
	/// Universal drawer for all LootDrops. 
	/// It draws "leaf" nodes (like ItemDrop) in a single compact line, 
	/// and complex nodes (like WeightedPoolDrop) using standard drawing.
	/// </summary>
	[CustomPropertyDrawer(typeof(LootDrop), true)]
	public class UniversalLootDropDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			// Check if this specific node has "Item" and "Probability" fields (meaning it's an ItemDrop leaf node)
			SerializedProperty itemProp = property.FindPropertyRelative("Item");
			SerializedProperty probProp = property.FindPropertyRelative("Probability");

			if (itemProp != null && probProp != null)
			{
				EditorGUI.BeginProperty(position, label, property);
				
				// THIS is the magic fix for the UI crushing issue in lists
				int oldIndent = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 0;

				SerializedProperty minProp = property.FindPropertyRelative("MinCount");
				SerializedProperty maxProp = property.FindPropertyRelative("MaxCount");

				// Fixed widths for numeric fields and labels
				float space = 5f;
				float pLabelW = 15f; float pFieldW = 40f;
				float mLabelW = 28f; float mFieldW = 35f;

				float fixedWidth = pLabelW + pFieldW + (mLabelW * 2) + (mFieldW * 2) + (space * 5);
				float itemFieldWidth = Mathf.Max(60f, position.width - fixedWidth);

				// Starting rect
				Rect r = new Rect(position.x, position.y + 1f, pLabelW, EditorGUIUtility.singleLineHeight);

				// P: [Probability]
				EditorGUI.LabelField(r, "P:");
				r.x += pLabelW; r.width = pFieldW;
				EditorGUI.PropertyField(r, probProp, GUIContent.none);
				r.x += pFieldW + space;

				// [Object Reference Field]
				r.width = itemFieldWidth;
				EditorGUI.PropertyField(r, itemProp, GUIContent.none);
				r.x += itemFieldWidth + space;

				// Min: [MinCount]
				r.width = mLabelW;
				EditorGUI.LabelField(r, "Min:");
				r.x += mLabelW; r.width = mFieldW;
				EditorGUI.PropertyField(r, minProp, GUIContent.none);
				r.x += mFieldW + space;

				// Max: [MaxCount]
				r.width = mLabelW;
				EditorGUI.LabelField(r, "Max:");
				r.x += mLabelW; r.width = mFieldW;
				EditorGUI.PropertyField(r, maxProp, GUIContent.none);

				// Restore indent
				EditorGUI.indentLevel = oldIndent;
				EditorGUI.EndProperty();
			}
			else
			{
				// If it's a complex object like WeightedPoolDrop, let Unity draw it normally with foldouts
				EditorGUI.PropertyField(position, property, label, true);
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			if (property.FindPropertyRelative("Item") != null && property.FindPropertyRelative("Probability") != null)
			{
				return EditorGUIUtility.singleLineHeight + 2f; // Compact height
			}
			
			// Default expanded height for complex objects
			return EditorGUI.GetPropertyHeight(property, label, true);
		}
	}

	/// <summary>
	/// Universal drawer for all WeightedPool entries.
	/// </summary>
	[CustomPropertyDrawer(typeof(WeightedPoolEntry), true)]
	public class UniversalWeightedEntryDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			
			// Fix for list indentation crushing
			int oldIndent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			SerializedProperty itemProp = property.FindPropertyRelative("Item");
			SerializedProperty weightProp = property.FindPropertyRelative("Weight");
			SerializedProperty minProp = property.FindPropertyRelative("MinCount");
			SerializedProperty maxProp = property.FindPropertyRelative("MaxCount");

			float space = 5f;
			float wLabelW = 20f; float wFieldW = 40f;
			float mLabelW = 28f; float mFieldW = 35f;

			float fixedWidth = wLabelW + wFieldW + (mLabelW * 2) + (mFieldW * 2) + (space * 5);
			float itemFieldWidth = Mathf.Max(60f, position.width - fixedWidth);

			Rect r = new Rect(position.x, position.y + 1f, itemFieldWidth, EditorGUIUtility.singleLineHeight);

			// [Object Reference Field]
			EditorGUI.PropertyField(r, itemProp, GUIContent.none);
			r.x += itemFieldWidth + space;

			// W: [Weight]
			r.width = wLabelW;
			EditorGUI.LabelField(r, "W:");
			r.x += wLabelW; r.width = wFieldW;
			EditorGUI.PropertyField(r, weightProp, GUIContent.none);
			r.x += wFieldW + space;

			// Min: [MinCount]
			r.width = mLabelW;
			EditorGUI.LabelField(r, "Min:");
			r.x += mLabelW; r.width = mFieldW;
			EditorGUI.PropertyField(r, minProp, GUIContent.none);
			r.x += mFieldW + space;

			// Max: [MaxCount]
			r.width = mLabelW;
			EditorGUI.LabelField(r, "Max:");
			r.x += mLabelW; r.width = mFieldW;
			EditorGUI.PropertyField(r, maxProp, GUIContent.none);

			EditorGUI.indentLevel = oldIndent;
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight + 2f;
		}
	}
}