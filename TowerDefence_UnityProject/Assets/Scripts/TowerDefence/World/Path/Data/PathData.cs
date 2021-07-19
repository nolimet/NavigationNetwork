using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;
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
        [JsonProperty("id")]
        public readonly Guid id;

        [JsonProperty("position")]
        public readonly Vector3 position;

        [JsonProperty("type"), JsonConverter(typeof(StringEnumConverter))]
        public readonly PointType type;

        [JsonProperty("connections")]
        public readonly Guid[] connections;

        [JsonConstructor]
        public PathPoint(Guid id, Vector3 position, PointType type, Guid[] connections)
        {
            this.id = id;
            this.position = position;
            this.type = type;
            this.connections = connections;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
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