using TowerDefence.Entities.Towers.Models;

namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    internal interface IInitializable
    {
        void PostInit(ITowerObject towerObject, ITowerModel towerModel);
    }
}