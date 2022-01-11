using TowerDefence.Entities.Enemies.Models;

namespace TowerDefence.Entities.Enemies
{
    public interface IEnemyObject : IEntityObjectBase
    {
        IEnemyModel EnemyModel { get; }

        public float DistanceToTarget();
    }
}