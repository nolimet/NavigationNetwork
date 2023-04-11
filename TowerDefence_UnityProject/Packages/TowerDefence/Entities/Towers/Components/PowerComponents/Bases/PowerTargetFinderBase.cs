using System;
using System.Collections.Generic;
using DataBinding;
using Newtonsoft.Json;
using NoUtil;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Utility;
using UnityEngine;
using Zenject;
using IInitializable = TowerDefence.Entities.Towers.Components.Interfaces.IInitializable;

namespace TowerDefence.Entities.Towers.Components.PowerComponents.Bases
{
    [Serializable]
    [Component(ComponentType.Tower, AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    internal class PowerTargetFinderBase : IPowerTargetFinder, IInitializable
    {
        public IReadOnlyList<IPowerComponent> Targets => TargetComponents;

        [SerializeField] [LayerDropdown] [JsonProperty]
        private int raycastLayer;

        protected readonly List<IPowerComponent> TargetComponents = new();
        private readonly RaycastHit2D[] results = new RaycastHit2D[128];
        private readonly BindingContext bindingContext = new();

        [Inject] private readonly ITowerModels towerModels;

        private TowerSettings towerSettings;
        private ITowerObject self;

        public void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            towerSettings = model.Components.GetComponent<TowerSettings>();
            if (towerSettings is null)
                throw new NullReferenceException("TowerSettings are missing are required for this component");

            self = towerObject;

            bindingContext.Bind(towerModels, x => x.Towers, OnTowersChanged);
        }

        private void OnTowersChanged(IList<ITowerObject> obj)
        {
            UpdateTargets(self.GetWorldPosition());
        }

        protected void UpdateTargets(Vector2 towerLocation)
        {
            TargetComponents.Clear();

            var size = Physics2D.CircleCastNonAlloc(towerLocation, (float)towerSettings.Range, Vector2.up, results, float.PositiveInfinity, raycastLayer);
            for (var i = 0; i < size; i++)
            {
                var hit = results[i];
                var towerObject = hit.collider.GetComponent<ITowerObject>();
                if (towerObject is not null && towerObject.Model.Components.HasComponent<IPowerComponent>()) TargetComponents.Add(towerObject.Model.Components.GetComponent<IPowerComponent>());
            }
        }
    }
}