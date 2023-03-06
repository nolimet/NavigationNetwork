using System;
using System.Collections.Generic;
using System.Linq;
using DataBinding;
using Newtonsoft.Json;
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
        protected List<IEnemyObject> TargetList { get; private set; }
        protected BindingContext BindingContext { get; private set; }

        protected ITowerObject TowerObject { get; private set; }
        protected ITowerModel TowerModel { get; private set; }

        public IEnumerable<IEnemyObject> FoundTargets => TargetList;

        public short TickPriority => short.MinValue;

        private TowerSettings towerSettings;

        public abstract void Tick();

        public virtual void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            TargetList ??= new List<IEnemyObject>();
            BindingContext ??= new BindingContext();

            TowerObject = towerObject;
            TowerModel = model;

            BindingContext.Bind(model, m => m.Components, OnComponentsChanged);
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
            var hits = Physics2D.CircleCastAll(TowerObject.GetWorldPosition(), (float)towerSettings.Range, Vector2.up);
            return hits.Length > 0 ? hits.Select(x => x.collider.GetComponent<IEnemyObject>()).Where(x => x != null) : Array.Empty<IEnemyObject>();
        }

        ~TargetFindBase()
        {
            BindingContext.Dispose();
        }
    }
}