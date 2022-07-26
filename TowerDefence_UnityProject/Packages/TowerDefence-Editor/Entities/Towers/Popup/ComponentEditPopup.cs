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

        public ComponentEditPopup(DisplayData displayData)
        {
            //TODO convert to new UIDocument thingy
            this.displayData = displayData;

            var jsonPropertyType = typeof(JsonPropertyAttribute);
            var component = displayData.component.GetType();
            var f1 = component.GetFields(BindingFlags.Instance | BindingFlags.Public);
            var f2 = component.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var allFields = f1.Concat(f2);

            fields = allFields.Where(x => x.CustomAttributes.Any(c => c.AttributeType == jsonPropertyType)).ToArray();
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

            DrawField();
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
