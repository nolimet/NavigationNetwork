using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class GridNode : IGridNode
    {
        public double NodeWeight { get; private set; }

        public bool HasStructure { get; private set; }

        public IReadOnlyCollection<IGridNode> ConnectedNodes { get; private set; }

        public Vector2Int Position { get; private set; }

        public GridNode(double nodeWeight, Vector2Int position)
        {
            NodeWeight = nodeWeight;
            Position = position;
        }

        public void SetConnectedNodes(IReadOnlyCollection<IGridNode> connectedNodes)
        {
            ConnectedNodes = connectedNodes;
        }

    }
}