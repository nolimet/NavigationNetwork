using DataBinding.BaseClasses;
using DataBinding.Helpers;
using TowerDefence.UI.Health;
using UnityEngine;

namespace TowerDefence.Entities.Enemies.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface IEnemyBase : IModelBase
    {
        TowerDefence.Entities.Enemies.EnemyBase obj { get; set; }
        HealthDrawer healthBar { get; set; }
        Transform transform { get; set; }
        double health { get; set; }
        double maxHealth { get; set; }
        Vector3 healthOffset { get; set; }
    }
}