using TowerDefence.UI.Game.Hud.Controllers;
using TowerDefence.UI.Game.SelectionDrawer;
using Zenject;

namespace TowerDefence.UI.Game.Hud
{
    public class HudInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TowerPlaceController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WaveHudController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<SelectionAreaDrawer>().AsSingle().NonLazy();
        }
    }
}