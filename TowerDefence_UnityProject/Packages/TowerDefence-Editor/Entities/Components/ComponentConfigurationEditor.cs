﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TowerDefence.EditorScripts.Entities.Components.Popup;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Data;
using UnityEditor;
using UnityEngine;

namespace TowerDefence.EditorScripts.Entities.Components
{
    [CustomEditor(typeof(ComponentConfigurationObject))]
    internal sealed class ComponentConfigurationEditor : Editor
    {
        private readonly Dictionary<ComponentType, Dictionary<string, Type>> componentTypesMap = new();
        private readonly Dictionary<ComponentData, DisplayData> componentsCache = new();
        private readonly Dictionary<Type, ComponentAttribute> componentAttributesMap = new();
        private readonly List<string> validationReslts = new();

        public override void OnInspectorGUI()
        {
            //TODO Add component type selector
            //TODO Add data validator
            //TODO Add data updater

            var target = this.target as ComponentConfigurationObject;
            target.type = (ComponentType)EditorGUILayout.EnumPopup("Components Type", target.type);

            if (target.components.Count != componentsCache.Count || !target.components.All(x => componentsCache.Keys.Any(c => x == c)))
            {
                RebuildComponentCache();
            }

            using (var h1 = new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Add Component"))
                {
                    var popup = new AddComponentPopup(componentTypesMap[target.type], target);
                    PopupWindow.Show(GUILayoutUtility.GetLastRect(), popup);
                }

                using (var d1 = new EditorGUI.DisabledGroupScope(validationReslts.Any()))
                {
                    if (GUILayout.Button("Save") && ValidateComponents())
                    {
                        SerializeComponents();
                        EditorUtility.SetDirty(target);
                        AssetDatabase.SaveAssetIfDirty(target);
                        AssetDatabase.Refresh();
                    }
                }

                if (GUILayout.Button("Validate"))
                {
                    ValidateComponents();
                }
            }

            if (validationReslts.Any())
            {
                foreach (var result in validationReslts)
                {
                    EditorGUILayout.HelpBox(result, MessageType.Error);
                }
            }
            
            foreach (var (componentData, displayData) in componentsCache)
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    displayData.IsExpanded = EditorGUILayout.Foldout(displayData.IsExpanded, displayData.ComponentName, true);
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Edit"))
                    {
                        var popup = new ComponentEditPopup(displayData);
                        PopupWindow.Show(GUILayoutUtility.GetLastRect(), popup);
                    }

                    if (GUILayout.Button("Remove"))
                    {
                        target.components.Remove(componentData);
                    }
                }

                if (displayData.IsExpanded)
                {
                    using (new EditorGUI.IndentLevelScope(1))
                    {
                        using (new EditorGUI.DisabledGroupScope(true))
                        {
                            EditorGUILayout.TextArea(displayData.DisplayJson);
                        }
                    }
                }
            }
        }

        private void SerializeComponents()
        {
            var target = this.target as ComponentConfigurationObject;

            foreach (var component in componentsCache)
            {
                component.Key.SerializeComponent(component.Value.Component);
            }

            target!.components = componentsCache.Keys.ToList();
        }

        private void RebuildComponentCache()
        {
            componentsCache.Clear();

            var target = this.target as ComponentConfigurationObject;

            for (var i = 0; i < target!.components.Count; i++)
            {
                var component = target!.components[i];
                var displayData = new DisplayData
                {
                    Component = component.DeserializeComponent(),
                    ComponentData = component
                };

                displayData.ComponentType = displayData.Component.GetType();
                displayData.ComponentName = componentTypesMap[target.type]
                    .First(x => x.Value == displayData.ComponentType).Key;
                displayData.ComponentToJson();
                displayData.ComponentData.SerializedComponent = displayData.Component;
                displayData.serializedProperty = serializedObject.FindProperty("components").GetArrayElementAtIndex(i);

                componentsCache.Add(component, displayData);
            }
        }

        private void OnEnable()
        {
            validationReslts.Clear();
            componentTypesMap.Clear();
            componentAttributesMap.Clear();

            //Get all the marked components
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies.Where(x => x.FullName.ToLower().Contains("towerdefence")))
            {
                var components = assembly.GetTypes()
                    .Where
                    (type =>
                            type.IsDefined(typeof(ComponentAttribute)) && //Checking if it the required attribute
                            !type.IsAbstract &&
                            type.GetInterfaces().Any(x => x == typeof(IComponent)) //And that it implements the interface.
                    );

                Enum.GetValues(typeof(ComponentType)).Cast<ComponentType>().Distinct()
                    .Where(x => !componentTypesMap.ContainsKey(x))
                    .ToList()
                    .ForEach(type => componentTypesMap.Add(type, new()));

                foreach (var component in components)
                {
                    var att = component.GetCustomAttribute<ComponentAttribute>();
                    string name = component.ToString();

                    name = att.ComponentType switch
                    {
                        ComponentType.Enemy => name.Replace("TowerDefence.Entities.Enemies.Components.", ""),
                        ComponentType.Tower => name.Replace("TowerDefence.Entities.Towers.Components.", ""),
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    componentTypesMap[att.ComponentType].Add(name, component);
                    componentAttributesMap.Add(component, att);
                }
            }

            RebuildComponentCache();
        }

        private bool ValidateComponents()
        {
            validationReslts.Clear();

            var usedTypes = componentsCache.Values.Select(x => x.ComponentType).ToArray();

            bool duplicates = false;
            bool invalidCombinations = false;

            int length = usedTypes.Length;
            for (int i = 0; i < length; i++)
            {
                var self = usedTypes[i];
                for (int j = 0; j < length; j++)
                {
                    var other = usedTypes[j];
                    if (i != j)
                    {
                        duplicates = !duplicates && self == other;
                        if (componentAttributesMap[self].AnyRestrictionsMatch(self, other))
                        {
                            string result = "";
                            if (self == other)
                            {
                                result += $"Duplicates of type {self}\n";
                            }
                            else
                            {
                                result += $"Cannot use {self} with {other}\n";
                            }

                            validationReslts.Add(result);
                        }
                    }
                }
            }

            return !(duplicates || invalidCombinations);
        }
    }
}