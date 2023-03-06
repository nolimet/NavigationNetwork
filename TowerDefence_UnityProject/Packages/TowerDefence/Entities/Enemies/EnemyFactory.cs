using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DataBinding;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies.Components.Interfaces;
using TowerDefence.Entities.Enemies.Models;
using TowerDefence.World;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace TowerDefence.Entities.Enemies
{
    internal sealed class EnemyFactory
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
            var unloaded = enemyConfiguration.EnemyBaseObjects.Where(x => !x.Value.IsValid());
            var handles = unloaded.Select(x => x.Value.LoadAssetAsync());
            var asyncOperationHandles = handles as AsyncOperationHandle<GameObject>[] ?? handles.ToArray();
            if (!asyncOperationHandles.Any()) return;

            var tasks = asyncOperationHandles.Select(x => x.Task);
            await tasks.WaitForAll();
        }

        public async UniTask<IEnemyObject> CreateEnemy(string id, UnityAction<IEnemyObject> outHealthAction)
        {
            //TODO fix spike when creating an enemy. Do some timing tests to see what costs so much time here
            var configurationData = enemyConfiguration.Enemies[id];
            var enemyBase = enemyConfiguration.EnemyBaseObjects[configurationData.BaseId];
            var componentConfiguration = configurationData.ComponentConfiguration;

            var enemyGameObject = await enemyBase.InstantiateAsync(worldContainer.EnemyContainer) as GameObject;

            if (enemyGameObject == null)
                throw new NullReferenceException($"got null enemy gameObject for id: {id}");

            var enemyModel = ModelFactory.Create<IEnemyModel>();
            var enemyObject = enemyGameObject.GetComponent<EnemyObject>();

            var components = await componentFactory.GetComponents(componentConfiguration.Components, InitHandler);
            enemyModel.Components = components.ToList();

            enemyObject.Setup(enemyModel, outHealthAction);

            return enemyObject;

            async UniTask<IComponent> InitHandler(IComponent component)
            {
                //Cannot be a switch
                if (component is IInitializable initializable)
                {
                    initializable.PostInit(enemyObject, enemyModel);
                }

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (component is IAsyncInitializer asyncInitializer)
                {
                    await asyncInitializer.AsyncPostInit(enemyObject, enemyModel);
                }

                return component;
            }
        }
    }
}