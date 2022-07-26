using Newtonsoft.Json;
using System;
using TowerDefence.Entities.Components;

namespace TowerDefence.Entities.Enemies.Components.BaseComponents
{
    [Serializable, Component(ComponentType.Enemy, typeof(EnemySettings))]
    internal class EnemySettings : IComponent
    {
        [JsonProperty] public readonly string TypeName = "";
        [JsonProperty] public readonly string BaseName = "";

        [JsonProperty] public readonly double MaxHealth = 10;
        [JsonProperty] public readonly float Speed = 5;
    }
}