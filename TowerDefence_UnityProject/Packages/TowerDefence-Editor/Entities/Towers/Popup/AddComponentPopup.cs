using System;
using System.Collections.Generic;
using TowerDefence.Entities.Towers.Builder.Data;
using TowerDefence.Entities.Towers.Components;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Popup
{
    internal class AddComponentPopup : PopupWindowContent
    {
        private readonly IReadOnlyDictionary<string, Type> values;
        private readonly TowerConfigurationObject towerConfigurationObject;

        public string SelectedValue { get; private set; } = string.Empty;

        private Vector2 scrollRectPosition = Vector2.zero;

        public AddComponentPopup(IReadOnlyDictionary<string, Type> values, TowerConfigurationObject towerConfigurationObject)
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
                        var newComponent = Activator.CreateInstance(values[SelectedValue]) as ITowerComponent;
                        var newComponentData = new TowerComponentData();
                        newComponentData.SerializeTowerComponent(newComponent);
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