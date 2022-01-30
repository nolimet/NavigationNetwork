using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TowerDefence.Systems.Waves.Data;
using UnityEngine;

namespace TowerDefence.Systems.Waves
{
    public class WaveController : MonoBehaviour
    {
        private Wave[] currentWaves;
        private int activeWave = 0;
        private CancellationTokenSource cancelTokenSource;
        private List<Task> activeWaves = new List<Task>();

        public int GetWavesLeft()
        {
            return activeWave - currentWaves.Length;
        }

        public void SetWaves(Wave[] waves)
        {
            currentWaves = waves;

            if (!cancelTokenSource.IsCancellationRequested)
                cancelTokenSource.Cancel();
            cancelTokenSource.Dispose();
            cancelTokenSource = new CancellationTokenSource();
            activeWave = 0;
        }

        public async void StartWavePlayBack()
        {
            if (currentWaves != null && currentWaves.Length > 0)
            {
                while (activeWave < currentWaves.Length)
                {
                    activeWaves.Add(PlayWave(currentWaves[activeWave]));
                    activeWave++;
                    try
                    {
                        await activeWaves.WaitForAll();
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
                activeWaves.Add(PlayWave(currentWaves[activeWave]));
                activeWave++;
            }
        }

        public void StopWavePlayBack()
        {
            cancelTokenSource.Cancel();
        }

        private async Task PlayWave(Wave wave)
        {
            //process wavedata;
            Debug.Log("wave started");
            await new WaitForSeconds(1f);
            Debug.Log("wave ended");
        }
    }
}