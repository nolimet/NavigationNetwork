using TowerDefence.UI.Game.Hud.Drawers;
using TowerDefence.UI.Game.Hud.Drawers.TowerPlace;
using TowerDefence.UI.Game.Hud.SubControllers;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Game.Hud_old
{
    public sealed class HudInstaller : MonoInstaller
    {
        [SerializeField] private TowerHudDrawer towerHud;
        [SerializeField] private TowerPlaceHudDrawer towerPlaceHud;
        [SerializeField] private TowerPlaceButton towerPlaceButtonPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TowerPlaceController>().AsCached().NonLazy();
            Container.BindFactory<TowerPlaceButton, TowerPlaceButton.Factory>().FromComponentInNewPrefab(towerPlaceButtonPrefab);

            Container.BindInstance(towerPlaceHud).AsCached();
            Container.BindInstance(towerHud).AsCached();
        }
    }
}