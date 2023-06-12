using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Entities.Towers;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Game.Hud.CustomUIElements;
using TowerDefence.UI.Models;
using TowerDefence.World.Grid.Data;
using UnityEngine;
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
        private CancellationTokenSource ctx;

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

            if (selection.Count == 1 && selection.TryFind(x => x is IGridCell, out var s) && s is IGridCell cell)
            {
                towerPlaceButtons.ForEach(x => x.SetEnabled(cell.SupportsTower && !cell.HasStructure && !cell.HasVirtualStructure));
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
            if (uiContainers.TryFind(x => x.Name == ContainerId, out var container) && container is UIDocumentContainer documentContainer)
            {
                if (activeContainer == container) return;
                if (activeContainer is not null) UnBind();

                activeContainer = container;
                var root = documentContainer.Document.rootVisualElement;
                var towerPlaceContainer = root.Q(TowerPlaceContainerId);
                if (towerPlaceContainer == this.towerPlaceContainer || towerPlaceContainer is null) return;

                this.towerPlaceContainer = towerPlaceContainer;
                ctx?.Cancel();
                ctx?.Dispose();
                ctx = new CancellationTokenSource();
                PopulateContainer(ctx.Token).Preserve();
            }

            void UnBind()
            {
                towerPlaceContainer?.Clear();
                towerPlaceButtons.Clear();
            }
        }

        private async UniTask PopulateContainer(CancellationToken ct)
        {
            await UniTask.NextFrame(ct);

            var towers = towerConfigurationData.Towers;
            foreach (var (id, tower) in towers)
            {
                ct.ThrowIfCancellationRequested();
                Sprite icon;
                if (!tower.Icon.IsValid())
                    icon = await tower.Icon.LoadAssetAsync().WithCancellation(ct);
                else
                    icon = tower.Icon.Asset as Sprite;

                var towerButton = new TowerPlaceButton(id)
                {
                    tooltip = id,
                    style =
                    {
                        backgroundImage = new StyleBackground(icon)
                    }
                };
                towerButton.OnCallback += OnTowerPlaceButtonClicked;
                towerButton.AddToClassList("HUD-TowerButton");
                towerPlaceContainer.Add(towerButton);
                towerPlaceButtons.Add(towerButton);
            }
        }

        private void OnTowerPlaceButtonClicked(string towerId)
        {
            if (selectionModel.Selection.All(x => x is not IGridCell)) return;

            var cell = selectionModel.Selection.First(x => x is IGridCell) as IGridCell;
            towerService.PlaceTower(towerId, cell!.WorldPosition, cell).Forget();

            towerPlaceButtons.ForEach(x => x.SetEnabled(cell.SupportsTower && !cell.HasStructure && !cell.HasVirtualStructure));
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
        }
    }
}