using Newtonsoft.Json;
using System;
using System.IO;
using TowerDefence.Entities.Enemies;
using TowerDefence.Systems.Waves.Data;
using TowerDefence.World;
using TowerDefence.World.Path.Data;
using UnityEngine;
using Zenject;
using static TowerDefence.Systems.Waves.Data.Wave;

namespace TowerDefence.Examples.Paths
{
    public class ExamplePathGenerator : MonoBehaviour
    {
        private readonly string filePath = Application.streamingAssetsPath + "/ExampleLevel.json";

        [HideInInspector]
        public PathWorldData ConstructedPath => worldController?.pathWorldData;

        [Inject] private WorldController worldController = null;
        [Inject] private EnemyController enemyController = null;
        [Inject] private EnemyConfigurationData enemyConfiguration = null;

        [ContextMenu("Generate Example Path")]
        public void GenerateExamplePath()
        {
            Guid[] pathIds = new Guid[]
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };

            PathData pathData = new PathData
            (
                new PathPoint[]
                {
                    new PathPoint(id: pathIds[0], new Vector3(0,0,0),  PointType.Entrance, new Guid[]{pathIds[1], pathIds[2]}),
                    new PathPoint(id: pathIds[1], new Vector3(5,5,0), PointType.Point,    new Guid[]{pathIds[4]}),
                    new PathPoint(id: pathIds[2], new Vector3(5,-5,0), PointType.Point,    new Guid[]{pathIds[3]}),
                    new PathPoint(id: pathIds[3], new Vector3(10,-5,0), PointType.Point,    new Guid[]{pathIds[5], pathIds[4]}),
                    new PathPoint(id: pathIds[4], new Vector3(10,5,0), PointType.Point,    new Guid[]{pathIds[6]}),
                    new PathPoint(id: pathIds[5], new Vector3(15,-5,0), PointType.Point,    new Guid[]{pathIds[7]}),
                    new PathPoint(id: pathIds[6], new Vector3(15,5,0), PointType.Point,    new Guid[]{pathIds[7]}),
                    new PathPoint(id: pathIds[7], new Vector3(20,0,0),  PointType.Exit,     new Guid[0])
                }
            );

            Wave[] waves = new Wave[]
            {
                new Wave(new[]
                {
                    new EnemyGroup("Walker", 0, new[]{0f,0.2f,0.3f,0.5f })
                })
            };

            var levelData = new LevelData(waves, pathData);

            string json = JsonConvert.SerializeObject(levelData, Formatting.Indented);

            string dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(filePath, json);
        }

        [ContextMenu("Build Example")]
        public void BuildExample()
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("Application should be playing before doing this");
                return;
            }
            string json = File.ReadAllText(filePath);

            var levelData = JsonConvert.DeserializeObject<LevelData>(json);

            worldController.SetPath(levelData.path);
        }

        public async void CreateWalker()
        {
            var path = worldController.pathWorldData.GetRandomPath();
            await enemyController.CreateNewEnemy("Walker");
        }

        public async void CreateWalkerDelayed(float delay)
        {
            await new WaitForSeconds(delay);
            var path = worldController.pathWorldData.GetRandomPath();
            await enemyController.CreateNewEnemy("Walker");
        }
    }
}