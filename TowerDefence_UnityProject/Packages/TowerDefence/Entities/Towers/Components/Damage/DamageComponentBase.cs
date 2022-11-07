using System;
using System.Collections.Generic;
using System.Linq;
using DataBinding;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.TargetFinders;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class DamageComponentBase : IDamageComponent, IInitializable
    {
        public abstract event Action<IEnumerable<IEnemyObject>> AppliedDamageToTargets;

        protected readonly BindingContext bindingContext = new();

        protected ITowerModel model { get; private set; }
        public abstract double DamagePerSecond { get; }
        protected ITargetFindComponent targetFindComponent { get; private set; } = NullTargetFinder.Instance;

        public abstract void Tick();

        public virtual void PostInit(ITowerObject towerObject, ITowerModel model)
        {
            targetFindComponent ??= NullTargetFinder.Instance;

            this.model = model;

            bindingContext.Bind(model, x => x.Components, OnComponentsChanged);
        }

        private void OnComponentsChanged(IList<IComponent> obj)
        {
            targetFindComponent = obj.Any(x => x is ITargetFindComponent) ? obj.First(x => x is ITargetFindComponent) as ITargetFindComponent : NullTargetFinder.Instance;
        }

        ~DamageComponentBase()
        {
            bindingContext.Dispose();
        }
    }
}