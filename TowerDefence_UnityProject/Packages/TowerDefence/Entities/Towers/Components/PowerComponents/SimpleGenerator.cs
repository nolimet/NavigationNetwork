using System;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.PowerComponents.Bases;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable, Component(ComponentType.Tower, typeof(IPowerComponent), AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    internal class SimpleGenerator : BaseGenerator
    {
    }
}