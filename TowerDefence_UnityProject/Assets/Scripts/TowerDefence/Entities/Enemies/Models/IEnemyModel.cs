using DataBinding.BaseClasses;
using DataBinding.Helpers;
using TowerDefence.UI.Health;
using UnityEngine;

namespace TowerDefence.Entities.Enemies.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface IEnemyModel : IModelBase
    {
        EnemyBase Obj { get; set; }
        HealthDrawer HealthBar { get; set; }
        Transform Transform { get; set; }
        double Health { get; set; }
        double MaxHealth { get; set; }
        Vector3 HealthOffset { get; set; }
    }
}