using TowerDefence.Entities.Towers.Components.Interfaces;

namespace TowerDefence.Entities.Towers.Components.PowerComponents
{
    public class SimplePowerConsumer : IPowerConsumer
    {
        public double MaxStored { get; private set; } = 1000;
        public double Stored { get; protected set; }

        public bool TryConsume(double amount)
        {
            if (Stored < amount) return false;
            Stored -= amount;
            return true;
        }

        public double PushPower(double maxAmount)
        {
            if (Stored + maxAmount > MaxStored)
            {
                double added = MaxStored - Stored;
                Stored = MaxStored;
                return added;
            }

            return maxAmount;
        }

        public void Dispose()
        {
        }
    }
}