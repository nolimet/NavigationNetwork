using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Data;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Utility;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    [Serializable]
    [Component(ComponentType.Tower, AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    public class PowerVisualDrawer : ITickableComponent, IInitializable, IDisposable
    {
        [SerializeField, JsonProperty] private AssetReferenceT<GameObject> visualPrefab;
        [JsonProperty] private readonly float decayRate;
        [JsonProperty] private readonly float activeLineWidth, inactiveLineWidth;

        private readonly List<(IPowerComponent component, LineRenderer line)> lineRenderers = new();
        private IPowerProducer powerProducer;

        public void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            powerProducer = model.Components.GetComponent<IPowerProducer>();

            powerProducer.PowerSend += OnPowerSend;
        }

        private void OnPowerSend(IReadOnlyCollection<PowerEventArgs> obj)
        {
        }

        public void Tick()
        {
        }

        public void Dispose()
        {
            powerProducer.PowerSend -= OnPowerSend;
        }
    }
}