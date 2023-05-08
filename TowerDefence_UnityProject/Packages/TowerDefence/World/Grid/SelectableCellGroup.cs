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
                selectionBounds = new Bounds[value.Length][];
                for (int x = 0; x < value.Length; x++)
                {
                    var subGroup = value[x];
                    selectionBounds[x] = new Bounds [subGroup.Length];
                    for (int y = 0; y < subGroup.Length; y++)
                    {
                        selectionBounds[x][y] = new Bounds(subGroup[y].WorldPosition, Vector3.one);
                    }
                }
            }
        }

        public Bounds[][] selectionBounds { get; private set; }
    }
}