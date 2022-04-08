using DataBinding;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using UnityEngine;

namespace TowerDefence.World.Grid
{
    internal class GridNodeSelector
    {
        private readonly ISelectionModel selectionModel;
        private readonly BindingContext context = new(true);

        public GridNodeSelector(ISelectionModel selectionModel)
        {
            this.selectionModel = selectionModel;

            context.Bind(selectionModel, model => model.Selection, OnSelectionChanged);
        }

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            if (selection.Any(x => x is SelectableNode))
            {
                var selectedNode = selection.First(x => x is SelectableNode) as SelectableNode;

                Debug.Log(selectedNode.GridNode.Position);
            }
        }
    }
}
