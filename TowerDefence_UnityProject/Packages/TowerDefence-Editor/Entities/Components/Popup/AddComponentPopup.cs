using System;
using System.Collections.Generic;
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

        public string SelectedValue { get; private set; } = string.Empty;

        private Vector2 scrollRectPosition = Vector2.zero;

        public AddComponentPopup(IReadOnlyDictionary<string, Type> values, ComponentConfigurationObject towerConfigurationObject)
        {
            this.values = values;
            this.towerConfigurationObject = towerConfigurationObject;
        }

        public override Vector2 GetWindowSize()
        {
            return new Vector2(400, 800);
        }

        public override void OnGUI(Rect rect)
        {
            using (var h1 = new EditorGUILayout.HorizontalScope())
            {
                using (var d1 = new EditorGUI.DisabledGroupScope(SelectedValue == string.Empty))
                {
                    if (GUILayout.Button("Add"))
                    {
                        var newComponent = Activator.CreateInstance(values[SelectedValue]) as IComponent;
                        var newComponentData = new ComponentData();
                        newComponentData.SerializeComponent(newComponent);
                        towerConfigurationObject.components.Add(newComponentData);

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
                    using (var disableGroup = new EditorGUI.DisabledGroupScope(value == SelectedValue))
                    {
                        if (GUILayout.Button(value))
                        {
                            SelectedValue = value;
                        }
                    }
                }

                scrollRectPosition = scrollRect.scrollPosition;
            }
        }
    }
}