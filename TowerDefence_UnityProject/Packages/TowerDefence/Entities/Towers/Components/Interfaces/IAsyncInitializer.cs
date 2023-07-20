using System.Threading.Tasks;
using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    internal interface IAsyncInitializer
    {
        Task AsyncPostInit(ITowerObject towerObject, ITowerModel model);
    }
}