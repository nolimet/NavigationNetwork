using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DataBinding;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers
{
    internal class PowerTickService : IDisposable
    {
        private readonly BindingContext bindingContext = new();
        private readonly List<ITickablePowerComponent> powerComponents = new();

        private readonly ITowerModels towerModels;
        private CancellationTokenSource cts;

        public PowerTickService(ITowerModels towerModels)
        {
            this.towerModels = towerModels;

            bindingContext.Bind(towerModels, x => x.Towers, OnTowersChanged);
        }

        private void OnTowersChanged(IList<ITowerObject> towers)
        {
            cts?.Cancel();
            cts?.Dispose();

            powerComponents.Clear();
            foreach (var tower in towers)
            {
                var foundComponents = tower.Model.Components.OfType<ITickablePowerComponent>();

                powerComponents.AddRange(foundComponents);
            }

            cts = new CancellationTokenSource();

            PowerUpdateLoop(cts.Token).SuppressCancellationThrow();
        }

        private async UniTask PowerUpdateLoop(CancellationToken ctx)
        {
            while (!ctx.IsCancellationRequested)
            {
                await UniTask.Delay(200, DelayType.Realtime, cancellationToken: ctx);
                foreach (var component in powerComponents) component.PowerTick(.200d);
            }
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
            cts?.Dispose();
        }
    }
}
