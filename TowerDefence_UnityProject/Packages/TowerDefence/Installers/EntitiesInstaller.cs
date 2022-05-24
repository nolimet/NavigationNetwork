using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Builder;
using UnityEngine;
using Zenject;

namespace TowerDefence.Installers
{
    [CreateAssetMenu(fileName = "Entities Installer", menuName = "Installers/Entities Installer")]
    public class EntitiesInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private EnemyConfigurationData enemyConfigurationData;
        [SerializeField] private TowerConfigurationData towerConfigurationData;

        public override void InstallBindings()
        {
            //Entities
            Container.BindInterfacesAndSelfTo<ComponentFactory>().AsSingle();

            //Enemies
            Container.BindInstance(enemyConfigurationData).AsSingle();
            Container.Bind<EnemyFactory>().ToSelf().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyController>().AsSingle().NonLazy();
            Container.BindTickableExecutionOrder<EnemyController>(998);

            //Towers
            Container.BindInstance(towerConfigurationData).AsSingle();
            Container.BindInterfacesAndSelfTo<TowerService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TowerController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TowerFactory>().AsSingle();
        }
    }
}