using System.Collections.Generic;
using System.Threading.Tasks;
using DataBinding;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Utility;
using IComponent = TowerDefence.Entities.Components.IComponent;

namespace TowerDefence.Entities.Towers.Components.AttackVisualizers
{
    public abstract class BaseAttackVisualizer : IAttackVisualizer, IAsyncInitializer
    {
        protected IDamageComponent DamageComponent;
        protected ITowerObject TowerObject;
        private readonly BindingContext bindingContext = new ();
        
        public void Dispose()
        {
            if (DamageComponent is not null)
            {
                DamageComponent.AppliedDamageToTargets -= OnTargetsDamaged;
            }
            
            bindingContext.Dispose();
        }
        
        public virtual Task AsyncPostInit(ITowerObject towerObject, ITowerModel towerModel)
        {
            TowerObject = towerObject;
            bindingContext.Bind(towerModel, x=>x.Components, OnComponentsChanged);
            return  Task.CompletedTask;
        }

        private void OnComponentsChanged(IList<IComponent> obj)
        {
            if (DamageComponent is not null)
            {
                DamageComponent.AppliedDamageToTargets -= OnTargetsDamaged;
            }
            
            if (obj.TryGetComponent(out DamageComponent))
            {
                DamageComponent.AppliedDamageToTargets += OnTargetsDamaged;
            }
        }

        protected abstract void OnTargetsDamaged(IEnumerable<IEnemyObject> targets);
    }
}