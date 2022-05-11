using System.Collections.Generic;
using TowerDefence.Entities.Towers;
using TowerDefence.World.Grid;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Hud.PlaceTower
{
    internal class TowerPlaceHudDrawer : HudDrawerBase<SelectableCell>
    {
        public delegate void TowerSelectedCallback(string towerId, SelectableCell selectedCell);
        public TowerSelectedCallback OnTowerButtonClickedCallback;

        [HideInInspector] public SelectableCell selectedCell;

        private TowerPlaceButton.Factory placeButtonFactory;
        private TowerConfigurationData towerConfiguration;
        private readonly List<TowerPlaceButton> placeButtons = new();

        [SerializeField] private Transform buttonContainer;

        [Inject]
        public void Inject(TowerPlaceButton.Factory placeButtonFactory, TowerConfigurationData towerConfiguration)
        {
            this.placeButtonFactory = placeButtonFactory;
            this.towerConfiguration = towerConfiguration;
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

        private void OnButtonClicked(string towerId) => OnTowerButtonClickedCallback(towerId, selectedCell);
    }
}
