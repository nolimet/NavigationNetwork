using TowerDefence.Entities.Enemies.Models;

namespace TowerDefence.Entities.Enemies
{
    public interface IEnemyObject : IEntityObjectBase
    {
        IEnemyModel Model { get; }

        public float DistanceToTarget();

        public void Damage(double damage);
    }
}