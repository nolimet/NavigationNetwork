namespace TowerDefence.Entities.Towers.Components.Interfaces
{
    public interface ITickableTowerComponent : ITowerComponent
    {
        /// <summary>
        /// The lower the value the higher the priorty use any value between -32,768 to 32,767
        /// </summary>
        public short TickPriority { get => 0; }

        void Tick();
    }
}