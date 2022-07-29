using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.Entities.Components.Popup
{
    internal class ComponentEditPopup : PopupWindowContent
    {
        public readonly DisplayData displayData;
        private readonly FieldInfo[] fields;
        private Vector2 scrollViewPosition = Vector2.zero;

        public ComponentEditPopup(DisplayData displayData)
        {
            this.displayData = displayData;

            var jsonPropertyType = typeof(JsonPropertyAttribute);
            var component = displayData.component.GetType();

            //Need specific flags as you can't just get the readonly fields
            var publicFields = component.GetFields(BindingFlags.Instance | BindingFlags.Public);
            var privateFields = component.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var allFields = publicFields.Concat(privateFields);

            //filtering out fields that don't have the jsonProperty
            fields = allFields.Where(x => x.CustomAttributes.Any(c => c.AttributeType == jsonPropertyType)).ToArray();
        }

        public override Vector2 GetWindowSize()
        {
            Vector2 maxSize = new(300, 600);
            Vector2 size = new(300, EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing);

            size.y += EditorGUIUtility.singleLineHeight * fields.Length + EditorGUIUtility.standardVerticalSpacing * fields.Length - 1;
            size.y = Mathf.Min(size.y, maxSize.y);

            return size;
        }

        public override void OnGUI(Rect rect)
        {
            using (var h1 = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Save"))
                {
                    displayData.ComponentToJson();
                    displayData.UpdateTowerComponentData();

                    editorWindow.Close();
                }

                if (GUILayout.Button("Cancel"))
                {
                    editorWindow.Close();
                }
                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.Space();

            using var scrollView = new EditorGUILayout.ScrollViewScope(scrollViewPosition);
            DrawField();

            scrollViewPosition = scrollView.scrollPosition;
        }

        private void DrawField()
        {
            foreach (var field in fields)
            {
                var val = field.GetValue(displayData.component);
                if (val is null)
                {
                    var type = field.FieldType;
                    if (type.IsValueType)
                    {
                        val = Activator.CreateInstance(field.FieldType);
                    }
                    else if (type == typeof(string))
                    {
                        val = string.Empty;
                    }
                }

                //Handling most basic value types
                val = val switch
                {
                    bool b => EditorGUILayout.Toggle(field.Name, b),
                    float f => EditorGUILayout.FloatField(field.Name, f),
                    double d => EditorGUILayout.DoubleField(field.Name, d),
                    string s => EditorGUILayout.TextField(field.Name, s),
                    int i => EditorGUILayout.IntField(field.Name, i),
                    long l => EditorGUILayout.LongField(field.Name, l),
                    Color c => EditorGUILayout.ColorField(field.Name, c),
                    Vector2 v2 => EditorGUILayout.Vector2Field(field.Name, v2),
                    Vector3 v3 => EditorGUILayout.Vector3Field(field.Name, v3),
                    Vector4 v4 => EditorGUILayout.Vector4Field(field.Name, v4),
                    Vector2Int v2i => EditorGUILayout.Vector2IntField(field.Name, v2i),
                    Vector3Int v3i => EditorGUILayout.Vector3IntField(field.Name, v3i),
                    Quaternion q => Quaternion.Euler(EditorGUILayout.Vector3Field(field.Name, q.eulerAngles)),
                    _ => val,
                };

                field.SetValue(displayData.component, val);
            }
        }
    }
}