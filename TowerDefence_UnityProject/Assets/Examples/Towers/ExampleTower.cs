using TowerDefence.Entities.Enemies;
using TowerDefence.World.Towers;

namespace Examples.Towers
{
    public class ExampleTower : SingleTargetTowerBase
    {
        public override float TargetRadius => 4f;

        public override float AttacksPerSecond => 2;

        public override void Tick()
        {
            UpdateTargetList();

            if (CanAttack())
            {
                var target = GetFirst<EnemyBase>();

                if (target)
                {
                    target.ApplyDamage(1);
                }
            }
        }
    }
}