using TowerDefence.UI.MainMenu;
using TowerDefence.UI.MainMenu.LevelDisplay;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Installers
{
    [CreateAssetMenu(fileName = "MainMenu UI Installer", menuName = "Installers/UI Installers/Main Menu")]
    public class MainMenuUIInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelDisplayController>().AsSingle().NonLazy();
            Container.Bind<MainMenuController>().AsSingle().NonLazy();
        }
    }
}