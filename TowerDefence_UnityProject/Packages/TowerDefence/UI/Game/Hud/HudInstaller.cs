using TowerDefence.UI.Game.Hud.Drawers;
using TowerDefence.UI.Game.Waves;
using Zenject;

namespace TowerDefence.UI.Game.Hud
{
    public class HudInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TowerPlaceController>().AsSingle();
            Container.Bind<WaveHudController>().AsSingle();
        }
    }
}