using System.Collections.Generic;
using System.Linq;
using DataBinding.BaseClasses;
using DataBinding.Helpers;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.Systems.LevelEditor.Models
{
    [DataModel(AddToZenject = false, Shared = false)]
    public interface IWorldLayoutModel : IModelBase
    {
        int Height { get; set; }
        int Width { get; set; }

        IList<ICellModel> Cells { get; }
        IList<Vector2Int> EntryPoints { get; }
        IList<Vector2Int> ExitPoints { get; }

        internal GridSettings ToGridSettings()
        {
            var cells = new GridSettings.Cell[Cells.Count];
            for (var i = 0; i < Cells.Count; i++)
            {
                cells[i] = Cells[i].ToGridCell();
            }

            return new GridSettings(Height, Width, cells, EntryPoints.ToArray(), ExitPoints.ToArray());
        }
    }

    [DataModel(AddToZenject = false, Shared = false)]
    public interface ICellModel : IModelBase
    {
        public byte Weight { get; set; }
        public bool SupportsTower { get; set; }

        internal GridSettings.Cell ToGridCell()
        {
            return new GridSettings.Cell(Weight, SupportsTower);
        }
    }
}