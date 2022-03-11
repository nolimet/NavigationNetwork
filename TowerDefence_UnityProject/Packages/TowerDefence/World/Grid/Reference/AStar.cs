using System.Collections.Generic;

namespace Pathing
{
    // algorithm based on WikiPedia: http://en.wikipedia.org/wiki/A*_search_algorithm
    //
    // implements the static GetPath(...) function that will return a IList of IAStarNodes that is the shortest path
    // between the two IAStarNodes that are passed as parameters to the function
    public static class AStar
    {
        private class OpenSorter : IComparer<IAStarNode>
        {
            private Dictionary<IAStarNode, float> fScore;

            public OpenSorter(Dictionary<IAStarNode, float> f)
            {
                fScore = f;
            }

            public int Compare(IAStarNode a, IAStarNode b)
            {
                if (a != null && b != null)
                    return fScore[a].CompareTo(fScore[b]);
                else
                    return 0;
            }
        }

        private readonly static List<IAStarNode> closed = new();
        private readonly static List<IAStarNode> open = new();
        private readonly static Dictionary<IAStarNode, IAStarNode> vistedNodes = new();
        private readonly static Dictionary<IAStarNode, float> gScore = new();
        private readonly static Dictionary<IAStarNode, float> hScore = new();
        private readonly static Dictionary<IAStarNode, float> fScore = new();

        // this function is the C# implementation of the algorithm presented on the wikipedia page
        // start and goal are the nodes in the graph we should find a path for
        //
        // returns null if no path is found
        //
        // this function is NOT thread-safe (due to using static data for GC optimization)
        public static IList<IAStarNode> GetPath(IAStarNode start, IAStarNode goal)
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
            hScore.Add(start, start.EstimatedCostTo(goal));
            fScore.Add(start, hScore[start]);

            OpenSorter sorter = new OpenSorter(fScore);
            IAStarNode current, previousNode = null;

            float tentativeGScore;
            bool tentativeIsBetter;

            while (open.Count > 0)
            {
                current = open[0];
                if (current == goal)
                {
                    return ReconstructPath(new List<IAStarNode>(), vistedNodes, goal);
                }

                open.Remove(current);
                closed.Add(current);

                if (current != start)
                {
                    previousNode = vistedNodes[current];
                }
                foreach (IAStarNode nextNode in current.Neighbours)
                {
                    if (previousNode != nextNode && !closed.Contains(nextNode))
                    {
                        tentativeGScore = gScore[current] + current.CostTo(nextNode);
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
                            hScore[nextNode] = nextNode.EstimatedCostTo(goal);
                            fScore[nextNode] = gScore[nextNode] + hScore[nextNode];
                        }
                    }
                }
                open.Sort(sorter);
            }
            return null;
        }

        private static IList<IAStarNode> ReconstructPath(IList<IAStarNode> path, Dictionary<IAStarNode, IAStarNode> visitedNodes, IAStarNode currentNode)
        {
            if (visitedNodes.ContainsKey(currentNode))
            {
                ReconstructPath(path, visitedNodes, visitedNodes[currentNode]);
            }
            path.Add(currentNode);
            return path;
        }
    }
}