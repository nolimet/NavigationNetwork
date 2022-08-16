using Zenject;

namespace TowerDefence.TestCode
{
    public class DebugInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WaveDebuggerStarter>().AsSingle().NonLazy();
        }
    }
}