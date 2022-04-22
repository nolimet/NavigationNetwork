﻿using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Enemies.Components;
using TowerDefence.Entities.Enemies.Models;
using TowerDefence.World;
using UnityEngine;
using Zenject;

namespace TowerDefence.Entities.Enemies
{
    public class EnemyController : ITickable
    {
        private readonly IEnemiesModel model;
        private readonly WorldContainer worldContainer;
        private readonly EnemyFactory enemyFactory;
        private readonly EnemyConfigurationData configurationData;
        private readonly DiContainer container;

        private readonly Queue<IEnemyObject> deadEnemies = new();

        internal EnemyController(DiContainer container, IEnemiesModel model, WorldContainer worldContainer, EnemyFactory enemyFactory, EnemyConfigurationData configurationData)
        {
            this.container = container;
            this.model = model;
            this.worldContainer = worldContainer;
            this.enemyFactory = enemyFactory;
            this.configurationData = configurationData;
        }

        //TODO Simplify workflow and add a enemy Creation service or something similar
        public async UniTask<IEnemyObject> CreateNewEnemy(string id, World.Path.Data.PathWorldData.AnimationCurve3D path)
        {
            var components = configurationData.Enemies[id];
            var baseObject = configurationData.EnemyBaseObjects[components.BaseId];
            var newEnemy = await enemyFactory.CreateEnemy(components.ComponentConfiguration, baseObject, EnemyDied);

            if (newEnemy.Model.Components.Any(x => x is StaticPathWalker))
            {
                var walker = newEnemy.Model.Components.First(x => x is StaticPathWalker) as StaticPathWalker;
                walker.SetPath(path);
                walker.ReachedEnd = EnemyDied;
            }

            model.Enemies.Add(newEnemy);
            return newEnemy;
        }

        public void Tick()
        {
            while (deadEnemies.Any())
            {
                DestroyEnemy(deadEnemies.Dequeue());
            }

            foreach (var enemy in this.model.Enemies)
                enemy.Tick();

            void DestroyEnemy(IEnemyObject enemy)
            {
                model.Enemies.Remove(enemy);
                Object.Destroy(enemy.Transform.gameObject);
            }
        }

        private void EnemyDied(IEnemyObject enemy)
        {
            if (!deadEnemies.Contains(enemy))
            {
                deadEnemies.Enqueue(enemy);
            }
        }
    }
}