using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal interface IGridNode
    {
        Vector2Int Position { get; }
        float NodeWeight { get; }
        bool HasStructure { get; }
        bool HasVirtualStructure { get; }
        IReadOnlyCollection<IGridNode> ConnectedNodes { get; }

        float GetCost(IGridNode goal);
    }
}
