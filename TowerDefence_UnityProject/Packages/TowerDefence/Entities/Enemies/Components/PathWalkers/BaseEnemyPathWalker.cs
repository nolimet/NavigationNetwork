using System.Collections.Generic;
using DataBinding;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Enemies.Components.Interfaces;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine.Events;

namespace TowerDefence.Entities.Enemies.Components
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class BaseEnemyPathWalker : IPathWalkerComponent, IInitializable
    {
        protected readonly BindingContext Context = new();
        public UnityAction<IEnemyObject> ReachedEnd { get; set; }

        protected IEnemyObject Self { get; private set; }
        protected IEnemyModel Model { get; private set; }
        public abstract float PathProgress { get; protected set; }
        public short TickPriority => short.MinValue;

        public virtual void PostInit(IEnemyObject enemyObject, IEnemyModel enemyModel)
        {
            Self = enemyObject;
            Model = enemyModel;

            Context.Bind(enemyModel, x => x.Components, OnComponentsChanged);
        }

        protected virtual void OnComponentsChanged(IList<IComponent> obj)
        {
        }

        public abstract void Tick();

        ~BaseEnemyPathWalker()
        {
            Context.Dispose();
        }
    }
}