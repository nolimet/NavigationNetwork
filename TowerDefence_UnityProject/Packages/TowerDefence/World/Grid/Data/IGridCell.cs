using System.Collections.Generic;
using TowerDefence.Systems.Selection;
using UnityEngine;

namespace TowerDefence.World.Grid.Data
{
    public interface IGridCell : ISelectable
    {
        Vector2Int Position { get; }
        Vector2 WorldPosition { get; }
        byte CellWeight { get; }
        bool HasStructure { get; }
        bool HasVirtualStructure { get; }
        bool SupportsTower { get; }

        IReadOnlyCollection<IGridCell> ConnectedCells { get; }

        byte GetCost(IGridCell goal);

        void SetStructure(bool hasStructure);

        void SetVirtualStructure(bool hasVirtualStructure);
    }
}
