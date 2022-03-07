using TowerDefence.Entities.Enemies.Components.Interfaces;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine.Events;

namespace TowerDefence.Entities.Enemies.Components
{
    public abstract class BaseEnemyPathWalker : IPathWalkerComponent, IInitializable
    {
        protected readonly UnityAction<IEnemyObject> reachedEnd;
        protected IEnemyObject self { get; private set; }
        protected IEnemyModel model { get; private set; }

        protected BaseEnemyPathWalker(UnityAction<IEnemyObject> reachedEnd)
        {
            this.reachedEnd = reachedEnd;
        }

        public short TickPriority => short.MinValue;

        public abstract float PathProgress { get; protected set; }

        public void PostInit(IEnemyObject enemyObject, IEnemyModel enemyModel)
        {
            self = enemyObject;
            model = enemyModel;
        }
        public abstract void Tick();
    }
}