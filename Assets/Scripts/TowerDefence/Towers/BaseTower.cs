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

        public Target_Priority TargetMode = Target_Priority.Closest;

        protected RaycastHit2D[] hits;
        protected BaseEnemy[] Enemies;

        int length;

        protected Transform Target;

        protected virtual void Update()
        {
            length = Physics2D.CircleCastNonAlloc(transform.position, range, Vector2.zero, hits);
            if (length > 0)
            {
                FillTargetsList();
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

        #region targeting
        protected virtual void FillTargetsList()
        {
            if (TargetMode == Target_Priority.Strongest || TargetMode == Target_Priority.Weakest)
            {
                Enemies = new BaseEnemy[length];
                for (int i = 0; i < length; i++)
                {
                    if (hits[i].transform.tag == TagManager.Enemy)
                    {
                        Enemies[i] = hits[i].transform.gameObject.GetComponent<BaseEnemy>();
                    }
                }
            }
        }

        protected virtual void FindClosestTarget()
        {
            RaycastHit2D closest = hits[0];
            foreach(RaycastHit2D hit in hits)
            {
                if(hit.distance<closest.distance)
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

        protected virtual void FireWeapon()
        {
            //TODO write code for FireWeapon
        }

        protected virtual void RotateToTarget()
        {
            
        }
    }
}