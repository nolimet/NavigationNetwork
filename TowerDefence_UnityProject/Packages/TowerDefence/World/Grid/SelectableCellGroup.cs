using System.Collections.Generic;
using NoUtil;
using TowerDefence.Systems.Selection;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    public sealed class SelectableCellGroup : MonoBehaviour, ISelectable
    {
        private IGridCell[][] gridCellGroup;

        public IGridCell[][] GridCellGroup
        {
            get => gridCellGroup;
            set
            {
                gridCellGroup = value;
                SelectionBounds = new Bounds[value.Length][];
                for (var x = 0; x < value.Length; x++)
                {
                    var subGroup = value[x];
                    SelectionBounds[x] = new Bounds [subGroup.Length];
                    for (var y = 0; y < subGroup.Length; y++) SelectionBounds[x][y] = new Bounds(subGroup[y].WorldPosition, Vector3.one);
                }
            }
        }

        public Bounds[][] SelectionBounds { get; private set; }

        public IGridCell GetSelectedCell(Vector2 worldPoint)
        {
            for (var x = 0; x < SelectionBounds.Length; x++)
            for (var y = 0; y < SelectionBounds[x].Length; y++)
                if (SelectionBounds[x][y].Contains(worldPoint))
                    return GridCellGroup[x][y];

            return null;
        }

        public IEnumerable<ISelectable> GetSelectedCells(Bounds area)
        {
            List<IGridCell> hitCells = new();
            for (var x = 0; x < SelectionBounds.Length; x++)
            for (var y = 0; y < SelectionBounds[x].Length; y++)
                if (area.ContainBounds(SelectionBounds[x][y]))
                    hitCells.Add(GridCellGroup[x][y]);
            return hitCells;
        }
    }
}