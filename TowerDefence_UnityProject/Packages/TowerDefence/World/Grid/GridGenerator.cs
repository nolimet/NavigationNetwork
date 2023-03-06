using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal sealed class GridGenerator
    {
        private readonly GridWorldSettings worldSettings;

        public GridGenerator(GridWorldSettings worldSettings)
        {
            this.worldSettings = worldSettings;
        }

        public IReadOnlyCollection<IGridCell> CreateNodes(GridSettings settings)
        {
            Validate();
            var returnValue = new List<IGridCell>();
            var cells = new GridCell[settings.GridWidth][];
            for (int index = 0; index < settings.GridWidth; index++)
            {
                cells[index] = new GridCell[settings.GridHeight];
            }

            CreateCells();
            LinkCells();

            return returnValue;

            void LinkCells()
            {
                List<IGridCell> neightbours = new();
                for (int x = 0; x < settings.GridWidth; x++)
                {
                    for (int y = 0; y < settings.GridHeight; y++)
                    {
                        neightbours.Add(GetNode(x, y - 1));
                        neightbours.Add(GetNode(x, y + 1));
                        neightbours.Add(GetNode(x - 1, y));
                        neightbours.Add(GetNode(x + 1, y));

                        //Generate diagonals
                        neightbours.Add(GetNode(x + 1, y + 1));
                        neightbours.Add(GetNode(x + 1, y - 1));
                        neightbours.Add(GetNode(x - 1, y + 1));
                        neightbours.Add(GetNode(x - 1, y - 1));

                        cells[x][y].SetConnectedCells(neightbours.Where(c => c != null).ToArray());
                        neightbours.Clear();
                    }
                }

                IGridCell GetNode(int x, int y)
                {
                    if (x >= 0 && y >= 0 && x < settings.GridWidth && y < settings.GridHeight)
                        return cells[x][y];
                    return default;
                }
            }

            void CreateCells()
            {
                var offset = worldSettings.TileSize * new Vector2(settings.GridWidth, settings.GridHeight) / 2f - worldSettings.TileSize / 2f;
                int counter = 0;
                for (int x = 0; x < settings.GridWidth; x++)
                {
                    for (int y = 0; y < settings.GridHeight; y++)
                    {
                        cells[x][y] = new GridCell
                        (
                            cellWeight: settings.Cells[counter].Weight,
                            position: new Vector2Int(x, y),
                            worldPosition: new Vector2(x * worldSettings.TileSize.x, y * worldSettings.TileSize.y) - offset,
                            supportsTower: settings.Cells[counter].SupportsTower
                        );
                        returnValue.Add(cells[x][y]);
                        counter++;
                    }
                }
            }

            void Validate()
            {
                if (settings.Cells == null) throw new NullReferenceException("No Cells");
                uint gridCount = settings.GridWidth * settings.GridHeight;
                if (gridCount != settings.Cells.Length) throw new Exception($"layout and size do not match! - {gridCount} vs {settings.Cells?.Length}");
            }
        }
    }
}