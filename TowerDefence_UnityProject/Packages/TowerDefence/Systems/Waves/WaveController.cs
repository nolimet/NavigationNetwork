using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DataBinding;
using TowerDefence.Entities.Enemies;
using TowerDefence.Systems.Waves.Data;
using TowerDefence.Systems.WorldLoader.Models;
using TowerDefence.World.Grid;
using UnityEngine;
using static TowerDefence.Systems.Waves.Data.Wave;

namespace TowerDefence.Systems.Waves
{
    public class WaveController
    {
        private Wave[] currentWaves;
        private int activeWave = 0;
        private CancellationTokenSource cancelTokenSource;
        private List<UniTask> activeWaves = new List<UniTask>();

        private readonly EnemyController enemyController;
        private readonly GridWorld gridWorld;
        private readonly BindingContext bindingContext = new();

        internal WaveController(EnemyController enemyController, GridWorld gridWorld, IWorldDataModel worldDataModel)
        {
            this.enemyController = enemyController;
            this.gridWorld = gridWorld;
            bindingContext.Bind(worldDataModel, x => x.Waves, OnWavesChanged);
        }

        private void OnWavesChanged(Wave[] obj)
        {
            currentWaves = obj;

            if (cancelTokenSource != null && !cancelTokenSource.IsCancellationRequested)
                cancelTokenSource.Cancel();
            cancelTokenSource?.Dispose();

            cancelTokenSource = new CancellationTokenSource();
            activeWave = 0;
        }

        public int GetWavesLeft()
        {
            return activeWave - currentWaves.Length;
        }

        public async void StartWavePlayBack()
        {
            if (currentWaves != null && currentWaves.Length > 0)
            {
                while (activeWave < currentWaves.Length)
                {
                    activeWaves.Add(PlayWave(currentWaves[activeWave], cancelTokenSource.Token));
                    activeWave++;
                    try
                    {
                        await UniTask.WhenAll(activeWaves);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
            }
        }

        public void ForceStartNextWave()
        {
            if (currentWaves != null && currentWaves.Length > 0 && GetWavesLeft() > 0)
            {
                activeWaves.Add(PlayWave(currentWaves[activeWave], cancelTokenSource.Token));
                activeWave++;
            }
        }

        public void StopWavePlayBack()
        {
            cancelTokenSource.Cancel();
        }

        private async UniTask PlayWave(Wave wave, CancellationToken token)
        {
            Debug.Log("wave started");
            var waveLookup = new (EnemyGroup group, Queue<float> time)[wave.enemyGroups.Length];
            int enemiesRemaining = 0;
            var enemyWatchers = new List<UniTask>();
            for (int i = 0; i < waveLookup.Length; i++)
            {
                var v = (wave.enemyGroups[i], new Queue<float>());
                waveLookup[i] = v;
                for (int j = 0; j < v.Item1.spawnTime.Length; j++)
                {
                    v.Item2.Enqueue(v.Item1.spawnTime[j]);
                }
            }

            float t = 0f;
            await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().WithCancellation(token))
            {
                if (waveLookup.All(x => !x.time.Any()))
                    break;

                foreach (var enemySet in waveLookup)
                {
                    if (enemySet.time.Any() && enemySet.time.Peek() < t)
                    {
                        enemySet.time.Dequeue();
                        enemyWatchers.Add(EnemyWatcherTask(enemySet.group));
                    }
                }

                t += Time.deltaTime;
            }

            await UniTask.WhenAll(enemyWatchers);

            Debug.Log("wave ended");

            async UniTask EnemyWatcherTask(EnemyGroup group)
            {
                enemiesRemaining++;
                var enemy = await enemyController.CreateNewEnemy(group);
                var model = enemy.Model;

                await UniTask.WaitUntil(() => model.Health <= 0);
                enemiesRemaining--;
            }

            ;
        }
    }
}