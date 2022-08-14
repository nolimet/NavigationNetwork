﻿using System.Threading.Tasks;
using TowerDefence.UI.Game.Health;
using TowerDefence.UI.Game.Tower.Range;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace TowerDefence.UI.Installers
{
    [CreateAssetMenu(fileName = "Game UI Installer", menuName = "Installers/UI Installers/Game")]
    public class GameUIInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private HealthDrawer healthbarPrefab;

        [SerializeField] private AssetReference hudControllerPrefab;
        [SerializeField] private AssetReference towerRangeDrawer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HealthDrawerController>().AsSingle().NonLazy();
            Container.BindFactory<HealthDrawer, HealthDrawer.Factory>().FromComponentInNewPrefab(healthbarPrefab);

            Container.BindInterfacesAndSelfTo<TowerRangeDrawerController>().AsSingle().WithArguments(towerRangeDrawer).NonLazy();

            InstallAsync();
        }

        private async void InstallAsync()
        {
            await Task.Delay(100);
            var hudPrefab = await this.hudControllerPrefab.LoadAssetAsync<GameObject>() as GameObject;
            Container.InstantiatePrefab(hudPrefab, Container.Resolve<UIContainer>().ScreenUIContainer);
        }
    }
}