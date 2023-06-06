using TowerDefence.Entities.Components;

namespace TowerDefence.UI.Game.Tower.Properties.Interfaces
{
    public interface ITowerProperty
    {
        string GetValue(IComponent component);
    }
}