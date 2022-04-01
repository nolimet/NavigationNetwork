using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class GridNode : IGridNode
    {
        private IReadOnlyCollection<IGridNode> connectedNodes;

        public float NodeWeight { get; }
        public bool HasStructure { get; private set; }
        public bool HasVirtualStructure { get; private set; }

        public IReadOnlyCollection<IGridNode> ConnectedNodes
        {
            get
            {
                if (!HasStructure && !HasVirtualStructure)
                    return connectedNodes;
                return Array.Empty<IGridNode>();
            }
        }

        public Vector2Int Position { get; }

        public GridNode(float nodeWeight, Vector2Int position)
        {
            NodeWeight = nodeWeight;
            Position = position;
        }

        public void SetConnectedNodes(IReadOnlyCollection<IGridNode> connectedNodes)
        {
            this.connectedNodes = connectedNodes;
        }

        public float GetCost(IGridNode goal) => this.NodeWeight;
    }
}