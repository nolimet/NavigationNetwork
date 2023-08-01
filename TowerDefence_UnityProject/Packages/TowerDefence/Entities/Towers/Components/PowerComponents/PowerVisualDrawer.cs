using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataBinding;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Data;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Object = UnityEngine.Object;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable]
    [Component(ComponentType.Tower, AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    public class PowerVisualDrawer : ITickableComponent, IAsyncInitializer, IDisposable
    {
        [SerializeField] [JsonProperty] private AssetReferenceT<GameObject> visualPrefab;
        [JsonProperty] private readonly float decayPerSecond = .5f;
        [JsonProperty] private readonly float maxLineWidth = .5f, minLineWidthMultiplier = 0.1f;

        private readonly Dictionary<IPowerComponent, LineRenderer> lineRenderers = new();
        [Inject] private readonly ITowerModels towerModels;
        private readonly BindingContext bindingContext = new();

        private IPowerProducer powerProducer;
        private TowerSettings towerSettings;
        private ITowerObject self;
        private GameObject linePrefab;

        public async Task AsyncPostInit(ITowerObject towerObject, ITowerModel model)
        {
            self = towerObject;
            powerProducer = model.Components.GetComponent<IPowerProducer>();
            towerSettings = model.Components.GetComponent<TowerSettings>();

            powerProducer.PowerSend += OnPowerSend;

            var handle = visualPrefab.LoadAssetAsync();
            await handle.Task;
            linePrefab = handle.Result;

            bindingContext.Bind(towerModels, x => x.Towers, OnTowersChanged);
        }

        private void OnTowersChanged(IList<ITowerObject> towers)
        {
            double dist = towerSettings.Range;
            Vector2 towerPos = self.GetWorldPosition();


            foreach (var tower in towers)
            {
                var otherTowerPos = tower.GetWorldPosition();
                if (tower == self || !(Vector2.Distance(otherTowerPos, towerPos) < dist)) continue;
                if (!tower.Model.Components.TryGetComponent<IPowerComponent>(out var component)) continue;
                if (!component.CanReceive) continue;
                if (lineRenderers.ContainsKey(component)) continue;

                var newLineGameObject = Object.Instantiate(linePrefab, self.Transform, false);
                var newLine = newLineGameObject.GetComponent<LineRenderer>();

                newLine.positionCount = 2;
                newLine.SetPosition(0, towerPos);
                newLine.SetPosition(1, otherTowerPos);

                newLine.startWidth = newLine.endWidth = maxLineWidth;
                newLine.widthMultiplier = minLineWidthMultiplier;

                lineRenderers.Add(component, newLine);
            }
        }

        private void OnPowerSend(IReadOnlyCollection<PowerEventArg> powerEventArgs)
        {
            foreach (var powerEventArg in powerEventArgs)
            {
                if (lineRenderers.TryGetValue(powerEventArg.target, out var lineRenderer))
                {
                    lineRenderer.widthMultiplier = Mathf.Max(minLineWidthMultiplier, (float)powerEventArg.acceptPercentage);
                }
            }
        }

        public void Tick()
        {
            //TODO make more efficient
            foreach (var (component, line) in lineRenderers)
            {
                line.widthMultiplier = Mathf.MoveTowards(line.widthMultiplier, minLineWidthMultiplier, decayPerSecond * Time.deltaTime);
            }
        }

        public void Dispose()
        {
            powerProducer.PowerSend -= OnPowerSend;
            linePrefab = null;
            visualPrefab.ReleaseAsset();
        }
    }
}