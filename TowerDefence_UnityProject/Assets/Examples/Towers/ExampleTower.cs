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
    public class ExampleTower : TowerBase
    {
        public override float TargetRadius => 10f;

        public override void Tick()
        {
            Debug.Log("TICK");
            var enemies = GetWalkersInRange<EnemyBase>();
            foreach (var enemy in enemies)
            {
                enemy.ApplyDamage(1);
            }
        }
    }
}