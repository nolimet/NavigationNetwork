using TowerDefence.Entities.Enemies.Models;
using UnityEngine.Events;

namespace TowerDefence.Entities.Enemies
{
    public interface IEnemyObject : IEntityObjectBase
    {
        IEnemyModel Model { get; }
        public event UnityAction<IEnemyObject> DeathAction;

        public float DistanceToTarget();

        public void Damage(double damage);
    }
}