using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefence
{
    public class BaseTower : MonoBehaviour
    {
        [SerializeField]
        protected Transform FirePoint;

        [SerializeField]
        protected int fireRate;
        [SerializeField]
        protected int Damage;
        [SerializeField]
        protected float range;
        [SerializeField]
        protected float rotationOffset;

        public Target_Priority TargetMode = Target_Priority.Closest;

        protected List<BaseEnemy> Enemies;

        int length;
        int mask;

        protected Transform Target;

        protected virtual void Start()
        {
            mask = LayerMask.GetMask(new string[] { LayerTagManager.Enemy });
            Enemies = new List<BaseEnemy>();

            GetComponent<CircleCollider2D>().radius = range;
            GetComponent<CircleCollider2D>().isTrigger = true;
        }

        protected virtual void Update()
        {
            length = Enemies.Count;

            if (length > 0)
            {
                CheckTargets();
                switch (TargetMode)
                {
                    case Target_Priority.Closest:
                        FindClosestTarget();
                        break;

                    case Target_Priority.First:
                        GetFirstEntered();
                        break;

                    case Target_Priority.Last:
                        break;

                    case Target_Priority.Strongest:
                        FindStrongest();
                        break;

                    case Target_Priority.Weakest:
                        FindWeakest();
                        break;
                }

                RotateToTarget();
                FireWeapon();
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == TagManager.Enemy)
            {
                if (!Enemies.Contains(collision.GetComponent<BaseEnemy>()))
                {
                    Enemies.Add(collision.GetComponent<BaseEnemy>());
                }
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == TagManager.Enemy)
            {
                if (Enemies.Contains(collision.GetComponent<BaseEnemy>()))
                {
                    Enemies.Remove(collision.GetComponent<BaseEnemy>());
                }
            }
        }

        #region targeting
        protected virtual void CheckTargets()
        {
            for (int i = length - 1; i >= 0; i--)
            {
                if (!Enemies[i] || Enemies[i] == null)
                {
                    Enemies.RemoveAt(i);
                }
            }
            Debug.Log(Enemies.Count);
        }

        protected virtual void FindClosestTarget()
        {
            BaseEnemy closest = Enemies[0];
            float dist = Vector2.Distance(transform.position, closest.transform.position);
            foreach (BaseEnemy hit in Enemies)
            {
                if (dist > Vector2.Distance(transform.position, hit.transform.position))
                {
                    closest = hit;
                }
            }
            Target = closest.transform;
        }

        protected virtual void GetFirstEntered()
        {
            //TODO write code for GetFirst entered
        }

        protected virtual void GetLastEntered()
        {
            //TODO write code for GetLastEntered
        }

        protected virtual void FindStrongest()
        {
            //TODO write code for FindStrongest
        }

        protected virtual void FindWeakest()
        {
            //TODO write code for FindWeakest
        }
        #endregion

        #region TowerUpgrades

        public virtual void UpgradeRange(float newRange)
        {

        }

        public virtual void UpgradeDamage(float newDamage)
        {

        }

        public virtual void UpgradeFireRate(float newFireRate)
        {

        }

        #endregion

        protected virtual void FireWeapon()
        {
            
        }

        protected virtual void RotateToTarget()
        {
            transform.rotation = Quaternion.Euler(0, 0, Util.Common.VectorToAngle(transform.position - Target.position) + rotationOffset);
        }
    }
}