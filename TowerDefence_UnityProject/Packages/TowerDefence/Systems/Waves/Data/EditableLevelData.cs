using NoUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Enemies;
using TowerDefence.World.Grid;
using TowerDefence.World.Path.Data;
using UnityEngine;
using FileEnemyGroup = TowerDefence.Systems.Waves.Data.Wave.EnemyGroup;
using FilePathPoint = TowerDefence.World.Path.Data.PathPoint;

namespace TowerDefence.Systems.Waves.Data
{
    [CreateAssetMenu(menuName = "Configuration/Create Level", fileName = "Level 0")]
    public class EditableLevelData : ScriptableObject
    {
        [SerializeField] private EditableWave[] waves;
        [SerializeField] private EditablePathData pathdata;
        [SerializeField] private EditableGridSettings gridSettings;

        internal EditablePathData Pathdata { get => pathdata; set => pathdata = value; }
        internal EditableWave[] Waves { get => waves; set => waves = value; }

        public LevelData ToLevelData()
        {
            var waves = new Wave[Waves?.Length ?? 0];

            return new LevelData(waves, Pathdata);
        }

        internal GridSettings ToGridSettings()
        {
            return gridSettings;
        }

        public void FromLevelData(LevelData levelData)
        {
            var waveCount = levelData.waves?.Length ?? 0;
            var waves = new EditableWave[waveCount];

            for (int i = 0; i < waveCount; i++)
            {
                waves[i] = levelData.waves[i];
            }
            Waves = waves;
            Pathdata = levelData.path;
        }

        internal void FromGridSettings(GridSettings gridSettings)
        {
            this.gridSettings = gridSettings;
        }

        [Serializable]
        internal class EditableWave
        {
            public EnemyGroup[] enemyGroups = new EnemyGroup[0];

            public EditableWave()
            {
            }

            public EditableWave(EnemyGroup[] enemyGroups)
            {
                this.enemyGroups = enemyGroups;
            }

            public static implicit operator Wave(EditableWave wave)
            {
                var enemygroups = new FileEnemyGroup[wave.enemyGroups.Length];
                for (int i = 0; i < wave.enemyGroups.Length; i++)
                {
                    enemygroups[i] = wave.enemyGroups[i];
                }
                return new Wave(enemygroups);
            }

            public static implicit operator EditableWave(Wave wave)
            {
                var enemygroups = new EnemyGroup[wave.enemyGroups?.Length ?? 0];
                if (enemygroups.Length > 0)
                {
                    for (int i = 0; i < wave.enemyGroups.Length; i++)
                    {
                        enemygroups[i] = wave.enemyGroups[i];
                    }
                }

                return new EditableWave(enemygroups);
            }

            [Serializable]
            public class EnemyGroup
            {
                [StringDropdown("enemies.id", typeof(EnemyConfigurationData))]
                public string enemyID = string.Empty;

                public int pathID = 0;
                public float[] spawnTime = new float[0];

                public EnemyGroup()
                {
                }

                public EnemyGroup(string enemyID, int pathID, float[] spawnTime)
                {
                    this.enemyID = enemyID;
                    this.pathID = pathID;
                    this.spawnTime = spawnTime;
                }

                public static implicit operator FileEnemyGroup(EnemyGroup group)
                {
                    return new FileEnemyGroup(group.enemyID, group.pathID, group.spawnTime);
                }

                public static implicit operator EnemyGroup(FileEnemyGroup group)
                {
                    return new EnemyGroup(group.enemyID, group.pathID, group.spawnTime);
                }
            }
        }

        [Serializable]
        internal class EditablePathData
        {
            public PathPoint[] pathPoints = new PathPoint[0];

            public EditablePathData()
            {
            }

            public EditablePathData(PathPoint[] pathPoints)
            {
                this.pathPoints = pathPoints;
            }

