using System;
using System.Collections.Generic;
using System.Linq;
using DataBinding;
using TowerDefence.Systems.LevelEditor.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using UnityEngine;

namespace TowerDefence.Systems.LevelEditor.Managers
{
    public class CellEditor : IDisposable
    {
        private static readonly int HeightMult = Shader.PropertyToID("_HeightMult");
        private static readonly int SupportsTower = Shader.PropertyToID("_SupportsTower");

        private readonly ISelectionModel selectionModel;
        private readonly ILevelEditorModel levelEditorModel;
        private IWorldLayoutModel worldLayoutModel;

        private readonly BindingContext bindingContext = new();
        private readonly List<ICellModel> selectedCells = new();

        internal CellEditor(ISelectionModel selectionModel, ILevelEditorModel levelEditorModel)
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

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            selectedCells.Clear();
            if (selection is null || worldLayoutModel is null) return;

            var cells = worldLayoutModel.Cells.ToArray().AsSpan();
            var spanSelection = selection.ToArray().AsSpan();
            //TODO fix this selection code
            /*for (int i = 0; i < cells.Length; i++)
            {
                if (spanSelection.Length == 0)
                    break;

                var cell = cells[i];
                for (int j = 0; j < spanSelection.Length; j++)
                {
                    if (!ReferenceEquals(spanSelection[j], cell.WorldCellGroup)) continue;

                    selectedCells.Add(cell);
                    spanSelection.Slice(j, 1);
                    break;
                }
            }*/
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
        }

        internal void ChangeCellsForSelection(byte weight, bool supportTower)
        {
            foreach (var cell in selectedCells)
            {
                cell.Weight = weight;
                cell.SupportsTower = supportTower;

                var renderer = cell.WorldCellGroup.GetComponent<Renderer>();
                renderer.material.SetFloat(HeightMult, 0.003921569F * weight);
                renderer.material.SetInt(SupportsTower, supportTower ? 1 : 0);
            }
        }
    }
}