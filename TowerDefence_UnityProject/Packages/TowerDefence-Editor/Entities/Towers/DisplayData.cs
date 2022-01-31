using Newtonsoft.Json;
using System;
using TowerDefence.Entities.Towers.Builder.Data;
using TowerDefence.Entities.Towers.Components;

namespace TowerDefence.Entities.Towers
{
    internal class DisplayData
    {
        public bool isExpanded;
        public string displayJson;
        public string componentName;

        public TowerComponentData towerComponentData;
        public ITowerComponent towerComponent;
        public Type componentType;

        public void ComponentToJson()
        {
            displayJson = JsonConvert.SerializeObject(towerComponent, Formatting.Indented);
        }

        public void ComponentFromJson()
        {
            towerComponent = JsonConvert.DeserializeObject(displayJson, componentType) as ITowerComponent;
        }

        public void UpdateTowerComponentData()
        {
            towerComponentData.SerializeTowerComponent(towerComponent);
        }
    }
}