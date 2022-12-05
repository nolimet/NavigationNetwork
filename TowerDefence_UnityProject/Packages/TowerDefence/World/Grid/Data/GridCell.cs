using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.World.Grid.Data
{
    internal sealed class GridCell : IGridCell
    {
        private IReadOnlyCollection<IGridCell> conntectedCells;

        public float CellWeight { get; }
        public bool SupportsTower { get; }
        public bool HasStructure { get; private set; }
        public bool HasVirtualStructure { get; private set; }


        public IReadOnlyCollection<IGridCell> ConnectedCells
        {
            get
            {
                if (!HasStructure && !HasVirtualStructure)
                    return conntectedCells;
                return Array.Empty<IGridCell>();
            }
        }

        public Vector2Int Position { get; }

        public Vector2 WorldPosition { get; }

        public GridCell(float cellWeight, Vector2Int position, Vector2 worldPosition, bool supportsTower)
        {
            CellWeight = cellWeight;
            Position = position;
            WorldPosition = worldPosition;
            SupportsTower = supportsTower;
        }

        public void SetConnectedCells(IReadOnlyCollection<IGridCell> connectedCells)
        {
            conntectedCells = connectedCells;
        }

        public float GetCost(IGridCell goal) => CellWeight;
        public void SetStructure(bool hasStructure) => HasStructure = hasStructure;
        public void SetVirtualStructure(bool hasVirtualStructure) => HasVirtualStructure = hasVirtualStructure;
    }
}