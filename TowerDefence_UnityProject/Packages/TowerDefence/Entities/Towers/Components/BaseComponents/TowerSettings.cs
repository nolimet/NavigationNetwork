using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.UI.Game.Tower.Properties.Attributes;

namespace TowerDefence.Entities.Towers.Components.BaseComponents
{
    [Serializable, Component(ComponentType.Tower)]
    [JsonObject(MemberSerialization.OptIn)]
    internal sealed class TowerSettings : IComponent
    {
        [HiddenProperty] [JsonProperty] public readonly string Name;
        [HiddenProperty] [JsonProperty] public readonly bool IsSolid;
        [JsonProperty] public readonly double Range;
    }
}