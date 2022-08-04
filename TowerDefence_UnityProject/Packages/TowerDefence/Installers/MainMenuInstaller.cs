using TowerDefence.UI.Menu.LevelDisplay;
using Zenject;

namespace TowerDefence.Installers
{
    public class MainMenuInstaller : MonoInstaller<MainMenuInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelDisplayController>().AsSingle().NonLazy();
        }
    }
}