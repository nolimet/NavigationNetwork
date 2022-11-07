using System.Collections.Generic;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Models;
using TowerDefence.World.Grid;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.Hud.Drawers
{
    public class TowerPlaceController
    {
        private const string containerId = "game_ui";
        private const string towerPlaceContainerId = "places_tower_container";

        private readonly BindingContext bindingContext = new();
        private IUIContainer activeContainer;
        private VisualElement towerPlaceContainer;

        public TowerPlaceController(IUIContainers uiContainers, ISelectionModel selectionModel)
        {
            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainersChanged);
            bindingContext.Bind(selectionModel, x => x.Selection, OnSelectionChanged;
        }

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            if (selection.Count == 1 && selection.TryFind(x => x is SelectableCell, out var s) && s is SelectableCell cell && !cell.GridCell.HasStructure)
            {
                towerPlaceContainer.visible = true;
                towerPlaceContainer.SetEnabled(true);
            }
            else
            {
                towerPlaceContainer.visible = false;
                towerPlaceContainer.SetEnabled(false);
            }
        }

        private void OnUIContainersChanged(IList<IUIContainer> uiContainers)
        {
            if (activeContainer is not null)
            {
                UnBind();
            }

            if (uiContainers.TryFind(x => x.Name == containerId, out var container) && container is UIDocumentContainer documentContainer)
            {
                var root = documentContainer.Document.rootVisualElement;
                towerPlaceContainer = root.Q(towerPlaceContainerId);
            }

            void UnBind()
            {
            }
        }
    }
}