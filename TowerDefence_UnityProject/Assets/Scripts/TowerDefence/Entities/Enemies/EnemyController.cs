using DataBinding;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies.Models;
using TowerDefence.World;
using TowerDefence.World.Path;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies
{
    public class EnemyController
    {
        private readonly IEnemiesModel model;
        private readonly WorldContainer worldContainer;
        private readonly DiContainer container;
        private readonly PathWalkerService pathWalkerService;

        public EnemyController(DiContainer container, IEnemiesModel model, PathWalkerService pathWalkerService, WorldContainer worldContainer)
        {
            this.container = container;
            this.pathWalkerService = pathWalkerService;
            this.model = model;
            this.worldContainer = worldContainer;
        }

        public async Task<T> CreateNewEnemy<T>(AssetReference enemyAssetRefrence, AnimationCurve3D path) where T : EnemyBase
        {
            var newEnemyGameObject = (GameObject)await enemyAssetRefrence.InstantiateAsync(worldContainer.EnemyContainer);
            container.InjectGameObject(newEnemyGameObject);

            if (newEnemyGameObject.TryGetComponent<T>(out var newEnemy))
            {
                var model = ModelFactory.Create<IEnemyModel>();
                model.obj = newEnemy;
                model.transform = newEnemy.transform;

                newEnemy.Setup(EnemyDied, path, model);

                pathWalkerService.AddWalker(newEnemy);
                this.model.Enemies.Add(model);
                return newEnemy;
            }
            throw new System.Exception("Could not load enemy");
        }

        private void EnemyDied(IEnemyModel enemy)
        {
            model.Enemies.Remove(enemy);
            pathWalkerService.RemoveWalker(enemy.obj);
            Object.DestroyImmediate(enemy.obj.gameObject);
        }
    }
}