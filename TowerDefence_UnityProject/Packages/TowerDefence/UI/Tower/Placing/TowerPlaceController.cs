using DataBinding;
using System.Collections.Generic;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.Utility;
using TowerDefence.World.Grid;

namespace TowerDefence.UI.Tower.Placing
{
    internal class TowerPlaceController
    {
        private readonly BindingContext bindingContext = new(true);

        public TowerPlaceController(ISelectionModel selectionModel)
        {
            bindingContext.Bind(selectionModel, x => x.Selection, OnSelectionChanged);
        }

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            if (selection.TryFindObject<SelectableCell>(out var cell))
            {

            }
        }

        ~TowerPlaceController()
        {
            bindingContext.Dispose();
        }
    }
}
