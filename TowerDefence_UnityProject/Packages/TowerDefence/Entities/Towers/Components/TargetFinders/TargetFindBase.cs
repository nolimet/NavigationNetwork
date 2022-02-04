using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    [Serializable]
    public abstract class TargetFindBase : ITargetFindComponent, IInitializableComponent
    {
        [NonSerialized, JsonIgnore] protected readonly List<IEnemyObject> targetList = new();
        [JsonIgnore] protected ITowerObject towerObject { get; private set; }
        [JsonIgnore] protected ITowerModel towerModel { get; private set; }

        [JsonIgnore] public IEnumerable<IEnemyObject> FoundTargets => targetList;

        [JsonIgnore] public short TickPriority => short.MinValue;

        public abstract void Tick();

        public virtual void PostInit(ITowerObject towerObject, ITowerModel towerModel)
        {
            this.towerObject = towerObject;
            this.towerModel = towerModel;
        }

        protected IEnumerable<IEnemyObject> GetEnemyObjectsInRange()
        {
            var hits = Physics2D.CircleCastAll(towerObject.GetWorldPosition(), (float)towerModel.Range, Vector2.up);
            if (hits.Length > 0)
            {
                return hits.Select(x => x.collider.GetComponent<IEnemyObject>()).Where(x => x != null);
            }
            return Array.Empty<IEnemyObject>();
        }
    }
}