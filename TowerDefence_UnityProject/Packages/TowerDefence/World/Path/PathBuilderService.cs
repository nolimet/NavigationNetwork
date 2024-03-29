﻿using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.World.Path.Data;
using TowerDefence.World.Path.Exceptions;
using TowerDefence.World.Path.Rendering;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace TowerDefence.World.Path
{
    public sealed class PathBuilderService
    {
        private readonly PathRendererBase.Factory lineFactory;

        public PathBuilderService(PathRendererBase.Factory lineFactory)
        {
            this.lineFactory = lineFactory;
        }

        public PathWorldData GeneratePathWorldData(PathData pathData)
        {
            return new PathWorldData(GeneratePaths(pathData), GenerateLineRenderers(pathData));
        }

        public Vector3[][] GeneratePaths(PathData pathData)
        {
            var entrances = pathData.pathPoints.Where(x => x.type == PointType.Entrance).ToArray();
            var pathLookup = pathData.pathPoints.ToDictionary(x => x.id);
            var visitedPoints = new List<Guid>();
            var tracedPaths = new List<Guid[]>();

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
                .Where(x => pathLookup[x.Last()].type == PointType.Exit)
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
                if (point.type == PointType.Exit)
                {
                    return new List<Guid[]>() { visitedPoints.Concat(new[] { point.id }).ToArray() };
                }
                if (!point.connections.Any())
                {
                    throw new InvalidPathException("Point is not exit! " + point.ToString());
                }
                if (visitedPoints.Contains(point.id))
                {
                    throw new InfinitePathException("Path loops around forever " + point.ToString());
                }

                var points = new List<Guid[]>();
                visitedPoints.Add(point.id);
                foreach (var pointId in point.connections)
                {
                    points.AddRange(CrawlPath(pathLookup[pointId]));
                }
                visitedPoints.Remove(point.id);

                return points.Distinct();
            }
        }

        public IEnumerable<PathRendererBase> GenerateLineRenderers(PathData pathData)
        {
            var lines = new List<PathRendererBase>();
            var entrances = pathData.pathPoints.Where(x => x.type == PointType.Entrance).ToArray();
            var pathLookup = pathData.pathPoints.ToDictionary(x => x.id);
            var visitedPoints = new List<PathPoint>();
            var virtualLines = new List<IEnumerable<PathPoint>>();

            try
            {
                //build lines starting at all entrances
                foreach (var entrance in entrances)
                {
                    virtualLines.Add(CrawlPath(entrance));
                }

                //Generate lines
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
                var line = new List<PathPoint>();
                do
                {
                    if (visitedPoints.Contains(currentPoint))
                    {
                        throw new InfinitePathException();
                    }

                    //add point to visited points to avoid endless loops
                    visitedPoints.Add(currentPoint);
                    //add current point to line
                    line.Add(currentPoint);

                    //Checking if there is any point then do
                    if (currentPoint.connections.Any())
                    {
                        //if there is more than one connection we will build that
                        if (currentPoint.connections.Length > 1)
                        {
                            //we skip first point we deal with that one below
                            for (int i = 1; i < currentPoint.connections.Length; i++)
                            {
                                virtualLines.Add(new[] { currentPoint }.Concat(CrawlPath(pathLookup[currentPoint.connections[i]])).ToArray());
                            }
                        }
                        //set first point as current
                        currentPoint = pathLookup[currentPoint.connections.First()];
                    }
                }
                while (currentPoint != PathPoint.Empty && currentPoint.type != PointType.Exit);

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
            if (!pathData.pathPoints.Any(x => x.type == PointType.Exit))
            {
                throw new NoPathExitException();
            }

            if (!pathData.pathPoints.Any(x => x.type == PointType.Entrance))
            {
                throw new NoPathEntranceException();
            }

            var entrances = pathData.pathPoints.Where(x => x.type == PointType.Entrance);
            var pathPoints = pathData.pathPoints.ToDictionary(point => point.id);
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
                if (current.type == PointType.Exit)
                {
                    return true;
                }

                if (!visitedPoints.Contains(current.id))
                {
                    List<(bool hasExit, PathPoint point)> connections = new List<(bool hasExit, PathPoint point)>();
                    visitedPoints.Add(current.id);

                    foreach (var pointId in current.connections)
                    {
                        if (pathPoints.TryGetValue(pointId, out var point))
                        {
                            connections.Add((WalkPath(point), point));
                        }
                    }

                    visitedPoints.Remove(current.id);
                    return connections.All(x => x.hasExit) && connections.Count > 0;
                }
                else
                {
                    throw new InfinitePathException($"Path has no end! {current}");
                }
            }
        }
    }
}