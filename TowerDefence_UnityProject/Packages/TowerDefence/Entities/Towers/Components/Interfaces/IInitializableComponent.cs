using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    internal interface IInitializableComponent
    {
        void PostInit(ITowerObject towerObject, ITowerModel towerModel);
    }
}