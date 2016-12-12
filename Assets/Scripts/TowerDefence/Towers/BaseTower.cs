using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Util;
using TowerDefence.Enemies;

namespace TowerDefence
{
    public class BaseTower : MonoBehaviour
    {
        /// <summary>
        /// Offset For the context menu on the tower
        /// </summary>
        [HideInInspector]
        public Vector2 ContextMenuOffset = Vector2.zero;

        /// <summary>
        /// Location the bullet is firedfrom
        /// </summary>
        [HideInInspector]
        public Vector2 fireLocation;
        public Vector2 fireWorldPosition { get { return fireLocation.Rotate(transform.rotation) + (Vector2)transform.position; } }

        /// <summary>
        /// projectiles shot per minute
        /// </summary>
        [SerializeField]
        protected float fireRate = 60;
        [SerializeField]
        protected int Damage = 5;
        [SerializeField]
        protected float range = 5;

        [SerializeField]
        protected float rotationOffset;
        [SerializeField]
        protected float shootingOffsetDirection;

        public Target_Priority TargetMode = Target_Priority.Closest;

        protected List<BaseEnemy> Enemies;

        int length;
        int mask;

        protected BaseEnemy Target;

        protected virtual void Start()
        {
            mask = LayerMask.GetMask(new string[] { Managers.LayerTagManager.Enemy });
            Enemies = new List<BaseEnemy>();

            //GetComponent<CircleCollider2D>().radius = range;
            GetComponent<CircleCollider2D>().isTrigger = true;
        }

        protected virtual void Update()
        {
            CircleCast(); 
            length = Enemies.Count;


            if (FindTarget())
            {
                RotateToTarget();
                HasTarget();
            }
        }

        int hitsLength;
        RaycastHit2D[] hits;
        void CircleCast()
        {
            Enemies = new List<BaseEnemy>();
            hits = Physics2D.CircleCastAll(transform.position, range, Vector2.up, range, mask);
            hitsLength = hits.Length;

            for (int i = 0; i < hitsLength; i++)
            {
                if(hits[i].collider.tag == Managers.TagManager.Enemy)
                {
                    if (!Enemies.Contains(hits[i].collider.GetComponent<BaseEnemy>()))
                    {
                        Enemies.Add(hits[i].collider.GetComponent<BaseEnemy>());
                    }
                }
            }
        }

        /// <summary>
        /// Runs each update if there is a target
        /// </summary>
        protected virtual void HasTarget() { }

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
                if (length > 0)
                {
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
                if (!Enemies[i] || Enemies[i] == null || !Enemies[i].isActiveAndEnabled || Enemies[i].VirtualHealth <= 0) 
                {
                    Enemies.RemoveAt(i);
                }
            }
                length = Enemies.Count;
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
            Target = closest;
        }
        /// <summary>
        /// Takes the first target it finds in the list
        /// </summary>
        protected virtual void GetFirstEntered()
        {
            Target = Enemies[0];
        }
        /// <summary>
        /// Take last target it find in the list
        /// </summary>
        protected virtual void GetLastEntered()
        {
            if (Enemies.Count > 0)
            {
                Target = Enemies[Enemies.Count - 1];
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
            Target = closest;
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
            Target = closest;
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
            transform.rotation = Quaternion.Euler(0, 0, Util.Common.VectorToAngle(transform.position - Target.transform.position) + rotationOffset);
        }

#if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            Vector2 fireLocationRotated = fireLocation.Rotate(transform.rotation);

            Gizmos.color = Color.red;
            Vector2 SphereLocation = fireLocationRotated + (Vector2)transform.position;
            Gizmos.DrawSphere(SphereLocation, 0.05f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(((Vector2)transform.position) + fireLocationRotated, ((Vector2)transform.position) + fireLocationRotated + (Util.Common.AngleToVector(shootingOffsetDirection).Rotate(transform.rotation) * 0.5f));

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(ContextMenuOffset + (Vector2)transform.position, 0.05f);
        }
#endif
    }
}