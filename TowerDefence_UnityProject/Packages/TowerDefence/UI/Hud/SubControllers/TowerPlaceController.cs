using DataBinding;
using TowerDefence.Entities.Towers;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Hud.PlaceTower;
using TowerDefence.World.Grid;

namespace TowerDefence.UI.Hud.SubControllers
{
    internal class TowerPlaceController
    {
        private readonly BindingContext bindingContext = new(true);
        private readonly TowerService towerService;
        private readonly ISelectionModel selectionModel;

        public TowerPlaceController(TowerService towerService, TowerPlaceHudDrawer hud, ISelectionModel selectionModel)
        {
            this.towerService = towerService;
            this.selectionModel = selectionModel;

            hud.OnTowerButtonClickedCallback = OnHudButtonClicked;
        }

        private async void OnHudButtonClicked(string towerId, SelectableCell selectedCell)
        {
            selectionModel.Selection.Clear();

            var newTower = await towerService.PlaceTower(towerId, selectedCell.GridCell.WorldPosition, selectedCell.GridCell);
            selectionModel.Selection.Add(newTower);
        }

        ~TowerPlaceController()
        {
            bindingContext.Dispose();
        }
    }
}
