using DataBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers.Components.Damage
{
    [Serializable]
    public abstract class DamageComponentBase : IDamageComponent, IInitializable
    {
        [JsonIgnore] protected ITowerModel model { get; private set; }
        [JsonIgnore] protected ITargetFindComponent targetFindComponent { get; private set; }
        [JsonIgnore] public abstract double DamagePerSecond { get; }

        [NonSerialized] protected readonly BindingContext bindingContext = new(true);

        public abstract void Tick();

        public virtual void PostInit(ITowerObject towerObject, ITowerModel towerModel)
        {
            this.model = model;
            bindingContext.Bind(model, x => x.Components, OnComponentsChanged);
        }

        private void OnComponentsChanged(IList<IComponent> obj)
        {
            targetFindComponent = obj.Any(x => x is ITargetFindComponent) ? obj.First(x => x is ITargetFindComponent) as ITargetFindComponent : null;
        }

        ~DamageComponentBase()
        {
            bindingContext.Dispose();
        }
    }
}