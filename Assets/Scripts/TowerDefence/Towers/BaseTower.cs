using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TowerDefence
{
    public class BaseTower : MonoBehaviour
    {
        int fireRate;
        int Damage;
        float range;

        public Target_Priority PriorityMode = Target_Priority.Closest;

        RaycastHit2D[] hits;
        BaseEnemy[] targets;
        int length;
        // Update is called once per frame
        void Update()
        {
            length = Physics2D.CircleCastNonAlloc(transform.position, range, Vector2.zero, hits);
            if (length > 0)
            {
                targets = new BaseEnemy[length];
                for (int i = 0; i < length; i++)
                {
                    if (hits[i].transform.tag == TagManager.Enemy)
                    {
                        targets[i] = hits[i].transform.gameObject.GetComponent<BaseEnemy>();
                    }
                }
            }
        }

        public void FindClosestTarget()
        {

        }

        public void GetFirstEntered()
        {

        }

        public void GetStrongest()
        {

        }

        public void GetWeakest()
        {

        }
    }
}