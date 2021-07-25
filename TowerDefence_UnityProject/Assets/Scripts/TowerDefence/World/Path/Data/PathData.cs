using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace TowerDefence.World.Path.Data
{
    [Serializable]
    public readonly struct PathData
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
    public readonly struct PathPoint
    {
        public static readonly PathPoint Empty = default;

        public static bool operator !=(PathPoint a, PathPoint b)
        {
            return !a.Equals(b);
        }

        public static bool operator ==(PathPoint a, PathPoint b)
        {
            return a.Equals(b);
        }

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

        public override bool Equals(object obj)
        {
            if (obj is PathPoint point)
            {
                if (point.id == id && point.type == type && point.connections == connections && point.position == position)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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