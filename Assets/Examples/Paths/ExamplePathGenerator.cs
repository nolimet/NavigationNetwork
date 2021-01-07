using UnityEngine;
using System.Collections;
using System;
using TowerDefence.World.Path.Data;
using Newtonsoft.Json;
using System.IO;

namespace Examples.Paths
{
    public class ExamplePathGenerator : MonoBehaviour
    {
        [ContextMenu("Generate Example Path")]
        public void GenerateExamplePath()
        {
            Guid[] pathIds = new Guid[]
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            PathData pathData = new PathData
            (
                new PathPoint[]
                {
                    new PathPoint(pointId: pathIds[0], new Vector3(0,0,0), PointType.Entrance, new Guid[]{pathIds[1]}),
                    new PathPoint(pointId: pathIds[1], new Vector3(10,0,0), PointType.Point, new Guid[]{pathIds[2]}),
                    new PathPoint(pointId: pathIds[2], new Vector3(10,5,0), PointType.Point, new Guid[]{pathIds[3]}),
                    new PathPoint(pointId: pathIds[3], new Vector3(0,5,0), PointType.Exit, new Guid[0])
                }
            );

            string json = JsonConvert.SerializeObject(pathData);

            string path = Application.streamingAssetsPath + "ExampleLevel.json";

            File.WriteAllText(path, json);
        }
    }
}