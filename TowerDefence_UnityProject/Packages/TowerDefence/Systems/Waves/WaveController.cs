using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DataBinding;
using TowerDefence.Entities.Enemies;
using TowerDefence.Systems.Waves.Data;
using TowerDefence.Systems.WorldLoader.Models;
using UnityEngine;
using static TowerDefence.Systems.Waves.Data.Wave;

namespace TowerDefence.Systems.Waves
{
    public sealed class WaveController
    {
        private Wave[] currentWaves;
        private int activeWave;
        private CancellationTokenSource cancelTokenSource;
        private readonly List<UniTask> activeWaves = new();

        private readonly EnemyController enemyController;
        private readonly BindingContext bindingContext = new();

        internal WaveController(EnemyController enemyController, IWorldDataModel worldDataModel)
        {
            this.enemyController = enemyController;
            bindingContext.Bind(worldDataModel, x => x.Waves, OnWavesChanged);
        }

        private void OnWavesChanged(Wave[] waves)
        {
            currentWaves = waves;

            if (cancelTokenSource is { IsCancellationRequested: false })
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
            if (currentWaves is not { Length: > 0 }) return;

            while (activeWave < currentWaves.Length)
            {
                Debug.Log($"Starting wave {activeWave}");
                activeWaves.Add(PlayWave(currentWaves[activeWave], cancelTokenSource.Token));
                activeWave++;

                try
                {
                    await UniTask.WhenAll(activeWaves);
                    activeWaves.Clear();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public void ForceStartNextWave()
        {
            if (currentWaves is not { Length: > 0 } || GetWavesLeft() <= 0) return;

            activeWaves.Add(PlayWave(currentWaves[activeWave], cancelTokenSource.Token));
            activeWave++;
        }

        public void StopWavePlayBack()
        {
            cancelTokenSource.Cancel();
        }

        private async UniTask PlayWave(Wave wave, CancellationToken token)
        {
            Debug.Log("wave started");
            var waveLookup = new (EnemyGroup group, Queue<float> time)[wave.enemyGroups.Length];
            var enemyWatchers = new List<UniTask>();
            for (int i = 0; i < waveLookup.Length; i++)
            {
                var (newGroup, spawnTimes) = waveLookup[i] = (wave.enemyGroups[i], new Queue<float>());
                foreach (var spawnTime in newGroup.spawnTime)
                {
                    spawnTimes.Enqueue(spawnTime);
                }
            }

            float t = 0f;
            await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate().WithCancellation(token))
            {
                if (waveLookup.All(x => !x.time.Any()))
                    break;

                foreach (var enemySet in waveLookup)
                {
                    if (!enemySet.time.Any() || enemySet.time.Peek() > t) continue;

                    enemySet.time.Dequeue();
                    enemyWatchers.Add(EnemyWatcherTask(enemySet.group));
                }

                t += Time.deltaTime;
            }

            await UniTask.WhenAll(enemyWatchers);

            Debug.Log("wave ended");

            async UniTask EnemyWatcherTask(EnemyGroup group)
            {
                var enemy = await enemyController.CreateNewEnemy(group);
                var model = enemy.Model;

                await UniTask.WaitUntil(() => model.Health <= 0, cancellationToken: token);
            }
        }
    }
}