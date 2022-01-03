using DataBinding.BaseClasses;
using DataBinding.Helpers;
using System.Collections.Generic;

namespace TowerDefence.World.Towers.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ITowerModels : IModelBase
    {
        IList<ITowerModel> Towers { get; set; }

        TowerBase SelectedTower { get; set; }
    }
}