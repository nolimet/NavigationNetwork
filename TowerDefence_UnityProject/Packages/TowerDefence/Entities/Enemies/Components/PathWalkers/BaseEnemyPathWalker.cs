using Newtonsoft.Json;
using TowerDefence.Entities.Enemies.Components.Interfaces;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine.Events;

namespace TowerDefence.Entities.Enemies.Components
{
    public abstract class BaseEnemyPathWalker : IPathWalkerComponent, IInitializable
    {
        [JsonIgnore] public UnityAction<IEnemyObject> ReachedEnd { get; set; }

        [JsonIgnore] protected IEnemyObject self { get; private set; }
        [JsonIgnore] protected IEnemyModel model { get; private set; }
        [JsonIgnore] public abstract float PathProgress { get; protected set; }
        [JsonIgnore] public short TickPriority => short.MinValue;

        public void PostInit(IEnemyObject enemyObject, IEnemyModel enemyModel)
        {
            self = enemyObject;
            model = enemyModel;
        }

        public abstract void Tick();
    }
}