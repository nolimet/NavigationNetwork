using Newtonsoft.Json;
using System;
using System.IO;
using TowerDefence.Entities.Enemies;
using TowerDefence.Systems.Waves;
using TowerDefence.Systems.Waves.Data;
using TowerDefence.Systems.WorldLoad;
using TowerDefence.Systems.WorldLoader.Data;
using TowerDefence.World;
using TowerDefence.World.Grid;
using TowerDefence.World.Path.Data;
using UnityEngine;
using Zenject;
using static TowerDefence.Systems.Waves.Data.Wave;

namespace TowerDefence.Examples.Paths
{
    public class ExamplePathGenerator : MonoBehaviour
    {
        private readonly string filePath = Application.streamingAssetsPath + "/ExampleLevel.json";
        private readonly string gridWorldPath = Application.streamingAssetsPath + "/ExampleLevel-grid.json";

        [HideInInspector]
        public PathWorldData ConstructedPath => pathWorldController?.PathWorldData;

        [Inject] private PathWorldController pathWorldController = null;
        [Inject] private EnemyController enemyController = null;
        [Inject] private EnemyConfigurationData enemyConfiguration = null;
        [Inject] private GridWorld gridWorld = null;
        [Inject] private WorldLoadController worldLoadController = null;
        [Inject] private WaveController waveController = null;

        private LevelData levelData;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.F1))
            {
                BuildGridWorld();
            }

            if (UnityEngine.Input.GetKeyUp(KeyCode.F2))
            {
                CreateGridWalker();
            }

            if (UnityEngine.Input.GetKeyUp(KeyCode.F3))
            {
                worldLoadController.LoadLevel("Level 0", WorldLoadController.LevelType.lvl);
            }

            if (UnityEngine.Input.GetKeyUp(KeyCode.F4))
            {
                waveController.StartWavePlayBack();
            }

            if (UnityEngine.Input.GetKeyUp(KeyCode.F5))
            {
                waveController.StopWavePlayBack();
            }
        }

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
                    new EnemyGroup("Walker", 0,0,0 ,new[]{0f,0.2f,0.3f,0.5f })
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

            pathWorldController.SetPath(levelData.path.Value);
        }

        public async void BuildGridWorld()
        {
            string json = File.ReadAllText(gridWorldPath);
            levelData = JsonConvert.DeserializeObject<LevelData>(json);
            if (levelData.gridSettings.HasValue)
                await gridWorld.CreateWorld(levelData.gridSettings.Value);
            else
                Debug.LogError("NO GRIDWORLD");
        }

        public async void CreateWalker()
        {
            var path = pathWorldController.PathWorldData.GetRandomPath();
            await enemyController.CreateNewEnemy("Walker", path);
        }

        public async void CreateWalkerDelayed(float delay)
        {
            await new WaitForSeconds(delay);
            var path = pathWorldController.PathWorldData.GetRandomPath();
            await enemyController.CreateNewEnemy("Walker", path);
        }

        public async void CreateGridWalker()
        {
            var gridSettings = levelData.gridSettings.Value;
            var start = gridSettings.EntryPoints[UnityEngine.Random.Range(0, gridSettings.EntryPoints.Length)];
            var end = gridSettings.EndPoints[UnityEngine.Random.Range(0, gridSettings.EndPoints.Length)];
            await enemyController.CreateNewEnemy("GridWalker", gridWorld.GetCell(start), gridWorld.GetCell(end));
        }
    }
}