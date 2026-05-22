using UnityEditor;
using UnityEngine;

namespace Bodix.Evolunity.Collections.Editor
{
	/// <summary>
	/// Universal drawer for all LootDrops. 
	/// It draws "leaf" nodes (like ItemDrop) in a single compact line, 
	/// and complex nodes (like WeightedPoolDrop) using dynamic property iteration.
	/// </summary>
	[CustomPropertyDrawer(typeof(LootDrop), true)]
	public class UniversalLootDropDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);

			SerializedProperty probProp = property.FindPropertyRelative("Probability");

			if (probProp == null)
			{
				Debug.LogError("UniversalLootDropDrawer: Missing 'Probability' property. Early return executed.");
				EditorGUI.LabelField(position, "Error: Missing 'Probability' property.");
				EditorGUI.EndProperty();
				return;
			}

			// Line 1: Probability with a slider (drawn over the default element label space).
			float probabilityLabelWidth = 70f;
			float probabilitySliderWidth = 140f;

			Rect firstLineRect = new Rect(position.x, position.y, probabilityLabelWidth, EditorGUIUtility.singleLineHeight);
			EditorGUI.LabelField(firstLineRect, "Probability");

			firstLineRect.x += probabilityLabelWidth;
			firstLineRect.width = probabilitySliderWidth;
			EditorGUI.Slider(firstLineRect, probProp, 0f, 1f, GUIContent.none);

			// Check if this specific node has an "Item" field (meaning it's an ItemDrop leaf node).
			SerializedProperty itemProp = property.FindPropertyRelative("Item");

			if (itemProp != null)
			{
				int previousIndentLevel = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 0;

				SerializedProperty minProp = property.FindPropertyRelative("MinCount");
				SerializedProperty maxProp = property.FindPropertyRelative("MaxCount");

				if (minProp == null || maxProp == null)
				{
					Debug.LogError("UniversalLootDropDrawer: Missing 'MinCount' or 'MaxCount' properties. Early return executed.");
					EditorGUI.LabelField(position, "Error: Missing properties.");
					EditorGUI.indentLevel = previousIndentLevel;
					EditorGUI.EndProperty();
					return;
				}

				// Line 2: Item reference, Min, Max.
				float spacing = 5f;
				float minMaxLabelWidth = 28f;
				float minMaxFieldWidth = 35f;

				float secondLineY = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
				float fixedFieldsWidth = (minMaxLabelWidth * 2) + (minMaxFieldWidth * 2) + (spacing * 2);
				float itemReferenceWidth = Mathf.Max(60f, position.width - fixedFieldsWidth);

				Rect secondLineRect = new Rect(position.x, secondLineY, itemReferenceWidth, EditorGUIUtility.singleLineHeight);

				// [Object Reference Field].
				EditorGUI.PropertyField(secondLineRect, itemProp, GUIContent.none);
				secondLineRect.x += itemReferenceWidth + spacing;

				// Min: [MinCount].
				secondLineRect.width = minMaxLabelWidth;
				EditorGUI.LabelField(secondLineRect, "Min:");
				secondLineRect.x += minMaxLabelWidth;
				secondLineRect.width = minMaxFieldWidth;
				EditorGUI.PropertyField(secondLineRect, minProp, GUIContent.none);
				secondLineRect.x += minMaxFieldWidth + spacing;

				// Max: [MaxCount].
				secondLineRect.width = minMaxLabelWidth;
				EditorGUI.LabelField(secondLineRect, "Max:");
				secondLineRect.x += minMaxLabelWidth;
				secondLineRect.width = minMaxFieldWidth;
				EditorGUI.PropertyField(secondLineRect, maxProp, GUIContent.none);

				// Restore indent.
				EditorGUI.indentLevel = previousIndentLevel;
			}
			else
			{
				// Complex node (e.g. WeightedPoolDrop). Draw its fields dynamically.
				float currentY = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

				SerializedProperty iterator = property.Copy();
				SerializedProperty endProperty = iterator.GetEndProperty();
				bool enterChildren = true;

				while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty))
				{
					enterChildren = false; // Only iterate direct children.

					// Skip because it's already drawn on the first line.
					if (iterator.name == "Probability")
						continue;

					float propHeight = EditorGUI.GetPropertyHeight(iterator, true);
					Rect propRect = new Rect(position.x, currentY, position.width, propHeight);

					EditorGUI.PropertyField(propRect, iterator, true);

					currentY += propHeight + EditorGUIUtility.standardVerticalSpacing;
				}
			}

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			SerializedProperty probProp = property.FindPropertyRelative("Probability");

			if (probProp == null)
			{
				return EditorGUIUtility.singleLineHeight;
			}

			float totalHeight = EditorGUIUtility.singleLineHeight; // Header line.

			SerializedProperty itemProp = property.FindPropertyRelative("Item");

			if (itemProp != null)
			{
				// Compact height calculation for ItemDrop.
				totalHeight += EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;
			}
			else
			{
				// Expanded height calculation for complex objects.
				SerializedProperty iterator = property.Copy();
				SerializedProperty endProperty = iterator.GetEndProperty();
				bool enterChildren = true;

				while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty))
				{
					enterChildren = false;

					if (iterator.name == "Probability")
						continue;

					totalHeight += EditorGUIUtility.standardVerticalSpacing + EditorGUI.GetPropertyHeight(iterator, true);
				}
			}

			return totalHeight;
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
			
			// THIS is the magic fix for the list indentation crushing.
			int previousIndentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			SerializedProperty itemProp = property.FindPropertyRelative("Item");
			SerializedProperty weightProp = property.FindPropertyRelative("Weight");
			SerializedProperty minProp = property.FindPropertyRelative("MinCount");
			SerializedProperty maxProp = property.FindPropertyRelative("MaxCount");

			if (itemProp == null || weightProp == null || minProp == null || maxProp == null)
			{
				Debug.LogError("UniversalWeightedEntryDrawer: Missing required properties. Early return executed.");
				EditorGUI.LabelField(position, "Error: Missing properties.");
				EditorGUI.indentLevel = previousIndentLevel;
				EditorGUI.EndProperty();
				return;
			}

			float spacing = 5f;
			float weightLabelWidth = 20f;
			float weightFieldWidth = 40f;
			float minMaxLabelWidth = 28f;
			float minMaxFieldWidth = 35f;

			// Adjusted spacing multiplier to 3 since there are 4 drawn blocks.
			float fixedFieldsWidth = weightLabelWidth + weightFieldWidth + (minMaxLabelWidth * 2) + (minMaxFieldWidth * 2) + (spacing * 3);
			float itemReferenceWidth = Mathf.Max(60f, position.width - fixedFieldsWidth);

			Rect currentRect = new Rect(position.x, position.y + 1f, itemReferenceWidth, EditorGUIUtility.singleLineHeight);

			// [Object Reference Field].
			EditorGUI.PropertyField(currentRect, itemProp, GUIContent.none);
			currentRect.x += itemReferenceWidth + spacing;

			// W: [Weight].
			currentRect.width = weightLabelWidth;
			EditorGUI.LabelField(currentRect, "W:");
			currentRect.x += weightLabelWidth;
			currentRect.width = weightFieldWidth;
			EditorGUI.PropertyField(currentRect, weightProp, GUIContent.none);
			currentRect.x += weightFieldWidth + spacing;

			// Min: [MinCount].
			currentRect.width = minMaxLabelWidth;
			EditorGUI.LabelField(currentRect, "Min:");
			currentRect.x += minMaxLabelWidth;
			currentRect.width = minMaxFieldWidth;
			EditorGUI.PropertyField(currentRect, minProp, GUIContent.none);
			currentRect.x += minMaxFieldWidth + spacing;

			// Max: [MaxCount].
			currentRect.width = minMaxLabelWidth;
			EditorGUI.LabelField(currentRect, "Max:");
			currentRect.x += minMaxLabelWidth;
			currentRect.width = minMaxFieldWidth;
			EditorGUI.PropertyField(currentRect, maxProp, GUIContent.none);

			EditorGUI.indentLevel = previousIndentLevel;
			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
		}
	}
}