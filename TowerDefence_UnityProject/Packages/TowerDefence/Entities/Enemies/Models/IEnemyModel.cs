using DataBinding.BaseClasses;
using DataBinding.Helpers;
using System.Collections.Generic;
using TowerDefence.Entities.Enemies.Components;
using TowerDefence.UI.Health;
using UnityEngine;

namespace TowerDefence.Entities.Enemies.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface IEnemyModel : IModelBase
    {
        HealthDrawer HealthBar { get; set; }
        double Health { get; set; }
        double MaxHealth { get; set; }
        Vector3 HealthOffset { get; set; }

        IList<IEnemyComponent> Components { get; }
    }
}