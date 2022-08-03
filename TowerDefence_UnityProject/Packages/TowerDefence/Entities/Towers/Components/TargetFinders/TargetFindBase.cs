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
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class TargetFindBase : ITargetFindComponent, IInitializable
    {
        protected List<IEnemyObject> targetList { get; private set; }
        protected BindingContext bindingContext { get; private set; }

        protected ITowerObject towerObject { get; private set; }
        protected ITowerModel towerModel { get; private set; }

        public IEnumerable<IEnemyObject> FoundTargets => targetList;

        public short TickPriority => short.MinValue;

        private TowerSettings towerSettings;

        public abstract void Tick();

        public virtual void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            targetList ??= new();
            bindingContext ??= new(true);

            this.towerObject = towerObject;
            this.towerModel = model;

            bindingContext.Bind(model, m => m.Components, OnComponentsChanged);
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

        ~TargetFindBase()
        {
            bindingContext.Dispose();
        }
    }
}