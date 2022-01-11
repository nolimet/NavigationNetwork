using DataBinding.BaseClasses;
using DataBinding.Helpers;
using System.Collections.Generic;
using TowerDefence.Entities.Towers.Components;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface ITowerModel : IModelBase
    {
        Vector3 Position { get; set; }
        double Range { get; set; }
        TowerBase TowerRenderer { get; set; }
        ITowerObject TowerObject { get; set; }
        IList<ITowerComponent> Components { get; set; }
    }
}