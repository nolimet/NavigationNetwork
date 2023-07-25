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
        public IReadOnlyList<(Vector2 worldPosition, IPowerComponent powerComponent)> Targets => TargetComponents;

        [SerializeField] [LayerDropdown] [JsonProperty]
        private int raycastLayer;

        protected readonly List<(Vector2 worldPosition, IPowerComponent powerComponent)> TargetComponents = new();
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

            var hits = Physics2D.CircleCastAll(towerLocation, (float)towerSettings.Range, Vector2.up);
            for (var i = 0; i < hits.Length; i++)
            {
                var hit = hits[i];
                var towerObject = hit.collider.GetComponent<ITowerObject>();
                if (towerObject is null || !towerObject.Model.Components.HasComponent<IPowerComponent>()) continue;
                var component = towerObject.Model.Components.GetComponent<IPowerComponent>();
                if (component.CanReceive)
                    TargetComponents.Add((towerObject.GetWorldPosition(), component));
            }
        }
    }
}