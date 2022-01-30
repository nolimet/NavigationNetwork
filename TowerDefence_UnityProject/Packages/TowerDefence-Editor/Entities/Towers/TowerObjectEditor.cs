using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TowerDefence.Entities.Towers.Builder;
using TowerDefence.Entities.Towers.Builder.Data;
using TowerDefence.Entities.Towers.Components;
using TowerDefence.Entities.Towers.Popup;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.Entities.Towers
{
    [CustomEditor(typeof(TowerConfigurationObject))]
    internal class TowerObjectEditor : Editor
    {
        private readonly List<Type> componentTypes = new();
        private readonly List<string> componentNames = new();

        private readonly Dictionary<TowerComponentData, ITowerComponent> componentsCache = new();

        public override void OnInspectorGUI()
        {
            var target = this.target as TowerConfigurationObject;
            if (GUILayout.Button("Add Component"))
            {
                var popup = new StringPopup(componentNames);
                PopupWindow.Show(GUILayoutUtility.GetLastRect(), popup);

                Debug.Log(popup.SelectedValue);
            }

            foreach (var type in componentNames)
            {
                EditorGUILayout.LabelField(type);
            }
        }

        private void OnEnable()
        {
            componentsCache.Clear();

            //Get all the marked components
            AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.ToLower().Contains("towerdefence")))
            {
                componentTypes.AddRange(assembly.GetTypes().Where(t => t.IsDefined(typeof(TowerComponentAttribute)) && !t.IsAbstract));
            }

            foreach (var type in componentTypes)
            {
                componentNames.Add(type.ToString().Replace("TowerDefence.Entities.Towers.Components.", ""));
            }
        }
    }
}