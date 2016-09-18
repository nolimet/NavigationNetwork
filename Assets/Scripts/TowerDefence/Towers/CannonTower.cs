using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class CannonTower : BaseTower
    {
        
        protected override void Update()
        {
            base.Update();
            Fire_Update();
        }

        protected virtual void Fire_Update()
        {

        }
    }
}