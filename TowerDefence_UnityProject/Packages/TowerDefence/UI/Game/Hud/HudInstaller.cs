using TowerDefence.UI.Game.Hud.Drawers;
using TowerDefence.UI.Game.Waves;
using Zenject;

namespace TowerDefence.UI.Game.Hud
{
    public class HudInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<TowerPlaceController>().AsSingle().NonLazy();
            Container.Bind<WaveHudController>().AsSingle().NonLazy();
        }
    }
}