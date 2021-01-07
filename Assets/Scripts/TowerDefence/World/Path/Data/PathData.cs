using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.World.Path.Data
{
    [Serializable]
    public class PathData
    {
        public readonly PathPoint[] pathPoints;

        public PathData(PathPoint[] pathPoints)
        {
            this.pathPoints = pathPoints;
        }
    }

    [Serializable]
    public class PathPoint
    {
        public readonly Guid pointID;
        public readonly Vector3 position;
        public readonly PointType pointType;

        public readonly Guid[] pointConnections;

        public PathPoint(Guid pointID, Vector3 position, PointType pointType, Guid[] pointConnections)
        {
            this.pointID = pointID;
            this.position = position;
            this.pointType = pointType;
            this.pointConnections = pointConnections;
        }
    }

    [Serializable]
    public enum PointType
    {
        Entrance = 0,
        Point = 1,
        Exit = 2
    }
}