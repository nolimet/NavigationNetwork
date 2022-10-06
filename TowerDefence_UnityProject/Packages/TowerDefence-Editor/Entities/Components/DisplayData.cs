using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Data;
using UnityEditor;

namespace TowerDefence.EditorScripts.Entities.Components
{
    internal sealed class DisplayData
    {
        public bool IsExpanded;
        public string DisplayJson;
        public string ComponentName;

        public ComponentData ComponentData;
        public IComponent Component;
        public Type ComponentType;
        public SerializedProperty serializedProperty;

        public void ComponentToJson()
        {
            DisplayJson = JsonConvert.SerializeObject(Component, Formatting.Indented);
        }

        public void ComponentFromJson()
        {
            Component = JsonConvert.DeserializeObject(DisplayJson, ComponentType) as IComponent;
        }

        public void UpdateTowerComponentData()
        {
            ComponentData.SerializeComponent(Component);
        }
    }
}