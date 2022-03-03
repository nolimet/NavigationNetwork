using System.Threading.Tasks;
using TowerDefence.Entities.Enemies.Models;

namespace TowerDefence.Entities.Enemies.Components.Interfaces
{
    internal interface IAsyncInitializer
    {
        Task AsyncPostInit(IEnemyObject EnemyObject, IEnemyModel enemyModel);
    }
}