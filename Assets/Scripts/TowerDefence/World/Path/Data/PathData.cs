using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.World.Path.Data
{
    [Serializable]
    public class PathData
    {
        [JsonProperty("pathPoints")]
        public readonly PathPoint[] pathPoints;

        [JsonConstructor]
        public PathData(PathPoint[] pathPoints)
        {
            this.pathPoints = pathPoints;
        }
    }

    [Serializable]
    public class PathPoint
    {
        [JsonProperty("pointId")]
        public readonly Guid pointId;

        [JsonProperty("position")]
        public readonly Vector3 position;

        [JsonProperty("pointType")]
        public readonly PointType pointType;

        [JsonProperty("pointConnections")]
        public readonly Guid[] pointConnections;

        [JsonConstructor]
        public PathPoint(Guid pointId, Vector3 position, PointType pointType, Guid[] pointConnections)
        {
            this.pointId = pointId;
            this.position = position;
            this.pointType = pointType;
            this.pointConnections = pointConnections;
        }
    }

    [Serializable]
    public enum PointType
    {
        [EnumMember(Value = "Entrance")]
        Entrance = 0,

        [EnumMember(Value = "Point")]
        Point = 1,

        [EnumMember(Value = "Exit")]
        Exit = 2
    }
}