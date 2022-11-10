using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Entities.Towers;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Game.Hud.CustomUIElements;
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
        private readonly TowerConfigurationData towerConfigurationData;
        private readonly TowerService towerService;

        private IUIContainer activeContainer;
        private ISelectionModel selectionModel;
        private VisualElement towerPlaceContainer;

        public TowerPlaceController(IUIContainers uiContainers, ISelectionModel selectionModel, TowerService towerService, TowerConfigurationData towerConfigurationData)
        {
            this.towerService = towerService;
            this.towerConfigurationData = towerConfigurationData;
            this.selectionModel = selectionModel;

            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainersChanged);
            bindingContext.Bind(selectionModel, x => x.Selection, OnSelectionChanged);
        }

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            if (towerPlaceContainer is null) return;

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

                var towers = towerConfigurationData.Towers;
                foreach (var (id, tower) in towers)
                {
                    var towerButton = new TowerPlaceButton(id)
                    {
                        text = tower.Id
                    };
                    towerButton.OnCallback += OnTowerPlaceButtonClicked;
                    towerButton.AddToClassList("HUD-TowerButton");
                    towerPlaceContainer.Add(towerButton);
                }
            }

            void UnBind()
            {
                if (towerPlaceContainer is null) return;

                foreach (var child in towerPlaceContainer.Children().Where(x => x is TowerPlaceButton))
                {
                    child.RemoveFromHierarchy();
                }
            }
        }

        private void OnTowerPlaceButtonClicked(string towerId)
        {
            if (selectionModel.Selection.Any(x => x is SelectableCell))
            {
                var cell = selectionModel.Selection.First(x => x is SelectableCell) as SelectableCell;
                towerService.PlaceTower(towerId, cell!.GridCell.WorldPosition, cell.GridCell).Forget();
            }
        }
    }
}