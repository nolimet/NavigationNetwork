using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies;
using TowerDefence.World.Path;
using TowerDefence.World.Towers;
using UnityEngine;

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
                    Debug.Log(target);
                    target.ApplyDamage(1);
                }
            }
        }
    }
}