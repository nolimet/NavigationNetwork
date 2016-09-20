using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class BaseEnemy : NavigationNetwork.NavigatorV2
    {
        public float MaxHealth;

        public float Health
        {
            get
            {
                return health;
            }
        }
        private float health;

        public float VirtualHealth
        {
            get
            {
                return virtualHealth;
            }
        }
        private float virtualHealth;

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
            if (health <= 0)
                Destroy(gameObject);
        }

        public void TakeVirtualDamage(float Damage)
        {
            virtualHealth -= Damage;
        }

        protected override void Start()
        {
            base.Start();
            health = MaxHealth;
            virtualHealth = health;
            armor = 0f;
        }
    }
}