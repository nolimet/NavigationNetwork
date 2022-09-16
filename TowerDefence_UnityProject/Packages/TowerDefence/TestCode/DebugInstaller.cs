using Zenject;

namespace TowerDefence.TestCode
{
    public sealed class DebugInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WaveDebuggerStarter>().AsSingle().NonLazy();
        }
    }
}