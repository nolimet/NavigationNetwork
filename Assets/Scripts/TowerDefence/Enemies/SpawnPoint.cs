using System.Collections;
using System.Collections.Generic;
using Util;
using UnityEngine;

namespace TowerDefence.Enemies
{
    public class SpawnPoint : MonoBehaviour
    {
        public NavigationNetwork.NavigationNode first;
        public NavigationNetwork.NavigationNode last;

        public List<NavigationNetwork.NavigationBase> path;
        public Utils.WaveSpawnGroup spawnGroup;

        public float spawnCoolDown = 0;
        public Managers.PathBuilderHelperClass pathBuilderData;

        void Start()
        {
            pathBuilderData = GetComponent<Managers.PathBuilderHelperClass>();
        }

        public void Spawn()
        {
           BaseEnemy e = ObjectPools.EnemyPool.GetObj(spawnGroup.enemy);
            e.transform.position = transform.position;
            e.SetPath(new List<NavigationNetwork.NavigationBase>(path));
            e.speed = spawnGroup.Speed;
            
            e.finalTargetNode = last;
            e.currentTargetNode = first;

            e.gameObject.SetActive(true);

            spawnCoolDown = spawnGroup.SpawnDelay;
            spawnGroup.spawnAmount--;
        }

        public void _Update()
        {
            if (spawnGroup.spawnAmount > 0)
            {
                if (spawnCoolDown <= 0)
                {
                    Spawn();
                }
                spawnCoolDown -= Time.deltaTime;
            }
        }
    }
}