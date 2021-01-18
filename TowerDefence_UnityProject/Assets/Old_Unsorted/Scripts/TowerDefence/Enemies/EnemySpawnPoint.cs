using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace TowerDefence.Enemies
{
    /// <summary>
    /// Old spawn Script
    /// </summary>
    public class EnemySpawnPoint : MonoBehaviour
    {
        public List<Utils.SpawnAbleEnemy> SpawnAbleEnemies;
        private Utils.SpawnAbleEnemy currentEnemy;

        public Utils.Wave currentWave;

        public string TagName;

        List<NavigationNetwork.NavigationBase> NavRoute;

        int currentIndex;

        public NavigationNetwork.NavigationNode FirstNode = null;
        [SerializeField]
        public NavigationNetwork.NavigationNode LastNode = null;

        float spawnDelayTimer;

        void Start()
        {
            NavRoute = new List<NavigationNetwork.NavigationBase>();

            foreach(Utils.SpawnAbleEnemy s in SpawnAbleEnemies)
            {
                s.AutoName();
            }
           Invoke("UpdateNavRoute",2);
            //Debug.Log(json);
            SetNewWave(currentWave);

        }

        void SetNewWave(Utils.Wave newWave)
        {
            currentIndex = -1;
            currentWave = newWave;
        }

        public void UpdateNavRoute()
        {
            BaseEnemy e = SpawnAbleEnemies[0].gameObject.GetComponent<BaseEnemy>();
            e.currentTargetNode = FirstNode;
            e.TargetID = LastNode.ID;

            NavRoute = e.GetPath();
        }

        void f_Update()
        {
            if (NavRoute.Count > 0)
            {
                if (currentIndex < currentWave.groups.Length)
                {
                    if (currentIndex == -1 || currentWave.groups[currentIndex].spawnAmount <= 0)
                    {
                        currentIndex++;
                        if (currentIndex >= currentWave.groups.Length || currentIndex == -1)
                        {
                            return;
                        }
                        else
                        {
                            currentEnemy = SpawnAbleEnemies.Single(x => x.refrenceName == currentWave.groups[currentIndex].enemy);
                        }
                    }


                    if (spawnDelayTimer <= 0)
                    {
                        currentWave.groups[currentIndex].spawnAmount--;
                        spawnDelayTimer = currentWave.groups[currentIndex].SpawnDelay;
                        SpawnEnemy(currentEnemy);
                    }
                    spawnDelayTimer -= Time.deltaTime;
                }
            }
        }

        public void SpawnEnemy(Utils.SpawnAbleEnemy enemyPrefab)
        {
            GameObject g = Instantiate(enemyPrefab.gameObject) as GameObject;
            g.SetActive(true);
            g.transform.position = transform.position;
            BaseEnemy e = g.GetComponent<BaseEnemy>();

            e.baseSpeed = 5f;
            e.speedEffector = 1f;
            e.SetPath(NavRoute.ToList());
            e.currentTargetNode = NavRoute[0];
        }
    }
}