using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class BaseEnemy : NavigationNetwork.NavigatorV2
    {
        public float Health
        {
            get
            {
                return health;
            }
        }
        private float health;
        
        public float Armor
        {
            get
            {
                return armor;
            }
        }
        private float armor;

        public void TakeDamage(float Damage)
        {

        }
    }
}