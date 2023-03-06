using System;
using System.Collections.Generic;
using DataBinding;
using TowerDefence.Systems.LevelEditor.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.World.Grid;
using UnityEngine;

namespace TowerDefence.Systems.LevelEditor.Managers
{
    public class CellEditor : IDisposable
    {
        private readonly ISelectionModel selectionModel;
        private readonly ILevelEditorModel levelEditorModel;
        private IWorldLayoutModel worldLayoutModel;

        private readonly BindingContext bindingContext = new();

        public CellEditor(ISelectionModel selectionModel, ILevelEditorModel levelEditorModel)
        {
            this.selectionModel = selectionModel;
            this.levelEditorModel = levelEditorModel;

            bindingContext.Bind(this.selectionModel, x => x.Selection, OnSelectionChanged);
            bindingContext.Bind(this.levelEditorModel, x => x.World, OnWorldModelChanged);
        }

        private void OnWorldModelChanged(IWorldLayoutModel world)
        {
            worldLayoutModel = world;
        }

        private void OnSelectionChanged(IList<ISelectable> obj)
        {
            Debug.Log(obj.Count);
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
        }

        public void ChangeCellsForSelection(byte weight, bool supportTower)
        {
            foreach (var selectable in selectionModel.Selection)
            {
                switch (selectable)
                {
                    case SelectableCell cell:
                    {
                        var cellPos = cell.GridCell.Position;
                        var cellIndex = (int)(cellPos.x * worldLayoutModel.Width + cellPos.y);
                        var cellModel = worldLayoutModel.Cells[cellIndex];
                        cellModel.Weight = weight;
                        cellModel.SupportsTower = supportTower;
                        break;
                    }
                }
            }
        }
    }
}