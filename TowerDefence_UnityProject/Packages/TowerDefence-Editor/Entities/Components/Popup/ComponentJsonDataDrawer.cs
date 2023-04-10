using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.EditorScripts.Entities.Components.Popup
{
    internal sealed class ComponentJsonDataDrawer
    {
        private static readonly Regex splitNameRegex = new(@"((?<=\p{Ll})\p{Lu}|\p{Lu}(?=\p{Ll}))");

        private readonly DisplayData displayData;
        private readonly MemberInfo[] members;
        private readonly SerializedObject serializedObject;

        public ComponentJsonDataDrawer(DisplayData displayData, SerializedObject serializedObject)
        {
            this.displayData = displayData;
            this.serializedObject = serializedObject;

            var jsonPropertyType = typeof(JsonPropertyAttribute);
            var unitySerializableType = typeof(SerializeField);
            var component = displayData.Component.GetType();

            var searchQuery = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy;
            var allFields = component.GetFields(searchQuery);
            var allProperties = component.GetProperties(searchQuery).Where(x => !x.CanWrite);

            //filtering out fields that don't have the jsonProperty and do have the serializedField Property
            members = allFields.Where
            (
                x =>
                    x.CustomAttributes.Any(c => c.AttributeType == jsonPropertyType) &&
                    x.CustomAttributes.All(c => c.AttributeType != unitySerializableType)
            ).Concat<MemberInfo>(
                allProperties.Where(
                    x =>
                        x.CustomAttributes.Any(c => c.AttributeType == jsonPropertyType) &&
                        x.CustomAttributes.All(c => c.AttributeType != unitySerializableType)
                )
            ).OrderBy(
                x => x.Name).ToArray();
        }

        public void OnGUI()
        {
            foreach (var memberInfo in members) DrawMember(memberInfo);
        }

        private void DrawMember(MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case FieldInfo fieldInfo:
                    DrawField(fieldInfo);
                    break;
                case PropertyInfo propertyInfo:
                    DrawProperty(propertyInfo);
                    break;
            }
        }

        private void DrawField(FieldInfo field)
        {
            var val = field.GetValue(displayData.Component);
            if (val is null)
            {
                var type = field.FieldType;
                if (type.IsValueType)
                    val = Activator.CreateInstance(field.FieldType);
                else if (type == typeof(string)) val = string.Empty;
            }

            //Handling most basic value types
            var newVal = DrawObject(val, field.Name);

            if (newVal == val || newVal.Equals(val)) return;

            field.SetValue(displayData.Component, newVal);
            serializedObject.Update();
        }

        private void DrawProperty(PropertyInfo property)
        {
            var val = property.GetValue(displayData.Component);
            if (val is null)
            {
                var type = property.PropertyType;
                if (type.IsValueType)
                    val = Activator.CreateInstance(property.PropertyType);
                else if (type == typeof(string)) val = string.Empty;
            }

            var newVal = DrawObject(val, property.Name);

            if (newVal == val || newVal.Equals(val)) return;

            property.SetValue(displayData.Component, newVal);
            serializedObject.Update();
        }

        private string FormatName(string name)
        {
            name = char.ToUpper(name[0]) + name[1..];
            return splitNameRegex.Replace(name, " $1").TrimStart(' ');
        }

        /// <summary>
        /// Draws all the simple types use serializedFields for complex types
        /// </summary>
        /// <param name="val">value you want to draw</param>
        /// <param name="name">Name used for the value</param>
        /// <returns></returns>
        private object DrawObject(object val, string name)
        {
            name = FormatName(name);
            return val switch
            {
                bool b => EditorGUILayout.Toggle(name, b),
                float f => EditorGUILayout.FloatField(name, f),
                double d => EditorGUILayout.DoubleField(name, d),
                string s => EditorGUILayout.TextField(name, s),
                int i => EditorGUILayout.IntField(name, i),
                long l => EditorGUILayout.LongField(name, l),
                Color c => EditorGUILayout.ColorField(name, c),
                Vector2 v2 => EditorGUILayout.Vector2Field(name, v2),
                Vector3 v3 => EditorGUILayout.Vector3Field(name, v3),
                Vector4 v4 => EditorGUILayout.Vector4Field(name, v4),
                Vector2Int v2i => EditorGUILayout.Vector2IntField(name, v2i),
                Vector3Int v3i => EditorGUILayout.Vector3IntField(name, v3i),
                Quaternion q => Quaternion.Euler(EditorGUILayout.Vector3Field(name, q.eulerAngles)),
                _ => val
            };
        }
    }
}
