using System.Collections.Generic;
using TowerDefence.World.Grid.Data;

namespace TowerDefence.World.Grid
{
    // algorithm based on WikiPedia: http://en.wikipedia.org/wiki/A*_search_algorithm
    //
    // implements the static GetPath(...) function that will return a IList of IGridNodes that is the shortest path
    // between the two IGridNodes that are passed as parameters to the function
    internal sealed class PathFinder
    {
        private class OpenSorter : IComparer<IGridCell>
        {
            private readonly Dictionary<IGridCell, float> fScore;

            public OpenSorter(Dictionary<IGridCell, float> f)
            {
                fScore = f;
            }

            public int Compare(IGridCell a, IGridCell b)
            {
                if (a != null && b != null)
                    return fScore[a].CompareTo(fScore[b]);
                else
                    return 0;
            }
        }

        public bool Working { get; set; }

        private readonly List<IGridCell> closed = new();
        private readonly List<IGridCell> open = new();
        private readonly Dictionary<IGridCell, IGridCell> vistedNodes = new();
        private readonly Dictionary<IGridCell, float> gScore = new();
        private readonly Dictionary<IGridCell, float> hScore = new();
        private readonly Dictionary<IGridCell, float> fScore = new();

        // this function is the C# implementation of the algorithm presented on the wikipedia page
        // start and goal are the nodes in the graph we should find a path for
        //
        // returns null if no path is found
        //
        // this function is NOT thread-safe (due to using static data for GC optimization)
        public IList<IGridCell> GetPath(IGridCell start, IGridCell goal)
        {
            if (start == null || goal == null)
            {
                return null;
            }

            closed.Clear();
            open.Clear();
            open.Add(start);

            vistedNodes.Clear();
            gScore.Clear();
            hScore.Clear();
            fScore.Clear();

            gScore.Add(start, 0f);
            hScore.Add(start, start.GetCost(goal));
            fScore.Add(start, hScore[start]);

            OpenSorter sorter = new OpenSorter(fScore);
            IGridCell current, previousNode = null;

            float tentativeGScore;
            bool tentativeIsBetter;

            while (open.Count > 0)
            {
                current = open[0];
                if (current == goal)
                {
                    return ReconstructPath(new List<IGridCell>(), vistedNodes, goal);
                }

                open.Remove(current);
                closed.Add(current);

                if (current != start)
                {
                    previousNode = vistedNodes[current];
                }
                foreach (IGridCell nextNode in current.ConnectedCells)
                {
                    if (previousNode != nextNode && !closed.Contains(nextNode))
                    {
                        tentativeGScore = gScore[current] + current.GetCost(nextNode);
                        tentativeIsBetter = true;

                        if (!open.Contains(nextNode))
                        {
                            open.Add(nextNode);
                        }
                        else
                        if (tentativeGScore >= gScore[nextNode])
                        {
                            tentativeIsBetter = false;
                        }

                        if (tentativeIsBetter)
                        {
                            vistedNodes[nextNode] = current;
                            gScore[nextNode] = tentativeGScore;
                            hScore[nextNode] = nextNode.GetCost(goal);
                            fScore[nextNode] = gScore[nextNode] + hScore[nextNode];
                        }
                    }
                }
                open.Sort(sorter);
            }
            return null;
        }

        private static IList<IGridCell> ReconstructPath(IList<IGridCell> path, Dictionary<IGridCell, IGridCell> visitedCells, IGridCell currentCell)
        {
            if (visitedCells.ContainsKey(currentCell))
            {
                ReconstructPath(path, visitedCells, visitedCells[currentCell]);
            }
            path.Add(currentCell);
            return path;
        }
    }
}