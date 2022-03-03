using DataBinding;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Data;
using TowerDefence.Entities.Enemies.Components.Interfaces;
using TowerDefence.Entities.Enemies.Models;
using TowerDefence.World;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Entities.Enemies
{
    internal class EnemyFactory
    {
        private readonly WorldContainer worldContainer;
        private readonly ComponentFactory componentFactory;
        private readonly EnemyConfigurationData enemyConfiguration;

        public EnemyFactory(WorldContainer worldContainer, ComponentFactory componentFactory, EnemyConfigurationData enemyConfiguration)
        {
            this.worldContainer = worldContainer;
            this.componentFactory = componentFactory;
            this.enemyConfiguration = enemyConfiguration;

            WarmupPrefabs();
        }

        private async void WarmupPrefabs()
        {
            var handles = enemyConfiguration.EnemyBaseObjects.Select(x => x.Value.LoadAssetAsync()).ToList();
            var tasks = handles.Select(x => x.Task);
            await tasks.WaitForAll();
        }

        public async Task<IEnemyObject> CreateEnemy(ComponentConfigurationObject componentConfiguration, AssetReferenceT<EnemyObject> enemyBase)
        {
            var enemyGameObject = await enemyBase.InstantiateAsync(worldContainer.EnemyContainer, false) as GameObject;

            var enemyModel = ModelFactory.Create<IEnemyModel>();
            var enemyObject = enemyGameObject.GetComponent<EnemyObject>();

            enemyObject.Setup(enemyModel);

            var components = await componentFactory.GetComponents(componentConfiguration.components, InitHandler);

            async Task<IComponent> InitHandler(IComponent component)
            {
                if (component is IInitializable initializable)
                {
                    initializable.PostInit(enemyObject, enemyModel);
                }

                if (component is IAsyncInitializer asyncInitializer)
                {
                    await asyncInitializer.AsyncPostInit(enemyObject, enemyModel);
                }
                return component;
            }
        }
    }
}