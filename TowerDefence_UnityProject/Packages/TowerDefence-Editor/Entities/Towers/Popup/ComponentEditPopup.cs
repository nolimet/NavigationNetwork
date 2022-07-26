using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Popup
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
            var f1 = component.GetFields(BindingFlags.Instance | BindingFlags.Public);
            var f2 = component.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var allFields = f1.Concat(f2);

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
                val = val switch
                {
                    bool b => EditorGUILayout.Toggle(field.Name, b),
                    float f => EditorGUILayout.FloatField(field.Name, f),
                    double d => EditorGUILayout.DoubleField(field.Name, d),
                    string s => EditorGUILayout.TextField(field.Name, s),
                    int i => EditorGUILayout.IntField(field.Name, i),
                    long l => EditorGUILayout.LongField(field.Name, l),
                    Color c => EditorGUILayout.ColorField(field.Name, c),
                    _ => val,
                };

                field.SetValue(displayData.component, val);
            }
        }
    }
}