using DataBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies.Components.Interfaces;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine.Events;

namespace TowerDefence.Entities.Enemies.Components
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BaseEnemyPathWalker : IPathWalkerComponent, IInitializable
    {
        protected readonly BindingContext context = new(true);
        public UnityAction<IEnemyObject> ReachedEnd { get; set; }

        protected IEnemyObject self { get; private set; }
        protected IEnemyModel model { get; private set; }
        public abstract float PathProgress { get; protected set; }
        public short TickPriority => short.MinValue;

        public virtual void PostInit(IEnemyObject enemyObject, IEnemyModel enemyModel)
        {
            self = enemyObject;
            model = enemyModel;

            context.Bind(enemyModel, x => x.Components, OnComponentsChanged);
        }

        protected virtual void OnComponentsChanged(IList<IComponent> obj) { }
        public abstract void Tick();

        ~BaseEnemyPathWalker()
        {
            context.Dispose();
        }
    }
}