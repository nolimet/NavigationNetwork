using System.Collections.Generic;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.World.Grid;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Game.Hud.PlaceTower
{
    internal class TowerPlaceHudDrawer : HudDrawerBase<SelectableCell>
    {
        public delegate void TowerSelectedCallback(string towerId, SelectableCell selectedCell);
        public TowerSelectedCallback OnTowerButtonClickedCallback;

        private SelectableCell selectableCell;
        private TowerPlaceButton.Factory placeButtonFactory;
        private TowerConfigurationData towerConfiguration;
        private ITowerModels towerModels;
        private readonly List<TowerPlaceButton> placeButtons = new();

        [SerializeField] private Transform buttonContainer;

        [Inject]
        public void Inject(TowerPlaceButton.Factory placeButtonFactory, TowerConfigurationData towerConfiguration, ITowerModels towerModels)
        {
            this.placeButtonFactory = placeButtonFactory;
            this.towerConfiguration = towerConfiguration;
            this.towerModels = towerModels;

            CreateButtons();
        }

        private void CreateButtons()
        {
            foreach (var tower in towerConfiguration.Towers)
            {
                var button = placeButtonFactory.Create(buttonContainer, tower.Key, tower.Key, OnButtonClicked);
                placeButtons.Add(button);
            }
        }

        private void OnButtonClicked(string towerId) => OnTowerButtonClickedCallback(towerId, selectableCell);
        public override void SetValue(ISelectable selectable)
        {
            if (selectable is not SelectableCell selectableCell || towerModels.CellHasTower(selectableCell.GridCell))
            {
                SetActive(false);
                return;
            }

            this.selectableCell = selectableCell;
        }
    }
}
