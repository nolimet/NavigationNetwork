﻿using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal interface IGridNode
    {
        Vector2Int Position { get; }
        double NodeWeight { get; }
        bool HasStructure { get; }
        IReadOnlyCollection<IGridNode> ConnectedNodes { get; }
    }
}
