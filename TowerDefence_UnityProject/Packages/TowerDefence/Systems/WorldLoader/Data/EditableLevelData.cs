using System;
using System.Collections.Generic;
using System.Linq;
using NoUtil;
using TowerDefence.Entities.Enemies;
using TowerDefence.Systems.Waves.Data;
using TowerDefence.World.Grid.Data;
using TowerDefence.World.Path.Data;
using UnityEngine;
using UnityEngine.Serialization;
using FileEnemyGroup = TowerDefence.Systems.Waves.Data.Wave.EnemyGroup;
using FilePathPoint = TowerDefence.World.Path.Data.PathPoint;

//TODO move this
namespace TowerDefence.Systems.WorldLoader.Data
{
    [CreateAssetMenu(menuName = "Configuration/Create Level", fileName = "Level 0")]
    public sealed class EditableLevelData : ScriptableObject
    {
        [SerializeField] private EditableWave[] waves;

        // ReSharper disable once StringLiteralTypo
        [FormerlySerializedAs("pathdata")] [SerializeField]
        private EditablePathData pathData;

        [SerializeField] private EditableGridSettings gridSettings;

        internal EditablePathData PathData
        {
            get => pathData;
            private set => pathData = value;
        }

        private EditableWave[] Waves
        {
            get => waves;
            set => waves = value;
        }

        public LevelData ToLevelDataPath()
        {
            return new LevelData(Waves.Select(x => (Wave)x).ToArray(), PathData);
        }

        internal LevelData ToLevelDataGrid()
        {
            return new LevelData(Waves.Select(x => (Wave)x).ToArray(), gridSettings);
        }

        public void FromLevelData(LevelData levelData)
        {
            var waveCount = levelData.Waves?.Length ?? 0;
            var waves = new EditableWave[waveCount];

            if (levelData.Waves is not null)
                for (int i = 0; i < waveCount; i++)
                {
                    waves[i] = levelData.Waves[i];
                }

            Waves = waves;
            PathData = levelData.Path;
        }

        internal void FromGridSettings(GridSettings gridSettings)
        {
            this.gridSettings = gridSettings;
        }

        [Serializable]
        internal class EditableWave
        {
            [FormerlySerializedAs("enemyGroups")] public EnemyGroup[] EnemyGroups;

            public EditableWave()
            {
            }

            public EditableWave(EnemyGroup[] enemyGroups)
            {
                EnemyGroups = enemyGroups;
            }

            public static implicit operator Wave(EditableWave wave)
            {
                var enemyGroups = new FileEnemyGroup[wave.EnemyGroups.Length];
                for (int i = 0; i < wave.EnemyGroups.Length; i++)
                {
                    enemyGroups[i] = wave.EnemyGroups[i];
                }

                return new Wave(enemyGroups);
            }

            public static implicit operator EditableWave(Wave wave)
            {
                var enemyGroups = new EnemyGroup[wave.EnemyGroups?.Length ?? 0];
                if (wave.EnemyGroups is not { Length: > 0 }) return new EditableWave(enemyGroups);
                for (int i = 0; i < wave.EnemyGroups.Length; i++)
                {
                    enemyGroups[i] = wave.EnemyGroups[i];
                }

                return new EditableWave(enemyGroups);
            }

            [Serializable]
            public class EnemyGroup
            {
                [FormerlySerializedAs("enemyID")] [StringDropdown("enemies.id", typeof(EnemyConfigurationData))]
                public string EnemyID;

                [FormerlySerializedAs("pathID")] public int PathID;
                [FormerlySerializedAs("entranceId")] public int EntranceId;
                [FormerlySerializedAs("exitId")] public int ExitId;
                [FormerlySerializedAs("spawnTime")] public double[] SpawnTime;

                [FormerlySerializedAs("spawnInterval")]
                public double SpawnInterval;

                [FormerlySerializedAs("spawnDelay")] public double SpawnDelay;
                [FormerlySerializedAs("groupSize")] public ulong GroupSize;

                public EnemyGroup()
                {
                }

                public EnemyGroup(string enemyID, int pathID, int entranceId, int exitId, double[] spawnTime, double? spawnInterval, double? spawnDelay, ulong? groupSize)
                {
                    EnemyID = enemyID;
                    PathID = pathID;
                    EntranceId = entranceId;
                    ExitId = exitId;
                    SpawnTime = spawnTime;
                    SpawnInterval = spawnInterval ?? 0;
                    SpawnDelay = spawnDelay ?? 0;
                    GroupSize = groupSize ?? 0;
                }

                public static implicit operator FileEnemyGroup(EnemyGroup group)
                {
                    return new FileEnemyGroup
                    (
                        enemyID: group.EnemyID,
                        entranceId: group.EntranceId,
                        exitId: group.ExitId,
                        pathID: group.PathID,
                        spawnTime: group.SpawnTime,
                        groupSize: group.GroupSize,
                        spawnInterval: group.SpawnInterval,
                        spawnDelay: group.SpawnDelay
                    );
                }

                public static implicit operator EnemyGroup(FileEnemyGroup group)
                {
                    return new EnemyGroup(group.EnemyID, group.EntranceId, group.ExitId, group.PathID, group.SpawnTime, group.SpawnInterval, group.SpawnDelay, group.GroupSize);
                }
            }
        }

