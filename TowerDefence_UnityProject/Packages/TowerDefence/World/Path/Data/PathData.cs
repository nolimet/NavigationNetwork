using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace TowerDefence.World.Path.Data
{
    [Serializable]
    public readonly struct PathData
    {
        [JsonProperty("PathPoints", Required = Required.Always)]
        public readonly PathPoint[] PathPoints;

        [JsonConstructor]
        public PathData(PathPoint[] pathPoints)
        {
            PathPoints = pathPoints;
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

        [JsonProperty("Id", Required = Required.Always)]
        public readonly Guid ID;

        [JsonProperty("Position", Required = Required.Always)]
        public readonly Vector3 Position;

        [JsonProperty("Type", Required = Required.Always), JsonConverter(typeof(StringEnumConverter))]
        public readonly PointType Type;

        [JsonProperty("Connections", NullValueHandling = NullValueHandling.Ignore)]
        public readonly Guid[] Connections;

        [JsonConstructor]
        public PathPoint(Guid id, Vector3 position, PointType type, Guid[] connections)
        {
            ID = id;
            Position = position;
            Type = type;
            Connections = connections;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override bool Equals(object obj)
        {
            if (obj is not PathPoint point) return false;
            return point.ID == ID && point.Type == Type && point.Connections == Connections && point.Position == Position;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [Serializable]
    public enum PointType
    {
        [EnumMember(Value = "Entrance")] Entrance = 0,

        [EnumMember(Value = "Point")] Point = 1,

        [EnumMember(Value = "Exit")] Exit = 2
    }
}