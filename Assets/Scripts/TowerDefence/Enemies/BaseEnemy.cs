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
            //TODO Write calc for damage using armor
           health -= Damage;
            if (health < 0)
                Destroy(gameObject);
        }

        protected override void Start()
        {
            base.Start();
            health = 10f;
            armor = 0f;
        }
    }
}