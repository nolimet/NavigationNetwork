using System.Collections.Generic;
using System.Linq;
using DataBinding.BaseClasses;
using DataBinding.Helpers;
using TowerDefence.World.Grid.Data;

namespace TowerDefence.Entities.Towers.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface ITowerModels : IModelBase
    {
        IList<ITowerObject> Towers { get; set; }

        ITowerObject SelectedTower { get; set; }

        internal bool CellHasTower(IGridCell cell)
        {
            return Towers.Any(x => x.GetGridPosition() == cell.Position);
        }
    }
}