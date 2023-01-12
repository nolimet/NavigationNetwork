using TowerDefence.Systems.LevelEditor.Managers;
using TowerDefence.Systems.LevelEditor.UI;
using Zenject;

namespace TowerDefence.Systems.LevelEditor
{
    public class LevelEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelEditorController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelEditorUIController>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<WorldEditorManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<WorldEditorUIManager>().AsSingle().NonLazy();
        }
    }
}