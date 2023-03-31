using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.EditorScripts.Entities.Components.Popup
{
    internal sealed class ComponentJsonDataDrawer
    {
        private readonly DisplayData displayData;
        private readonly FieldInfo[] fields;
        private readonly SerializedObject serializedObject;

        public ComponentJsonDataDrawer(DisplayData displayData, SerializedObject serializedObject)
        {
            this.displayData = displayData;
            this.serializedObject = serializedObject;

            var jsonPropertyType = typeof(JsonPropertyAttribute);
            var unitySerializableType = typeof(SerializeField);
            var component = displayData.Component.GetType();

            //Need specific flags as you can't just get the readonly fields
            var publicFields = component.GetFields(BindingFlags.Instance | BindingFlags.Public);
            var privateFields = component.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            var allFields = publicFields.Concat(privateFields);

            //filtering out fields that don't have the jsonProperty and do have the serializedField Property
            fields = allFields.Where
            (
                x =>
                    x.CustomAttributes.Any(c => c.AttributeType == jsonPropertyType) &&
                    x.CustomAttributes.All(c => c.AttributeType != unitySerializableType)
            ).ToArray();
        }

        public void OnGUI()
        {
            foreach (var field in fields)
            {
                DrawField(field);
            }
        }

        private void DrawField(FieldInfo field)
        {
            var val = field.GetValue(displayData.Component);
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
            var newVal = val switch
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
                _ => val
            };

            if (newVal == val || newVal.Equals(val)) return;
            field.SetValue(displayData.Component, newVal);
            serializedObject.Update();
        }
    }
}