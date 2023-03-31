using System;
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
        private readonly List<string> validationResults = new();

        public override void OnInspectorGUI()
        {
            //TODO Add component type selector
            //TODO Add data validator
            //TODO Add data updater

            var config = target as ComponentConfigurationObject;
            config!.Type = (ComponentType)EditorGUILayout.EnumPopup("Components Type", config.Type);

            if (config.Components.Count != componentsCache.Count || !config.Components.All(x => componentsCache.Keys.Any(c => x == c)))
            {
                RebuildComponentCache();
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Add Component"))
                {
                    var popup = new AddComponentPopup(componentTypesMap[config.Type], serializedObject, config);
                    PopupWindow.Show(GUILayoutUtility.GetLastRect(), popup);
                }

                using (new EditorGUI.DisabledGroupScope(validationResults.Any()))
                {
                    if (GUILayout.Button("Save") && ValidateComponents())
                    {
                        SerializeComponents();
                        EditorUtility.SetDirty(config);
                        AssetDatabase.SaveAssetIfDirty(config);
                        AssetDatabase.Refresh();
                    }
                }

                if (GUILayout.Button("Validate"))
                {
                    ValidateComponents();
                }
            }

            if (validationResults.Any())
            {
                foreach (var result in validationResults)
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

                    if (GUILayout.Button("Remove")) config.Components.Remove(componentData);
                }

                if (!displayData.IsExpanded) continue;

                if (displayData.serializedComponent.hasChildren)
                {
                    using var c = new EditorGUI.ChangeCheckScope();
                    int depth = displayData.serializedComponent.depth;
                    foreach (SerializedProperty child in displayData.serializedProperty.FindPropertyRelative(nameof(componentData.SerializedComponent)))
                    {
                        if (child.depth != depth + 1) continue;
                        EditorGUILayout.PropertyField(child, true);
                    }

                    if (c.changed)
                        serializedObject.ApplyModifiedProperties();
                }

                displayData.JsonEditorDrawer.OnGUI();

                using (new EditorGUI.DisabledGroupScope(true))
                    EditorGUILayout.TextArea(displayData.DisplayJson);
            }
        }

        private void SerializeComponents()
        {
            var config = target as ComponentConfigurationObject;

            foreach (var component in componentsCache)
            {
                component.Key.SerializeComponent(component.Value.Component);
                component.Value.ComponentToJson();
            }

            config!.Components = componentsCache.Keys.ToList();
        }

        private void RebuildComponentCache()
        {
            componentsCache.Clear();

            var config = target as ComponentConfigurationObject;

            for (var i = 0; i < config!.Components.Count; i++)
            {
                var component = config!.Components[i];
                var displayData = new DisplayData
                {
                    ComponentData = component
                };

                component.SetReferenceValue();

                displayData.ComponentType = displayData.Component.GetType();
                displayData.ComponentName = componentTypesMap[config.Type].First(x => x.Value == displayData.ComponentType).Key;
                displayData.ComponentToJson();

                displayData.serializedProperty = serializedObject.FindProperty("Components").GetArrayElementAtIndex(i);
                displayData.serializedComponent = displayData.serializedProperty.FindPropertyRelative("SerializedComponent");
                displayData.JsonEditorDrawer = new ComponentJsonDataDrawer(displayData, serializedObject);

                componentsCache.Add(component, displayData);
            }
        }

        private void OnEnable()
        {
            validationResults.Clear();
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
                    .ForEach(type => componentTypesMap.Add(type, new Dictionary<string, Type>()));

                foreach (var component in components)
                {
                    var att = component.GetCustomAttribute<ComponentAttribute>(true);
                    string componentName = component.ToString();

                    componentName = att.ComponentType switch
                    {
                        ComponentType.Enemy => componentName.Replace("TowerDefence.Entities.Enemies.Components.", ""),
                        ComponentType.Tower => componentName.Replace("TowerDefence.Entities.Towers.Components.", ""),
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    componentTypesMap[att.ComponentType].Add(componentName, component);
                    componentAttributesMap.Add(component, att);
                }
            }

            RebuildComponentCache();
        }

        private bool ValidateComponents()
        {
            validationResults.Clear();

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
                    if (i == j) continue;

                    duplicates = !duplicates && self == other;
                    if (!componentAttributesMap[self].AnyRestrictionsMatch(self, other)) continue;

                    string result = "";
                    if (self == other)
                    {
                        result += $"Duplicates of type {self}\n";
                    }
                    else
                    {
                        result += $"Cannot use {self} with {other}\n";
                    }

                    validationResults.Add(result);
                }
            }

            return !(duplicates || invalidCombinations);
        }
    }
}