﻿using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataBinding;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Data;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.World;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Builder
{
    internal sealed class TowerFactory
    {
        private readonly WorldContainer worldContainer;
        private readonly ComponentFactory componentFactory;
        private readonly TowerConfigurationData towerConfiguration;

        public TowerFactory(WorldContainer worldContainer, ComponentFactory componentFactory, TowerConfigurationData towerConfiguration)
        {
            this.worldContainer = worldContainer;
            this.componentFactory = componentFactory;
            this.towerConfiguration = towerConfiguration;

            WarmPrefabs();
        }

        private async void WarmPrefabs()
        {
            if (!towerConfiguration.TowerBase.IsValid())
                await towerConfiguration.TowerBase.LoadAssetAsync();
        }

        public async UniTask<ITowerObject> CreateTower(ComponentConfigurationObject componentConfiguration, Vector2 position, IGridCell cell)
        {
            var towerGameObject = await towerConfiguration.TowerBase.InstantiateAsync(worldContainer.TowerContainer) as GameObject;

            if (!towerGameObject) throw new NullReferenceException($"Could not create tower base for configuration {towerConfiguration.TowerBase}");

            var towerObject = towerGameObject.GetComponent<TowerObject>();
            var model = ModelFactory.Create<ITowerModel>();

            towerObject.Setup(model, cell);

            await componentFactory.GetComponents(componentConfiguration.Components, AssignList, ProcessComponentInit);

            return towerObject;

            void AssignList(List<IComponent> components)
            {
                model.Components = components;
            }

            async UniTask<IComponent> ProcessComponentInit(IComponent arg)
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