using Newtonsoft.Json;
using System;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Data;

namespace TowerDefence.Entities.Towers
{
    internal class DisplayData
    {
        public bool isExpanded;
        public string displayJson;
        public string componentName;

        public ComponentData towerComponentData;
        public IComponent towerComponent;
        public Type componentType;

        public void ComponentToJson()
        {
            displayJson = JsonConvert.SerializeObject(towerComponent, Formatting.Indented);
        }

        public void ComponentFromJson()
        {
            towerComponent = JsonConvert.DeserializeObject(displayJson, componentType) as IComponent;
        }

        public void UpdateTowerComponentData()
        {
            towerComponentData.SerializeComponent(towerComponent);
        }
    }
}