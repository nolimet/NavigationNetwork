using DataBinding.BaseClasses;
using DataBinding.Helpers;
using System.Collections.Generic;

namespace TowerDefence.Entities.Towers.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ITowerModels : IModelBase
    {
        IList<ITowerObject> Towers { get; set; }

        ITowerObject SelectedTower { get; set; }
    }
}