﻿using UnityEngine;
using System.Collections;
using System;
using TowerDefence.World.Path.Data;
using Newtonsoft.Json;
using System.IO;
using TowerDefence.World.Path;
using Zenject;

namespace Examples.Paths
{
    public class ExamplePathGenerator : MonoBehaviour
    {
        private readonly string filePath = Application.streamingAssetsPath + "ExampleLevel.json";

        [Inject]
        private PathBuilderService pathBuilderService;

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
                    new PathPoint(pointId: pathIds[0], new Vector3(0,0,0),  PointType.Entrance, new Guid[]{pathIds[1], pathIds[2]}),
                    new PathPoint(pointId: pathIds[1], new Vector3(10,0,0), PointType.Point,    new Guid[]{pathIds[4]}),
                    new PathPoint(pointId: pathIds[2], new Vector3(10,5,0), PointType.Point,    new Guid[]{pathIds[3]}),
                    new PathPoint(pointId: pathIds[3], new Vector3(10,5,0), PointType.Point,    new Guid[]{pathIds[5], pathIds[4]}),
                    new PathPoint(pointId: pathIds[4], new Vector3(10,5,0), PointType.Point,    new Guid[]{pathIds[6]}),
                    new PathPoint(pointId: pathIds[5], new Vector3(10,5,0), PointType.Point,    new Guid[]{pathIds[7]}),
                    new PathPoint(pointId: pathIds[6], new Vector3(10,5,0), PointType.Point,    new Guid[]{pathIds[7]}),
                    new PathPoint(pointId: pathIds[7], new Vector3(0,5,0),  PointType.Exit,     new Guid[0])
                }
            );

            string json = JsonConvert.SerializeObject(pathData);

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

            var pathData = JsonConvert.DeserializeObject<PathData>(json);

            var constructedPath = new PathWorldData(pathBuilderService.GenerateLineData(pathData), pathBuilderService.GenerateWorldPath(pathData));
        }
    }
}