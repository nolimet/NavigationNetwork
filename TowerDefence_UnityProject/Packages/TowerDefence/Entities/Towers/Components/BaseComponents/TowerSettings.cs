using Newtonsoft.Json;
using System;
using TowerDefence.Entities.Components;

namespace TowerDefence.Entities.Towers.Components.BaseComponents
{
    [Serializable, Component(ComponentType.Tower)]
    internal class TowerSettings : IComponent
    {
        [JsonProperty] public readonly string Name;
        [JsonProperty] public readonly double Range;
    }
}