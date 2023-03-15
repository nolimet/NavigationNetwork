using TowerDefence.UI.Game.Hud.Controllers;
using Zenject;

namespace TowerDefence.UI.Game.Hud
{
    public class HudInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TowerPlaceController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WaveHudController>().AsSingle().NonLazy();
        }
    }
}