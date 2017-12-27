using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence
{
    public class AOETower : BaseTower
    {
        protected override void Start()
        {
            base.Start();
            _type = "Area of Effect Tower";
            _range = 3f;
            _damage = 4f;
        }

        
        List<Enemies.BaseEnemy> Effected,Removed;

        public override void IUpdate()
        {
            CircleCast();
            if (Effected == null)
                Effected = new List<TowerDefence.Enemies.BaseEnemy>();

            if (Effected.Count() == 0 && Enemies == null)
                return;
            
            Removed = new List<TowerDefence.Enemies.BaseEnemy>();

            foreach (Enemies.BaseEnemy e in Effected)
            {
                if (!Enemies.Contains(e))
                {
                    Removed.Add(e);
                }
            }

            foreach (Enemies.BaseEnemy e in Enemies)
            {
                if (!Effected.Contains(e))
                {
                    e.speedEffector -= 0.05f * _damage;
                    Effected.Add(e);
                }
            }

            foreach (Enemies.BaseEnemy e in Removed)
            {
                e.speedEffector += 0.05f * _damage;
                Effected.Remove(e);
            }
        }
    }
}
