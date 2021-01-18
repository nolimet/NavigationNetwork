using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.World.Path.Data;
using TowerDefence.World.Path.Exceptions;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace TowerDefence.World.Path
{
    public class PathBuilderService
    {
        private readonly PathLineRenderer.Factory lineFactory;

        public PathBuilderService(PathLineRenderer.Factory lineFactory)
        {
            this.lineFactory = lineFactory;
        }

        public Vector3[][] GeneratePaths(PathData pathData)
        {
            var entrances = pathData.pathPoints.Where(x => x.pointType == PointType.Entrance).ToArray();
            var pathLookup = pathData.pathPoints.ToDictionary(x => x.pointId);
            List<Guid> visitedPoints = new List<Guid>();
            List<Guid[]> tracedPaths = new List<Guid[]>();

            if (!entrances.Any())
            {
                throw new NoPathEntranceException();
            }

            //walking all routs
            foreach (var entrance in entrances)
            {
                tracedPaths.AddRange(CrawlPath(entrance));
            }

            //filtering paths and converting them to vector3 arrays

            return tracedPaths
                .Where(x => pathLookup[x.Last()].pointType == PointType.Exit)
                .Select
                (x =>
                    x.Select
                    (y =>
                        pathLookup[y].position
                    ).ToArray()
                ).ToArray();

            IEnumerable<Guid[]> CrawlPath(PathPoint point)
            {
                //it return the full path once it hits a end
                if (point.pointType == PointType.Exit)
                {
                    return new List<Guid[]>() { visitedPoints.Concat(new[] { point.pointId }).ToArray() };
                }
                if (!point.pointConnections.Any())
                {
                    throw new InvalidPathException("Point is not exit! " + point.ToString());
                }
                if (visitedPoints.Contains(point.pointId))
                {
                    throw new InfitePathException("Path loops around forever " + point.ToString());
                }

                List<Guid[]> points = new List<Guid[]>();
                visitedPoints.Add(point.pointId);
                foreach (var pointId in point.pointConnections)
                {
                    points.AddRange(CrawlPath(pathLookup[pointId]));
                }
                visitedPoints.Remove(point.pointId);

                return points.Distinct();
            }
        }

        public IEnumerable<PathLineRenderer> GenerateLineRenderers(PathData pathData)
        {
            List<PathLineRenderer> lines = new List<PathLineRenderer>();
            var entrances = pathData.pathPoints.Where(x => x.pointType == PointType.Entrance).ToArray();
            var pathLookup = pathData.pathPoints.ToDictionary(x => x.pointId);
            List<PathPoint> visitedPoints = new List<PathPoint>();
            List<IEnumerable<PathPoint>> virtualLines = new List<IEnumerable<PathPoint>>();
            try
            {
                foreach (var entrance in entrances)
                {
                    virtualLines.Add(CrawlPath(entrance));
                }

                foreach (var virtualLine in virtualLines)
                {
                    var newLine = lineFactory.Create(virtualLine.Select(x => x.position).ToArray());
                    lines.Add(newLine);
                }
            }
            catch
            {
                foreach (var line in lines)
                {
                    UObject.DestroyImmediate(line.gameObject);
                }
                throw;
            }
            return lines;

            IEnumerable<PathPoint> CrawlPath(PathPoint startPoint)
            {
                PathPoint currentPoint = startPoint;
                List<PathPoint> line = new List<PathPoint>();
                do
                {
                    if (visitedPoints.Contains(currentPoint))
                    {
                        throw new InfitePathException();
                    }

                    //add point to visited points to avoid endless loops
                    visitedPoints.Add(currentPoint);
                    //add current point to line
                    line.Add(currentPoint);

                    //Checking if there is any point then do
                    if (currentPoint.pointConnections.Any())
                    {
                        //if there is more than one connection we will build that
                        if (currentPoint.pointConnections.Length > 1)
                        {
                            //we skip first point we deal with that one below
                            for (int i = 1; i < currentPoint.pointConnections.Length; i++)
                            {
                                virtualLines.Add(new[] { currentPoint }.Concat(CrawlPath(pathLookup[currentPoint.pointConnections[i]])).ToArray());
                            }
                        }
                        //set first point as current
                        currentPoint = pathLookup[currentPoint.pointConnections.First()];
                    }
                }
                while (currentPoint != null && currentPoint.pointType != PointType.Exit);

                //add the last point as the loop did not get a chance to add it
                if (currentPoint != null && !line.Contains(currentPoint))
                {
                    line.Add(currentPoint);
                }

                //remove the line from visited points as we ended a line
                foreach (var point in line)
                {
                    visitedPoints.Remove(point);
                }

                return line.ToArray();
            }
        }

        public bool ValidatePath(PathData pathData)
        {
            if (!pathData.pathPoints.Any(x => x.pointType == PointType.Exit))
            {
                throw new NoPathExitException();
            }

            if (!pathData.pathPoints.Any(x => x.pointType == PointType.Entrance))
            {
                throw new NoPathEntranceException();
            }

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
                throw new InvalidPathException("This path is invalid in some strange way! It was not walkable");
            }
            return walkResults.All(x => x) && walkResults.Count > 0;

            bool WalkPath(PathPoint current)
            {
                if (current.pointType == PointType.Exit)
                {
                    return true;
                }

                if (!visitedPoints.Contains(current.pointId))
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
                else
                {
                    throw new InfitePathException($"Path has no end! {current}");
                }
            }
        }
    }
}