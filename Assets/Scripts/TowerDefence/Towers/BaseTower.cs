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


            if (FindTarget())
            {
                RotateToTarget();
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
        /// <summary>
        /// Handels the finding of the current target
        /// </summary>
        /// <returns></returns>
        protected virtual bool FindTarget()
        {
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
                        GetLastEntered();
                        break;

                    case Target_Priority.Strongest:
                        FindStrongest();
                        break;

                    case Target_Priority.Weakest:
                        FindWeakest();
                        break;
                }
                return true;
            }

            return false;
        }
        /// <summary>
        /// Checks if Enemies still exist and are alive
        /// </summary>
        protected virtual void CheckTargets()
        {
            for (int i = length - 1; i >= 0; i--)
            {
                if (!Enemies[i] || Enemies[i] == null || !Enemies[i].isActiveAndEnabled)
                {
                    Enemies.RemoveAt(i);
                }
            }
            Debug.Log(Enemies.Count);
        }
        /// <summary>
        /// Takes target closest to the tower
        /// </summary>
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
        /// <summary>
        /// Takes the first target it finds in the list
        /// </summary>
        protected virtual void GetFirstEntered()
        {
            Target = Enemies[0].transform;
        }
        /// <summary>
        /// Take last target it find in the list
        /// </summary>
        protected virtual void GetLastEntered()
        {
            if (Enemies.Count > 0) {
                Target = Enemies[Enemies.Count - 1].transform;
            }
        }
        /// <summary>
        /// Finds the strongest or first encounterd in the list
        /// </summary>
        protected virtual void FindStrongest()
        {
            BaseEnemy closest = Enemies[0];
            foreach (BaseEnemy hit in Enemies)
            {
                if (closest.Health > hit.Health)
                {
                    closest = hit;
                }
            }
            Target = closest.transform;
        }
        /// <summary>
        /// Finds the weakest or first encounterd in the list
        /// </summary>
        protected virtual void FindWeakest()
        {
            BaseEnemy closest = Enemies[0];
            foreach (BaseEnemy hit in Enemies)
            {
                if (closest.Health < hit.Health)
                {
                    closest = hit;
                }
            }
            Target = closest.transform;
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

        /// <summary>
        /// Function rotates front of tower to enemy. Using a off set to correct for any rotation issues
        /// </summary>
        protected virtual void RotateToTarget()
        {
            transform.rotation = Quaternion.Euler(0, 0, Util.Common.VectorToAngle(transform.position - Target.position) + rotationOffset);
        }

        /// <summary>
        /// Runs each update if there is a target
        /// </summary>
        protected virtual void HasTarget()
        {

        }
    }
}