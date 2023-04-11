using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Data;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.EditorScripts.Entities.Components.Popup
{
    internal sealed class AddComponentPopup : PopupWindowContent
    {
        private readonly IReadOnlyDictionary<string, Type> values;
        private readonly ComponentConfigurationObject towerConfigurationObject;
        private readonly SerializedObject serializedObject;
        private readonly IReadOnlyList<string> restirctedTypes;

        private string selectedValue = string.Empty;

        private Vector2 scrollRectPosition = Vector2.zero;

        public AddComponentPopup(IReadOnlyDictionary<string, Type> values, SerializedObject serializedObject, ComponentConfigurationObject towerConfigurationObject)
        {
            this.values = values.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            this.towerConfigurationObject = towerConfigurationObject;
            this.serializedObject = serializedObject;

            var invertedValues = this.values.ToDictionary(x => x.Value, x => x.Key);

            var usedTypes = towerConfigurationObject.Components.Select(x => x.Type).ToArray();
            var allTypes = values.Values;
            restirctedTypes = allTypes.Where(
                type =>
                {
                    var compAtt = type.GetCustomAttribute<ComponentAttribute>(true);
                    return usedTypes.Any(other => compAtt.AnyRestrictionsMatch(type, other));
                }
            ).Select(x => invertedValues[x]).ToArray();
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(400, 800);
        }

        public override void OnGUI(Rect rect)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                using (new EditorGUI.DisabledGroupScope(selectedValue == string.Empty))
                {
                    if (GUILayout.Button("Add"))
                    {
                        var newComponent = Activator.CreateInstance(values[selectedValue]) as IComponent;
                        var newComponentData = new ComponentData();
                        newComponentData.SerializeComponent(newComponent);
                        towerConfigurationObject.Components.Add(newComponentData);
                        serializedObject.Update();

                        editorWindow.Close();
                    }
                }

                if (GUILayout.Button("Cancel"))
                {
                    editorWindow.Close();
                }
            }

            using (var scrollRect = new EditorGUILayout.ScrollViewScope(scrollRectPosition))
            {
                foreach (string value in values.Keys)
                {
                    using (new EditorGUI.DisabledGroupScope(value == selectedValue || restirctedTypes.Contains(value)))
                    {
                        if (GUILayout.Button(value))
                        {
                            selectedValue = value;
                        }
                    }
                }

                scrollRectPosition = scrollRect.scrollPosition;
            }
        }
    }
}