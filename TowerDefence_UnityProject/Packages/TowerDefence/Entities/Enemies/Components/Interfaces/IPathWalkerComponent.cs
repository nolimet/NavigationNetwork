namespace TowerDefence.Entities.Enemies.Components
{
    public interface IPathWalkerComponent : ITickableEnemyComponent
    {
        float PathProgress { get; }
    }
}