using DataBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    [Serializable]
    public abstract class TargetFindBase : ITargetFindComponent, IInitializable
    {
        [NonSerialized, JsonIgnore] protected readonly List<IEnemyObject> targetList = new();
        [NonSerialized, JsonIgnore] protected readonly BindingContext bindingContext = new(true);

        [JsonIgnore] protected ITowerObject towerObject { get; private set; }
        [JsonIgnore] protected ITowerModel towerModel { get; private set; }

        [JsonIgnore] public IEnumerable<IEnemyObject> FoundTargets => targetList;

        [JsonIgnore] public short TickPriority => short.MinValue;

        [NonSerialized, JsonIgnore] private TowerSettings towerSettings;

        public abstract void Tick();

        public virtual void PostInit(ITowerObject towerObject, ITowerModel towerModel)
        {
            this.towerObject = towerObject;
            this.towerModel = towerModel;

            bindingContext.Bind(towerModel, m => m.Components, OnComponentsChanged);
        }

        private void OnComponentsChanged(IList<IComponent> components)
        {
            if (components.Any(x => x is TowerSettings))
            {
                towerSettings = components.First(x => x is TowerSettings) as TowerSettings;
            }
        }

        protected IEnumerable<IEnemyObject> GetEnemyObjectsInRange()
        {
            var hits = Physics2D.CircleCastAll(towerObject.GetWorldPosition(), (float)towerSettings.Range, Vector2.up);
            if (hits.Length > 0)
            {
                return hits.Select(x => x.collider.GetComponent<IEnemyObject>()).Where(x => x != null);
            }
            return Array.Empty<IEnemyObject>();
        }
    }
}