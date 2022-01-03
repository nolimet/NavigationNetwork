using DataBinding.BaseClasses;
using DataBinding.Helpers;
using UnityEngine;

namespace TowerDefence.World.Towers.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface ITowerModel : IModelBase
    {
        Vector3 Position { get; set; }
        double Range { get; set; }
        TowerBase TowerRenderer { get; set; }
    }
}