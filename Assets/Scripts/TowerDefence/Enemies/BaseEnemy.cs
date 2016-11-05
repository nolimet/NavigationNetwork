using UnityEngine;
using System.Collections;

namespace TowerDefence.Enemies
{
    public class BaseEnemy : NavigationNetwork.NavigatorV2
    {
        #region Enemy Stats
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
        #endregion
        public string displayName { get; protected set; }
        public string typeName { get; protected set; }
        [SerializeField]
        string _editorTypeName = "base";
        const string defaultName = "Basic Enemy";
        protected void Awake()
        {
            typeName = _editorTypeName;
        }
        protected override void setName()
        {
            name = defaultName;
        }
        protected override void Start()
        {
            base.Start();
            health = MaxHealth;
            virtualHealth = health;
            displayName = defaultName;
        }
    }
}