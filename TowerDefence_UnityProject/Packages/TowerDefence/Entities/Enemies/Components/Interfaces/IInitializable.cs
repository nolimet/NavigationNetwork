using TowerDefence.Entities.Enemies.Models;

namespace TowerDefence.Entities.Enemies.Components.Interfaces
{
    internal interface IInitializable
    {
        void PostInit(IEnemyObject EnemyObject, IEnemyModel enemyModel);
    }
}