            public static implicit operator EditablePathData(PathData pathData)
            {
                var pathPoints = new List<PathPoint>();
                var groupedPoints = pathData.pathPoints.GroupBy(x => x.type);
                foreach (var group in groupedPoints)
                {
                    var arr = group.ToArray();
                    for (int i = 0; i < group.Count(); i++)
                    {
                        var point = arr[i];
                        pathPoints.Add(new PathPoint($"{i + 1:00} - {group.Key}", point.id, point.position, point.type, point.connections));
                    }
                }

                return new EditablePathData(pathPoints.ToArray());
            }

            public static implicit operator PathData(EditablePathData pathData)
            {
                var pathPoints = new FilePathPoint[pathData.pathPoints.Length];
                for (int i = 0; i < pathData.pathPoints.Length; i++)
                {
                    pathPoints[i] = pathData.pathPoints[i];
                }
                return new PathData(pathPoints);
            }

            [Serializable]
            public class PathPoint
            {
                public string name;
                public string id;
                public Vector3 position;
                public PointType type;
                public string[] connections;

                public PathPoint()
                {
                }

                public PathPoint(string name, Guid id, Vector3 position, PointType type, Guid[] connections)
                {
                    this.name = name;
                    this.id = id.ToString();
                    this.position = position;
                    this.type = type;
                    this.connections = connections.Select(x => x.ToString()).ToArray();
                }

                public static implicit operator FilePathPoint(PathPoint point)
                {
                    return new FilePathPoint(new Guid(point.id), point.position, point.type, point.connections.Select(x => new Guid(x)).ToArray());
                }

                public static implicit operator PathPoint(FilePathPoint point)
                {
                    return new PathPoint(point.id.ToString(), point.id, point.position, point.type, point.connections);
                }
            }
        }

        [Serializable]
        internal class EditableGridSettings
        {
            public int GridHeight;

            public int GridWidth;

            /// <summary>
            /// Grid weights and layout. 255 is not traversable
            /// </summary>
            public EditableGridLayout gridLayout;

            public EditableGridSettings(int gridHeight, int gridWidth, GridSettings.Node[] nodes)
            {
                GridHeight = gridHeight;
                GridWidth = gridWidth;
                this.gridLayout = nodes;
            }

            [System.Serializable]
            internal class EditableGridLayout
            {
                public EditableLayoutNode[] nodes;

                public int Length => nodes.Length;

                public EditableLayoutNode this[int index]
                {
                    get => nodes[index];
                }

                public EditableGridLayout(EditableLayoutNode[] gridLayout)
                {
                    this.nodes = gridLayout;
                }

                public static implicit operator GridSettings.Node[](EditableGridLayout v)
                {
                    var nodes = new GridSettings.Node[v.nodes.Length];
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        nodes[i] = v.nodes[i];
                    }
                    return nodes;
                }

                public static implicit operator EditableGridLayout(GridSettings.Node[] v)
                {
                    var nodes = new EditableLayoutNode[v.Length];
                    for (int i = 0; i < nodes.Length; i++)
                    {
                        nodes[i] = v[i];
                    }
                    return new EditableGridLayout(nodes);
                }
            }

            [System.Serializable]
            internal class EditableLayoutNode
            {
                public byte weight;

                public EditableLayoutNode(byte weight)
                {
                    this.weight = weight;
                }

                public static implicit operator GridSettings.Node(EditableLayoutNode v)
                {
                    return new GridSettings.Node(v.weight);
                }

                public static implicit operator EditableLayoutNode(GridSettings.Node v)
                {
                    return new EditableLayoutNode(v.weight);
                }
            }

            public static implicit operator GridSettings(EditableGridSettings v)
            {
                var nodes = new GridSettings.Node[v.gridLayout.Length];
                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i] = v.gridLayout.nodes[i];
                }
                return new GridSettings(v.GridHeight, v.GridWidth, nodes);
            }

            public static implicit operator EditableGridSettings(GridSettings v)
            {
                return new EditableGridSettings(v.GridHeight, v.GridWidth, v.Nodes);
            }
        }
    }
}