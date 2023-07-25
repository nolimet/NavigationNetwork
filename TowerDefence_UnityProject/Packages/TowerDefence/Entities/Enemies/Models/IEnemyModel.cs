using System.Collections.Generic;
using DataBinding.BaseClasses;
using DataBinding.Helpers;
using TowerDefence.Entities.Components;
using TowerDefence.UI.Game.Health;
using UnityEngine;

namespace TowerDefence.Entities.Enemies.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface IEnemyModel : IModelBase
    {
        HealthDrawer HealthBar { get; set; }
        double Health { get; set; }
        double MaxHealth { get; set; }
        double VirtualHealth { get; set; }
        Vector3 HealthOffset { get; set; }

        IList<IComponent> Components { get; set; }
    }
}