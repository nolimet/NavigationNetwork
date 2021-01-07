using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.World.Path.Data;
using UnityEngine;

namespace TowerDefence.World.Path
{
    public class PathBuilderService
    {
        public void GenerateWorldPath(PathData pathData)
        {
            throw new NotImplementedException("Generating world path");
        }

        public bool ValidatePath(PathData pathData)
        {
            //TODO add no exit and entrance exception throwing
            var entrances = pathData.pathPoints.Where(x => x.pointType == PointType.Entrance);
            var pathPoints = pathData.pathPoints.ToDictionary(point => point.pointId);
            List<Guid> visitedPoints = new List<Guid>();
            List<bool> walkResults = new List<bool>();

            foreach (var entrance in entrances)
            {
                walkResults.Add(WalkPath(entrance));
            }

            if (walkResults.Count == 0)
            {
            }
            return walkResults.All(x => x) && walkResults.Count > 0;
            bool WalkPath(PathPoint current)
            {
                if (current.pointType == PointType.Exit)
                {
                    return true;
                }

                if (visitedPoints.Contains(current.pointId))
                {
                    List<(bool hasExit, PathPoint point)> connections = new List<(bool hasExit, PathPoint point)>();
                    visitedPoints.Add(current.pointId);

                    foreach (var pointId in current.pointConnections)
                    {
                        if (pathPoints.TryGetValue(pointId, out var point))
                        {
                            connections.Add((WalkPath(point), point));
                        }
                    }

                    visitedPoints.Remove(current.pointId);
                    return connections.All(x => x.hasExit) && connections.Count > 0;
                }

                throw new InfitePathException($"Path has no end! {current}");
            }
        }
    }
}