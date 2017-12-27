using UnityEngine;
using System.Collections;

namespace TowerDefence.Enemies
{
    public class BaseEnemy : NavigationNetwork.NavigatorV2
    {
        #region Enemy Stats
        public float MaxHealth;
        public float DistLeft;

        public bool CanBeHit
        {
            get
            {
                return (invisibltyDistance < 0f) && isActiveAndEnabled;
            }
        }

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
            if (invisibltyDistance >= 0)
                Debug.Log("STILL INVISI!" + name);
        }
        #endregion
        public string displayName { get; protected set; }
        public string typeName { get { return _editorTypeName; } }
        [SerializeField]
        string _editorTypeName = "base";
        const string defaultName = "Basic Enemy";

        public int resourceDropQuantity = 4;
        float _speedEffector;
        /// <summary>
        /// don't use to edit speed using tower. It's the base speed of a enemy;
        /// </summary>
        public float baseSpeed;
        /// <summary>
        /// Changes how fast a enemy can move. 
        /// A enemy will never go slower than 5% of it's orignal speed.
        /// Or 5times faster than it;
        /// 
        /// 1 is normal speed
        /// </summary>
        public float speedEffector
        {
            get { return _speedEffector; }
            set
            {
                _speedEffector = value;
                speed = baseSpeed * Mathf.Clamp(_speedEffector,  0.05f, 5f); 
            }
        }
        Vector2 lastPos;
        float invisibltyDistance;
        protected override void setName()
        {
            name = displayName + " " + GetInstanceID();
        }
        
        public void Reset()
        {
            health = MaxHealth;
            virtualHealth = MaxHealth;
            invisibltyDistance = 1.5f;
            lastPos = transform.position;
            speedEffector = 1f;
        }

        protected override void Start()
        {
            base.Start();
            health = MaxHealth;
            virtualHealth = health;
            displayName = defaultName;
            speedEffector = 1f;
        }

        public override void IUpdate()
        {
            base.IUpdate();
            DistLeft = GetPathLengthLeft();

            if (health <= 0)
            {
                ObjectPools.EnemyPool.RemoveObj(this);

                Util.Update.UpdateManager.removeUpdateAble(this);
            }                

            if (invisibltyDistance>=0f)
            {
                invisibltyDistance -= Vector2.Distance(lastPos, transform.position);
            }
            lastPos = transform.position;

        }
    }
}