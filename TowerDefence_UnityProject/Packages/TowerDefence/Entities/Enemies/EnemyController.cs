using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using NoUtil.Extentsions;
using TowerDefence.Entities.Enemies.Components;
using TowerDefence.Entities.Enemies.Models;
using static TowerDefence.Systems.Waves.Data.Wave;
using Object = UnityEngine.Object;

namespace TowerDefence.Entities.Enemies
{
    internal sealed class EnemyController : IDisposable
    {
        private readonly IEnemiesModel model;
        private readonly EnemyFactory enemyFactory;
        private readonly CancellationTokenSource tokenSource = new();

        private readonly Queue<IEnemyObject> deadEnemies = new();

        public event Action EnemyReachedEnd;

        internal EnemyController(IEnemiesModel model, EnemyFactory enemyFactory)
        {
            this.model = model;
            this.enemyFactory = enemyFactory;
            EnemyUpdateLoop(tokenSource.Token).Preserve().SuppressCancellationThrow().Forget();
        }

        public async UniTask<IEnemyObject> CreateNewEnemy(EnemyGroup group)
        {
            var newEnemy = await enemyFactory.CreateEnemy(group.EnemyID, EnemyDied);

            if (newEnemy.Model.Components.TryFind(x => x is GridPathWalker, out var comp) && comp is GridPathWalker pathFinder)
            {
                pathFinder.ReachedEnd = OnEnemyReachedEnd;
                await pathFinder.SetStartEnd(group);
            }

            model.Enemies.Add(newEnemy);
            return newEnemy;
        }

        private void KillAllEnemies()
        {
            foreach (var e in model.Enemies) Object.Destroy(e.Transform.gameObject);
            model.Enemies.Clear();
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            KillAllEnemies();
        }

        private async UniTask EnemyUpdateLoop(CancellationToken token)
        {
            await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate(PlayerLoopTiming.EarlyUpdate))
            {
                token.ThrowIfCancellationRequested();
                while (deadEnemies.Any()) DestroyEnemy(deadEnemies.Dequeue());

                foreach (var enemy in model.Enemies)
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
            if (!deadEnemies.Contains(enemy)) deadEnemies.Enqueue(enemy);
        }

        private void OnEnemyReachedEnd(IEnemyObject enemy)
        {
            enemy.Model.Health = 0;
            EnemyReachedEnd?.Invoke();
        }
    }
}
