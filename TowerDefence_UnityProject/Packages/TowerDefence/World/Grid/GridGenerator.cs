using System;
using System.Collections.Generic;
using System.Linq;

namespace TowerDefence.World.Grid
{
    internal class GridGenerator
    {
        public IEnumerable<IGridNode> CreateNodes(GridSettings settings)
        {
            Validate();
            var nodes = new GridNode[settings.GridHeight][];
            CreateNodes();
            LinkNodes();

            return nodes.SelectMany(x => x).Cast<IGridNode>().ToArray();

            void LinkNodes()
            {
                List<IGridNode> neightbours = new();
                for (int y = 0; y < settings.GridHeight; y++)
                {
                    for (int x = 0; x < settings.GridWidth; x++)
                    {
                        neightbours.Add(GetNode(x, y - 1));
                        neightbours.Add(GetNode(x, y + 1));
                        neightbours.Add(GetNode(x - 1, y));
                        neightbours.Add(GetNode(x + 1, y));

                        nodes[x][y].SetConnectedNodes(neightbours.Where(x => x != null).ToArray());
                        neightbours.Clear();
                    }
                }

                IGridNode GetNode(int x, int y)
                {
                    if (y > 0)
                        return nodes[y][x];

                    if (x > 0)
                        return nodes[y][x];

                    if (x < settings.GridWidth - 1)
                        return nodes[y][x];

                    if (y < settings.GridHeight - 1)
                        return nodes[y][x];
                    return null;
                }
            }
            void CreateNodes()
            {
                int counter = 0;
                for (int y = 0; y < settings.GridHeight; y++)
                {
                    var row = nodes[y] = new GridNode[settings.GridWidth];
                    for (int x = 0; x < settings.GridWidth; x++)
                    {
                        row[x] = new GridNode(settings.gridLayout[counter].weight, new(x, y));
                        counter++;
                    }
                }
            }

            void Validate()
            {
                int gridCount = settings.GridWidth * settings.GridHeight;
                if (gridCount != settings.gridLayout.Length) throw new Exception("layout and size do not match!");
            }
        }
    }
}