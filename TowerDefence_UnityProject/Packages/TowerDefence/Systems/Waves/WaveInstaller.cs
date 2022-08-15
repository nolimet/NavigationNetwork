using Zenject;

namespace TowerDefence.Systems.Waves
{
    public class WaveInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<WaveController>().AsSingle();
        }
    }
}