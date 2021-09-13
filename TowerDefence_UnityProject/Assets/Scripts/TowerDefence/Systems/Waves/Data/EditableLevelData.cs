using System;
using System.Linq;
using TowerDefence.World.Path.Data;
using UnityEngine;
using FileEnemyGroup = TowerDefence.Systems.Waves.Data.Wave.EnemyGroup;
using FilePathPoint = TowerDefence.World.Path.Data.PathPoint;

namespace TowerDefence.Systems.Waves.Data
{
    [CreateAssetMenu(menuName = "Configuration/Create Level", fileName = "Level 0")]
    public class EditableLevelData : ScriptableObject
    {
        [SerializeField]
        private EditableWave[] waves;

        [SerializeField]
        private EditablePathData pathdata;

        public EditablePathData Pathdata { get => pathdata; set => pathdata = value; }
        public EditableWave[] Waves { get => waves; set => waves = value; }

        [Serializable]
        public class EditableWave
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
                var enemygroups = new EnemyGroup[wave.enemyGroups.Length];
                for (int i = 0; i < wave.enemyGroups.Length; i++)
                {
                    enemygroups[i] = wave.enemyGroups[i];
                }
                return new EditableWave(enemygroups);
            }

            [Serializable]
            public class EnemyGroup
            {
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
        public class EditablePathData
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
                var pathPoints = new PathPoint[pathData.pathPoints.Length];
                for (int i = 0; i < pathData.pathPoints.Length; i++)
                {
                    pathPoints[i] = pathData.pathPoints[i];
                }
                return new EditablePathData(pathPoints);
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
    }
}