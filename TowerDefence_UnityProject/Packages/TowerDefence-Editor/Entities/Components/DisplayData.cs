using Newtonsoft.Json;
using System;
using TowerDefence.Entities.Components.Data;

namespace TowerDefence.Entities.Components
{
    internal class DisplayData
    {
        public bool isExpanded;
        public string displayJson;
        public string componentName;

        public ComponentData componentData;
        public IComponent component;
        public Type componentType;

        public void ComponentToJson()
        {
            displayJson = JsonConvert.SerializeObject(component, Formatting.Indented);
        }

        public void ComponentFromJson()
        {
            component = JsonConvert.DeserializeObject(displayJson, componentType) as IComponent;
        }

        public void UpdateTowerComponentData()
        {
            componentData.SerializeComponent(component);
        }
    }
}