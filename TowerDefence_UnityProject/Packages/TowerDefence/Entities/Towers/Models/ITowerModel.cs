using System.Collections.Generic;
using DataBinding.BaseClasses;
using DataBinding.Helpers;
using TowerDefence.Entities.Components;

namespace TowerDefence.Entities.Towers.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface ITowerModel : IModelBase
    {
        IList<IComponent> Components { get; set; }
    }
}