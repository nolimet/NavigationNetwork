﻿using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.TargetFinders
{
    [Serializable]
    public abstract class TargetFindBase : ITargetFindComponent
    {
        [NonSerialized] protected readonly List<IEnemyObject> targetList = new();
        protected ITowerObject towerObject { get; private set; }
        protected ITowerModel towerModel { get; private set; }

        public IEnumerable<IEnemyObject> FoundTargets => targetList;

        public short TickPriority => short.MinValue;

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