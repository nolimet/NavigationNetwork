using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TowerDefence.Entities.Enemies.Components;
using TowerDefence.Entities.Enemies.Models;
using static TowerDefence.Systems.Waves.Data.Wave;
using Object = UnityEngine.Object;

namespace TowerDefence.Entities.Enemies
{
    internal class EnemyController : IDisposable
    {
        private readonly IEnemiesModel model;
        private readonly EnemyFactory enemyFactory;
        private readonly CancellationTokenSource tokenSource = new();

        private readonly Queue<IEnemyObject> deadEnemies = new();

        internal EnemyController(IEnemiesModel model, EnemyFactory enemyFactory)
        {
            this.model = model;
            this.enemyFactory = enemyFactory;
            EnemyUpdateLoop(tokenSource.Token).Preserve().SuppressCancellationThrow().Forget();
        }

        //TODO Simplify workflow and add a enemy Creation service or something similar
        public async UniTask<IEnemyObject> CreateNewEnemy(string id, World.Path.Data.PathWorldData.AnimationCurve3D path)
        {
            var newEnemy = await enemyFactory.CreateEnemy(id, EnemyDied);

            if (newEnemy.Model.Components.Any(x => x is StaticPathWalker))
            {
                var walker = newEnemy.Model.Components.First(x => x is StaticPathWalker) as StaticPathWalker;

                walker.SetPath(path);
                walker.ReachedEnd = EnemyDied;
            }

            model.Enemies.Add(newEnemy);
            return newEnemy;
        }

        public async UniTask<IEnemyObject> CreateNewEnemy(EnemyGroup group)
        {
            var newEnemy = await enemyFactory.CreateEnemy(group.enemyID, EnemyDied);

            if (newEnemy.Model.Components.Any(x => x is GridPathWalker))
            {
                var pathFinder = newEnemy.Model.Components.First(x => x is GridPathWalker) as GridPathWalker;

                pathFinder.ReachedEnd = EnemyDied;
                await pathFinder.SetStartEnd(group);
            }

            model.Enemies.Add(newEnemy);
            return newEnemy;
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
        }

        private async UniTask EnemyUpdateLoop(CancellationToken token)
        {
            await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate(PlayerLoopTiming.EarlyUpdate))
            {
                token.ThrowIfCancellationRequested();
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