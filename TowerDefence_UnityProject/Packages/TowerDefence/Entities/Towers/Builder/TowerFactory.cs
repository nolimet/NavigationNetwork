using DataBinding;
using System;
using System.Threading.Tasks;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Data;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.World;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TowerDefence.Entities.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Builder
{
    internal class TowerFactory
    {
        private readonly WorldContainer worldContainer;
        private readonly ComponentFactory componentFactory;
        private readonly TowerConfigurationData towerConfiguration;

        public TowerFactory(WorldContainer worldContainer, ComponentFactory componentFactory, TowerConfigurationData towerConfiguration)
        {
            this.worldContainer = worldContainer;
            this.componentFactory = componentFactory;
            this.towerConfiguration = towerConfiguration;
        }

        public async Task<ITowerObject> CreateTower(ComponentConfigurationObject componentConfiguration, Vector2 position)
        {
            var towerGameObject = await towerConfiguration.TowerBase.InstantiateAsync(worldContainer.TowerContainer, false) as GameObject;

            var towerObject = towerGameObject.GetComponent<TowerObject>();
            var model = ModelFactory.Create<ITowerModel>();

            await componentFactory.GetComponents(componentConfiguration.components, ProcessComponentInit);

            return towerObject;

            async Task<IComponent> ProcessComponentInit(IComponent arg)
            {
                if (arg is IInitializable initializable)
                {
                    initializable.PostInit(towerObject, model);
                }

                if (arg is IAsyncInitializer asyncInitializer)
                {
                    await asyncInitializer.AsyncPostInit(towerObject, model);
                }

                return arg;
            }
        }
    }
}