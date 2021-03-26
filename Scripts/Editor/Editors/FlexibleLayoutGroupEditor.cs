using UnityEditor;
using UnityEngine.UI;
using Evolutex.Evolunity.Components.UI;

namespace Evolutex.Evolunity.Editor.Editors
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FlexibleLayoutGroup))]
    public class FlexibleLayoutGroupEditor : UnityEditor.Editor
    {
        private SerializedProperty paddingProperty;
        private SerializedProperty cellSizeProperty;
        private SerializedProperty spacingProperty;
        private SerializedProperty startCornerProperty;
        private SerializedProperty startAxisProperty;
        private SerializedProperty childAlignmentProperty;
        private SerializedProperty constraintProperty;
        private SerializedProperty constraintCountProperty;

        private SerializedProperty columnSizeModeProperty;
        private SerializedProperty columnCountProperty;
        private SerializedProperty rowSizeModeProperty;
        private SerializedProperty rowCountProperty;

        protected void OnEnable()
        {
            paddingProperty = serializedObject.FindProperty("m_Padding");
            cellSizeProperty = serializedObject.FindProperty("m_CellSize");
            spacingProperty = serializedObject.FindProperty("m_Spacing");
            startCornerProperty = serializedObject.FindProperty("m_StartCorner");
            startAxisProperty = serializedObject.FindProperty("m_StartAxis");
            childAlignmentProperty = serializedObject.FindProperty("m_ChildAlignment");
            constraintProperty = serializedObject.FindProperty("m_Constraint");
            constraintCountProperty = serializedObject.FindProperty("m_ConstraintCount");

            FlexibleLayoutGroup flexibleLayoutGroup = (FlexibleLayoutGroup) target;

            columnSizeModeProperty = serializedObject.FindProperty(nameof(flexibleLayoutGroup.ColumnSizeMode));
            columnCountProperty = serializedObject.FindProperty(nameof(flexibleLayoutGroup.ColumnCount));
            rowSizeModeProperty = serializedObject.FindProperty(nameof(flexibleLayoutGroup.RowSizeMode));
            rowCountProperty = serializedObject.FindProperty(nameof(flexibleLayoutGroup.RowCount));
        }

        public override void OnInspectorGUI()
        {
            FlexibleLayoutGroup flexibleLayoutGroup = (FlexibleLayoutGroup) target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(paddingProperty, true);
            EditorGUILayout.PropertyField(columnSizeModeProperty, true);
            if (flexibleLayoutGroup.ColumnSizeMode == ColumnSizeMode.Expand)
                EditorGUILayout.PropertyField(columnCountProperty, true);
            EditorGUILayout.PropertyField(rowSizeModeProperty, true);
            if (flexibleLayoutGroup.RowSizeMode == RowSizeMode.Expand)
                EditorGUILayout.PropertyField(rowCountProperty, true);
            if (flexibleLayoutGroup.ColumnSizeMode != ColumnSizeMode.Expand
                || flexibleLayoutGroup.RowSizeMode != RowSizeMode.Expand)
                EditorGUILayout.PropertyField(cellSizeProperty, true);
            EditorGUILayout.PropertyField(spacingProperty, true);
            EditorGUILayout.PropertyField(startCornerProperty, true);
            EditorGUILayout.PropertyField(startAxisProperty, true);
            EditorGUILayout.PropertyField(childAlignmentProperty, true);
            EditorGUILayout.PropertyField(constraintProperty, true);
            if (flexibleLayoutGroup.constraint != GridLayoutGroup.Constraint.Flexible)
                EditorGUILayout.PropertyField(constraintCountProperty, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}