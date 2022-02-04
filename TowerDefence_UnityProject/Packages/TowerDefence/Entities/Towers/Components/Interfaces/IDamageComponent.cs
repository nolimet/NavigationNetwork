namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface IDamageComponent : ITickableTowerComponent
    {
        double DamagePerSecond { get; }
    }
}