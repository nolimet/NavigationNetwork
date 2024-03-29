﻿using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.World.Grid.Data
{
    internal interface IGridCell
    {
        Vector2Int Position { get; }
        Vector2 WorldPosition { get; }
        float CellWeight { get; }
        bool HasStructure { get; }
        bool HasVirtualStructure { get; }
        bool SupportsTower { get; }

        IReadOnlyCollection<IGridCell> ConnectedCells { get; }

        float GetCost(IGridCell goal);

        void SetStructure(bool hasStructure);

        void SetVirtualStructure(bool hasVirtualStructure);
    }
}