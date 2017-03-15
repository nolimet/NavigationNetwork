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
        public Utils.WaveSpawnGroup[] spawnGroup;

        public float[] spawnCoolDown;
        public Managers.PathBuilderHelperClass pathBuilderData;

        void Start()
        {
            pathBuilderData = GetComponent<Managers.PathBuilderHelperClass>();
        }

        public void PreWaveInit()
        {
            spawnCoolDown = new float[spawnGroup.Length];
        }

        protected void Spawn(int i)
        {
           BaseEnemy e = ObjectPools.EnemyPool.GetObj(spawnGroup[i].enemy);
            e.transform.position = transform.position;
            e.SetPath(new List<NavigationNetwork.NavigationBase>(path));
            e.speed = spawnGroup[i].Speed;
            
            e.finalTargetNode = last;
            e.currentTargetNode = first;

            e.gameObject.SetActive(true);

            e.Reset();

            spawnCoolDown[i] = spawnGroup[i].SpawnDelay;
            spawnGroup[i].spawnAmount--;
        }
        
        public void _Update()
        {
            for (int i = 0; i < spawnGroup.Length; i++)
            {

                if (spawnGroup[i].StartDelay <= 0 && spawnGroup[i].spawnAmount > 0)
                {
                    if (spawnCoolDown[i] <= 0)
                    {
                        Spawn(i);
                    }
                    spawnCoolDown[i] -= Time.deltaTime;
                }
                else
                {
                    spawnGroup[i].StartDelay -= Time.deltaTime;
                }
            }            
        }
    }
}