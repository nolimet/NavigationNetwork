namespace TowerDefence.Entities.Towers.Components
{
    public interface ITickableTowerComponent : ITowerComponent
    {
        /// <summary>
        /// The lower the value the higher the priorty use any value between -32,768 to 32,767
        /// </summary>
        short TickPriority { get; }

        void Tick();
    }
}