        [Serializable]
        internal class EditablePathData
        {
            [FormerlySerializedAs("pathPoints")] public PathPoint[] PathPoints;

            public EditablePathData()
            {
            }

            public EditablePathData(PathPoint[] pathPoints)
            {
                PathPoints = pathPoints;
            }

            public static implicit operator EditablePathData(PathData pathData)
            {
                var pathPoints = new List<PathPoint>();
                var groupedPoints = pathData.PathPoints.GroupBy(x => x.Type);
                foreach (var group in groupedPoints)
                {
                    var arr = group.ToArray();
                    for (int i = 0; i < group.Count(); i++)
                    {
                        var point = arr[i];
                        pathPoints.Add(new PathPoint($"{i + 1:00} - {group.Key}", point.ID, point.Position, point.Type, point.Connections));
                    }
                }

                return new EditablePathData(pathPoints.ToArray());
            }

            public static implicit operator PathData(EditablePathData pathData)
            {
                var pathPoints = new FilePathPoint[pathData.PathPoints.Length];
                for (int i = 0; i < pathData.PathPoints.Length; i++)
                {
                    pathPoints[i] = pathData.PathPoints[i];
                }

                return new PathData(pathPoints);
            }

            [Serializable]
            public class PathPoint
            {
                [FormerlySerializedAs("name")] public string Name;
                [FormerlySerializedAs("id")] public string ID;
                [FormerlySerializedAs("position")] public Vector3 Position;
                [FormerlySerializedAs("type")] public PointType Type;
                [FormerlySerializedAs("connections")] public string[] Connections;

                public PathPoint()
                {
                }

                public PathPoint(string name, Guid id, Vector3 position, PointType type, Guid[] connections)
                {
                    Name = name;
                    ID = id.ToString();
                    Position = position;
                    Type = type;
                    Connections = connections.Select(x => x.ToString()).ToArray();
                }

                public static implicit operator FilePathPoint(PathPoint point)
                {
                    return new FilePathPoint(new Guid(point.ID), point.Position, point.Type, point.Connections.Select(x => new Guid(x)).ToArray());
                }

                public static implicit operator PathPoint(FilePathPoint point)
                {
                    return new PathPoint(point.ID.ToString(), point.ID, point.Position, point.Type, point.Connections);
                }
            }
        }

        [Serializable]
        internal class EditableGridSettings
        {
            [FormerlySerializedAs("gridHeight")] public uint GridHeight;

            [FormerlySerializedAs("gridWidth")] public uint GridWidth;

            [FormerlySerializedAs("entryPoints")] public Vector2Int[] EntryPoints;

            [FormerlySerializedAs("endPoints")] public Vector2Int[] EndPoints;

            /// <summary>
            /// Grid weights and layout. 255 is not traversable
            /// </summary>
            [FormerlySerializedAs("nodes")] public EditableLayoutNode[] Nodes;

            public EditableGridSettings(uint gridHeight, uint gridWidth, GridSettings.Cell[] nodes, Vector2Int[] entryPoints, Vector2Int[] endPoints)
            {
                GridHeight = gridHeight;
                GridWidth = gridWidth;
                Nodes = ((EditableGridLayout)nodes).Nodes;
                EntryPoints = entryPoints;
                EndPoints = endPoints;
            }

            [Serializable]
            internal class EditableGridLayout
            {
                [FormerlySerializedAs("nodes")] public EditableLayoutNode[] Nodes;

                public int Length => Nodes.Length;

                public EditableLayoutNode this[int index] => Nodes[index];

                public EditableGridLayout(EditableLayoutNode[] gridLayout)
                {
                    Nodes = gridLayout;
                }

                public static implicit operator GridSettings.Cell[](EditableGridLayout v)
                {
                    var nodes = new GridSettings.Cell[v.Nodes.Length];
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        nodes[i] = v.Nodes[i];
                    }

                    return nodes;
                }

                public static implicit operator EditableGridLayout(GridSettings.Cell[] v)
                {
                    var nodes = new EditableLayoutNode[v.Length];
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        nodes[i] = v[i];
                    }

                    return new EditableGridLayout(nodes);
                }
            }

            [Serializable]
            internal class EditableLayoutNode
            {
                [FormerlySerializedAs("weight")] public byte Weight;

                [FormerlySerializedAs("supportsTower")]
                public bool SupportsTower;

                public EditableLayoutNode(byte weight, bool supportsTower)
                {
                    Weight = weight;
                    SupportsTower = supportsTower;
                }

                public static implicit operator GridSettings.Cell(EditableLayoutNode v)
                {
                    return new GridSettings.Cell(v.Weight, v.SupportsTower);
                }

                public static implicit operator EditableLayoutNode(GridSettings.Cell v)
                {
                    return new EditableLayoutNode(v.Weight, v.SupportsTower);
                }
            }

            public static implicit operator GridSettings(EditableGridSettings v)
            {
                var nodes = new GridSettings.Cell[v.Nodes.Length];
                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i] = v.Nodes[i];
                }

                return new GridSettings(v.GridHeight, v.GridWidth, nodes, v.EntryPoints, v.EndPoints);
            }

            public static implicit operator EditableGridSettings(GridSettings v)
            {
                return new EditableGridSettings(v.GridHeight, v.GridWidth, v.Cells, v.EntryPoints, v.EndPoints);
            }
        }
    }
}