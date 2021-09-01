using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.World.Path;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies
{
    public class EnemyController
    {
        private readonly List<EnemyBase> enemies = new List<EnemyBase>();
        private readonly DiContainer container;
        private readonly PathWalkerService pathWalkerService;

        public EnemyController(DiContainer container, PathWalkerService pathWalkerService)
        {
            this.container = container;
            this.pathWalkerService = pathWalkerService;
        }

        public async Task<T> CreateNewEnemy<T>(AssetReference enemyAssetRefrence, AnimationCurve3D path) where T : EnemyBase
        {
            var newEnemyGameObject = (GameObject)await enemyAssetRefrence.InstantiateAsync();
            container.InjectGameObject(newEnemyGameObject);

            if (newEnemyGameObject.TryGetComponent<T>(out var newEnemy))
            {
                newEnemy.Setup(EnemyReachedEnd, EnemyOutOfHealth, path);
                pathWalkerService.AddWalker(newEnemy);

                return newEnemy;
            }
            throw new System.Exception("Could not load enemy");
        }

        private void EnemyOutOfHealth(EnemyBase enemy)
        {
            enemies.Remove(enemy);
            pathWalkerService.RemoveWalker(enemy);
            Object.DestroyImmediate(enemy.gameObject);
        }

        private void EnemyReachedEnd(EnemyBase enemy)
        {
            enemies.Remove(enemy);
            pathWalkerService.RemoveWalker(enemy);
            Object.DestroyImmediate(enemy.gameObject);

            //TODO send a message to player lives tracker or somethin
        }
    }
}