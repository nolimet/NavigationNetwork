using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Entities.Towers;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Game.Hud.CustomUIElements;
using TowerDefence.UI.Models;
using TowerDefence.World.Grid;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.Hud.Controllers
{
    public class TowerPlaceController : IDisposable
    {
        private const string ContainerId = "GameUI-HUD";
        private const string TowerPlaceContainerId = "TowerIconContainer";

        private readonly BindingContext bindingContext = new();
        private readonly TowerConfigurationData towerConfigurationData;
        private readonly TowerService towerService;
        private readonly ISelectionModel selectionModel;

        private IUIContainer activeContainer;
        private VisualElement towerPlaceContainer;

        private readonly List<TowerPlaceButton> towerPlaceButtons = new();

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


            if (selection.Count == 1 && selection.TryFind(x => x is SelectableCell, out var s) &&
                s is SelectableCell cell && !cell.GridCell.HasStructure)
            {
                towerPlaceButtons.ForEach(x => x.SetEnabled(cell.GridCell.SupportsTower));
                Update(true);
            }
            else
            {
                Update(false);
            }

            void Update(bool selectionValid)
            {
                towerPlaceContainer.visible = selectionValid;
                towerPlaceContainer.SetEnabled(selectionValid);
            }
        }

        private void OnUIContainersChanged(IList<IUIContainer> uiContainers)
        {
            if (activeContainer is not null)
            {
                UnBind();
            }

            if (uiContainers.TryFind(x => x.Name == ContainerId, out var container) && container is UIDocumentContainer documentContainer)
            {
                activeContainer = container;
                var root = documentContainer.Document.rootVisualElement;
                towerPlaceContainer = root.Q(TowerPlaceContainerId);

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
                    towerPlaceButtons.Add(towerButton);
                }
            }

            void UnBind()
            {
                towerPlaceContainer?.Clear();
                towerPlaceButtons.Clear();
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

        public void Dispose()
        {
            bindingContext?.Dispose();
        }
    }
}