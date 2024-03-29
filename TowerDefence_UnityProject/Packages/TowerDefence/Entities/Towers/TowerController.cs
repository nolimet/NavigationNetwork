﻿using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Threading;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers
{
    public sealed class TowerController : IDisposable
    {
        private readonly ITowerModels towerModels;
        private readonly CancellationTokenSource tokenSource = new();
        public TowerController(ITowerModels towerModels)
        {
            this.towerModels = towerModels;
            TowerUpdateLoop(tokenSource.Token).Preserve().SuppressCancellationThrow().Forget();
        }

        private async UniTask TowerUpdateLoop(CancellationToken token)
        {
            await foreach (var _ in UniTaskAsyncEnumerable.EveryUpdate(PlayerLoopTiming.LastUpdate))
            {
                token.ThrowIfCancellationRequested();
                foreach (var tower in towerModels.Towers)
                {
                    tower.Tick();
                }
            }
        }

        public void Dispose()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
        }
    }
}