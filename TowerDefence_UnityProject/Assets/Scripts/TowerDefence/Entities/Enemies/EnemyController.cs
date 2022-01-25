using DataBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies.Models;
using TowerDefence.World;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies
{
    public class EnemyController : ITickable
    {
        private readonly IEnemiesModel model;
        private readonly WorldContainer worldContainer;
        private readonly DiContainer container;

        private readonly Queue<IEnemyObject> deadEnemies = new();

        public EnemyController(DiContainer container, IEnemiesModel model, WorldContainer worldContainer)
        {
            this.container = container;
            this.model = model;
            this.worldContainer = worldContainer;
        }

        //TODO Rework completely when adding new system. Current implementation is completely wrong
        public async Task<IEnemyObject> CreateNewEnemy(AssetReference enemyAssetRefrence, AnimationCurve3D path)
        {
            var newEnemyGameObject = (GameObject)await enemyAssetRefrence.InstantiateAsync(worldContainer.EnemyContainer);
            container.InjectGameObject(newEnemyGameObject);

            if (newEnemyGameObject.TryGetComponent<EnemyObject>(out var newEnemy))
            {
                var model = ModelFactory.Create<IEnemyModel>();
                model.Components.Add(new Components.StaticPathWalker(path, newEnemy.transform, 5f, newEnemy, EnemyDied));
                model.Health = 10;
                model.MaxHealth = 10;

                newEnemy.Setup(model, EnemyDied);
                this.model.Enemies.Add(newEnemy);
                return newEnemy;
            }
            throw new System.Exception("Could not load enemy");
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