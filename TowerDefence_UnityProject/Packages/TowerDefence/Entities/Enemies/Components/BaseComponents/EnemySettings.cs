using TowerDefence.Entities.Components;

namespace TowerDefence.Entities.Enemies.Components.BaseComponents
{
    [System.Serializable]
    internal class EnemySettings : IComponent
    {
        public readonly string TypeName;
        public readonly string BaseName;

        public readonly double MaxHealth;
        public readonly double Speed;
    }
}