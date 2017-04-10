using UnityEngine;
using System.Collections;

namespace TowerDefence.Enemies
{
    public class BaseEnemy : NavigationNetwork.NavigatorV2
    {
        #region Enemy Stats
        public float MaxHealth;
        public float DistLeft;

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
                ObjectPools.EnemyPool.RemoveObj(this);
        }

        public void TakeVirtualDamage(float Damage)
        {
            virtualHealth -= Damage;
        }
        #endregion
        public string displayName { get; protected set; }
        public string typeName { get { return _editorTypeName; } }
        [SerializeField]
        string _editorTypeName = "base";
        const string defaultName = "Basic Enemy";

        public int resourceDropQuantity = 4;
        protected override void setName()
        {
            name = displayName + " " + GetInstanceID();
        }

        public void Reset()
        {
            health = MaxHealth;
            virtualHealth = MaxHealth;
        }

        protected override void Start()
        {
            base.Start();
            health = MaxHealth;
            virtualHealth = health;
            displayName = defaultName;
        }

        protected override void Update()
        {
            base.Update();
            DistLeft = GetPathLengthLeft();

            if (health <= 0)
                ObjectPools.EnemyPool.RemoveObj(this);
        }
    }
}