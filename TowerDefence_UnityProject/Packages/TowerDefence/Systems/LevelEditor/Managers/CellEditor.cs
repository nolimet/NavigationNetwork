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
        private readonly ISelectionModel _selectionModel;
        private readonly IWorldLayoutModel _worldLayoutModel;
        private readonly BindingContext _bindingContext = new();

        public CellEditor(ISelectionModel selectionModel, IWorldLayoutModel worldLayoutModel)
        {
            _selectionModel = selectionModel;
            _worldLayoutModel = worldLayoutModel;
            _bindingContext.Bind(_selectionModel, x => x.Selection, OnSelectionChanged);
        }

        private void OnSelectionChanged(IList<ISelectable> obj)
        {
            Debug.Log(obj.Count);
        }

        public void Dispose()
        {
            _bindingContext?.Dispose();
        }

        public void ChangeCellsForSelection(byte weight, bool supportTower)
        {
            foreach (var selectable in _selectionModel.Selection)
            {
                switch (selectable)
                {
                    case SelectableCell cell:
                    {
                        var cellPos = cell.GridCell.Position;
                        var cellIndex = (int)(cellPos.x * _worldLayoutModel.Width + cellPos.y);
                        var cellModel = _worldLayoutModel.Cells[cellIndex];
                        cellModel.Weight = weight;
                        cellModel.SupportsTower = supportTower;
                        break;
                    }
                }
            }
        }
    }
}