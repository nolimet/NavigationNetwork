using DataBinding;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class GridCellSelector
    {
        private readonly ISelectionModel selectionModel;
        private readonly BindingContext context = new(true);

        public GridCellSelector(ISelectionModel selectionModel)
        {
            this.selectionModel = selectionModel;

            context.Bind(selectionModel, model => model.Selection, OnSelectionChanged);
        }
        ~GridCellSelector()
        {
            context.Dispose();
        }

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            if (selection.Any(x => x is SelectableCell))
            {
                var selectedCell = selection.First(x => x is SelectableCell) as SelectableCell;

                Debug.Log(selectedCell.GridCell.Position);
            }
        }
    }
}
