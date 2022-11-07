using DataBinding;
using TowerDefence.Entities.Towers;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Game.Hud.Drawers.TowerPlace;
using TowerDefence.World.Grid;

namespace TowerDefence.UI.Game.Hud_old.SubControllers
{
    internal sealed class TowerPlaceController
    {
        private readonly BindingContext bindingContext = new();
        private readonly TowerService towerService;
        private readonly ISelectionModel selectionModel;
        private readonly GridWorld gridWorld;

        public TowerPlaceController(TowerService towerService, TowerPlaceHudDrawer hud, ISelectionModel selectionModel, GridWorld gridWorld)
        {
            this.towerService = towerService;
            this.selectionModel = selectionModel;
            this.gridWorld = gridWorld;

            hud.TowerButtonClickedCallback = OnHudButtonClicked;
        }

        private async void OnHudButtonClicked(string towerId, SelectableCell selectedCell)
        {
            selectionModel.Selection.Clear();

            var newTower = await towerService.PlaceTower(towerId, selectedCell.GridCell.WorldPosition, selectedCell.GridCell);
            selectionModel.Selection.Add(newTower);
            gridWorld.ClearPathCache();
        }

        ~TowerPlaceController()
        {
            bindingContext.Dispose();
        }
    }
}