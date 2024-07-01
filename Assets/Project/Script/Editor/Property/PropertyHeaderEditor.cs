using UnityEditor;
using UnityEngine;

namespace Colonizer
{
    [CustomPropertyDrawer(typeof(PropertyHeader))]
    public class PropertyHeaderEditor : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            bool IsDependent = property.FindPropertyRelative("IsDependent").boolValue;
            // use the default property height, which takes into account the expanded state
            int line = IsDependent ? 5 : 4;
            return (EditorGUIUtility.singleLineHeight * line);
        }

        public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("Name"));
                position.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("Description"));
                position.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("DataType"));
                position.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("IsDependent"));
                bool IsDependent = property.FindPropertyRelative("IsDependent").boolValue;
                if (IsDependent)
                {
                    position.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(position, property.FindPropertyRelative("DependMethod"));
                }
            }
            EditorGUI.EndProperty();
        }
    }
}
