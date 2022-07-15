using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TowerDefence.Entities.Enemies;
using TowerDefence.Systems.Waves.Data;
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

        internal WaveController(EnemyController enemyController, GridWorld gridWorld)
        {
            this.enemyController = enemyController;
            this.gridWorld = gridWorld;
        }

        public int GetWavesLeft()
        {
            return activeWave - currentWaves.Length;
        }

        public void SetWaves(Wave[] waves)
        {
            currentWaves = waves;

            if (cancelTokenSource != null && !cancelTokenSource.IsCancellationRequested)
                cancelTokenSource.Cancel();
            cancelTokenSource?.Dispose();

            cancelTokenSource = new CancellationTokenSource();
            activeWave = 0;
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
                        enemyController.CreateNewEnemy(enemySet.group).Forget();
                    }
                }
                t += Time.deltaTime;
            }
            Debug.Log("wave ended");
        }
    }
}