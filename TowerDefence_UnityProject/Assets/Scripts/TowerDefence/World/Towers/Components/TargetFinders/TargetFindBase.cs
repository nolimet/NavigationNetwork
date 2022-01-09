﻿using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Enemies;
using TowerDefence.World.Towers.Models;
using UnityEngine;

namespace TowerDefence.World.Towers.Components.TargetFinders
{
    public abstract class TargetFindBase : ITargetFindComponent
    {
        protected readonly List<IEnemyObject> targetList;
        protected readonly ITowerObject towerObject;
        protected readonly ITowerModel towerModel;

        protected TargetFindBase(ITowerObject towerObject, ITowerModel towerModel)
        {
            this.towerObject = towerObject;
            this.towerModel = towerModel;
        }

        public IEnumerable<IEnemyObject> FoundTarget => targetList;

        public short TickPriority => short.MinValue;

        public abstract void Tick();

        protected IEnumerable<IEnemyObject> GetEnemyObjectsInRange()
        {
           var hits = Physics2D.CircleCastAll(towerObject.GetWorldPosition(), (float)towerModel.Range, Vector2.up);
            if (hits.Length > 0)
            {
                return hits.Select(x => x.collider.GetComponent<IEnemyObject>()).Where(x=>x!=null);
            }
            return Array.Empty<IEnemyObject>();
        }

    }
}