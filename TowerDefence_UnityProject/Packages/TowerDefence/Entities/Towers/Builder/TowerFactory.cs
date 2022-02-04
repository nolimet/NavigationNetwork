using DataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Towers.Builder.Data;
using TowerDefence.Entities.Towers.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.World;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Entities.Towers.Builder
{
    internal class TowerFactory
    {
        private AssetReference baseTower;
        private WorldContainer worldContainer;

        public async Task<ITowerObject> CreateTower(ComponentConfigurationObject towerconfiguration)
        {
            var towerGameObject = await baseTower.InstantiateAsync(worldContainer.TowerContainer, false) as GameObject;

            var towerObject = towerGameObject.GetComponent<TowerObject>();
            var model = ModelFactory.Create<ITowerModel>();

            foreach (var componentData in towerconfiguration.components)
            {
                var component = componentData.DeserializeTowerComponent();

                model.Components.Add(component);
            }

            List<Task> asyncInitTasks = new List<Task>();
            foreach (var component in model.Components)
            {
                if (component is IInitializableComponent initializable)
                {
                    initializable.PostInit(towerObject, model);
                }
                if (component is IAsyncInitializer asyncInitializer)
                {
                    asyncInitTasks.Add(asyncInitializer.AsyncPostInit(towerObject, model));
                }
            }

            await asyncInitTasks.WaitForAll();

            return towerObject;
        }
    }
}