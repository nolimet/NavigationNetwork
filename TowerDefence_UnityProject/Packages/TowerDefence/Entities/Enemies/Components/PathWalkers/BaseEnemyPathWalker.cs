using UnityEngine.Events;

namespace TowerDefence.Entities.Enemies.Components
{
    public abstract class BaseEnemyPathWalker : IPathWalkerComponent
    {
        protected readonly UnityAction<IEnemyObject> reachedEnd;

        protected BaseEnemyPathWalker(UnityAction<IEnemyObject> reachedEnd)
        {
            this.reachedEnd = reachedEnd;
        }

        public short TickPriority => short.MinValue;

        public abstract float PathProgress { get; protected set; }

        public abstract void Tick();
    }
}