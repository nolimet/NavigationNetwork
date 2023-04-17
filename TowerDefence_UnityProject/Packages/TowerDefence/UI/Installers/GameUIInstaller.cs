using TowerDefence.UI.Game.Health;
using TowerDefence.UI.Game.Hud;
using TowerDefence.UI.Game.PauseMenu;
using TowerDefence.UI.Game.Tower.Properties;
using TowerDefence.UI.Game.Tower.Range;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace TowerDefence.UI.Installers
{
    [CreateAssetMenu(fileName = "Game UI Installer", menuName = "Installers/UI Installers/Game")]
    public sealed class GameUIInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private HealthDrawer healthbarPrefab;

        [SerializeField] private AssetReference towerRangeDrawer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HealthDrawerController>().AsSingle().NonLazy();
            Container.BindFactory<HealthDrawer, HealthDrawer.Factory>().FromComponentInNewPrefab(healthbarPrefab);

            Container.BindInterfacesAndSelfTo<TowerRangeDrawerController>().AsSingle().WithArguments(towerRangeDrawer).NonLazy();

            Container.BindInterfacesAndSelfTo<PauseMenuController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerPropertiesExtractor>().AsSingle().NonLazy();

            Container.Install<HudInstaller>();
        }
    }
}
