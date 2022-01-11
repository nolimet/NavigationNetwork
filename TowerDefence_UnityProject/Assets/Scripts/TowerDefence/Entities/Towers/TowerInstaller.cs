using UnityEngine;
using Zenject;

namespace TowerDefence.Entities.Towers
{
    [CreateAssetMenu(fileName = "Tower Installer", menuName = "Installers/Tower Installer")]
    public class TowerInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private TowerConfigurationData towerConfigurationData;

        public override void InstallBindings()
        {
            Container.BindInstance(towerConfigurationData).AsSingle();
            Container.BindInterfacesAndSelfTo<TowerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TowerController>().AsSingle().NonLazy();
            Container.BindTickableExecutionOrder<TowerController>(999);
        }
    }
}