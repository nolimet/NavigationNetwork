using System;
using Newtonsoft.Json;
using TowerDefence.EditorScripts.Entities.Components.Popup;
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
        public ComponentJsonDataDrawer JsonEditorDrawer;

        public IComponent Component => ComponentData.SerializedComponent;
        public Type ComponentType;

        public SerializedProperty serializedProperty;
        public SerializedProperty serializedComponent;

        public void ComponentToJson()
        {
            DisplayJson = JsonConvert.SerializeObject(Component, Formatting.Indented);
        }

        public void ComponentFromJson()
        {
            ComponentData.SerializedComponent = JsonConvert.DeserializeObject(DisplayJson, ComponentType) as IComponent;
        }

        public void UpdateTowerComponentData()
        {
            ComponentData.SerializeComponent(ComponentData.SerializedComponent);
        }
    }
}