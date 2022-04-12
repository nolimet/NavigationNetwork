using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.World.Grid.Data;

namespace TowerDefence.World.Grid
{
    internal class GridGenerator
    {
        public IEnumerable<IGridCell> CreateNodes(GridSettings settings)
        {
            Validate();
            var returnValue = new List<IGridCell>();
            var cells = new GridCell[settings.GridHeight, settings.GridWidth];
            CreateCells();
            LinkCells();

            return returnValue;

            void LinkCells()
            {
                List<IGridCell> neightbours = new();
                for (int y = 0; y < settings.GridHeight; y++)
                {
                    for (int x = 0; x < settings.GridWidth; x++)
                    {
                        neightbours.Add(GetNode(x, y - 1));
                        neightbours.Add(GetNode(x, y + 1));
                        neightbours.Add(GetNode(x - 1, y));
                        neightbours.Add(GetNode(x + 1, y));

                        cells[x, y].SetConnectedCells(neightbours.Where(x => x != null).ToArray());
                        neightbours.Clear();
                    }
                }

                IGridCell GetNode(int x, int y)
                {
                    if (x >= 0 && y >= 0 && x < settings.GridWidth && y < settings.GridHeight)
                        return cells[y, x];
                    return default;
                }
            }

            void CreateCells()
            {
                int counter = 0;
                for (int y = 0; y < settings.GridHeight; y++)
                {
                    for (int x = 0; x < settings.GridWidth; x++)
                    {
                        cells[y, x] = new GridCell(settings.Cells[counter].weight, new(x, y));
                        returnValue.Add(cells[y, x]);
                        counter++;
                    }
                }
            }

            void Validate()
            {
                int gridCount = settings.GridWidth * settings.GridHeight;
                if (gridCount != settings.Cells.Length) throw new Exception($"layout and size do not match! - {gridCount} vs {settings.Cells.Length}");
            }
        }
    }
}