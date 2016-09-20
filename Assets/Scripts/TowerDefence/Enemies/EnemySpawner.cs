using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace TowerDefence.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        public List<Utils.SpawnAbleEnemy> SpawnAbleEnemies;
        private Utils.SpawnAbleEnemy currentEnemy;

        public Utils.Wave currentWave;

        List<NavigationNetwork.NavigationBase> NavRoute;

        int currentIndex;

        [SerializeField]
        private NavigationNetwork.NavigationNode FirstNode;
        [SerializeField]
        private NavigationNetwork.NavigationNode LastNode;

        float spawnDelayTimer;

        void Start()
        {
            NavRoute = new List<NavigationNetwork.NavigationBase>();

            foreach(Utils.SpawnAbleEnemy s in SpawnAbleEnemies)
            {
                s.AutoName();
            }
            Invoke("UpdateNavRoute", 2f);
           // UpdateNavRoute();
           
            //TODO remove DEbug Stuff;
            string json = JsonUtility.ToJson(currentWave, true);
            Debug.Log(json);
            SetNewWave(currentWave);

        }

        void SetNewWave(Utils.Wave newWave)
        {
            currentIndex = -1;
            currentWave = newWave;
        }

        void UpdateNavRoute()
        {
            BaseEnemy e = SpawnAbleEnemies[0].gameObject.GetComponent<BaseEnemy>();
            e.currentTargetNode = FirstNode;
            e.TargetID = LastNode.ID;

            NavRoute = e.GetPath();
        }

        void Update()
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
                        spawnDelayTimer = currentWave.groups[currentIndex].SpawnDelay;
                        SpawnEnemy(currentEnemy);
                    }
                    spawnDelayTimer -= Time.deltaTime;
                }
            }
        }

        void SpawnEnemy(Utils.SpawnAbleEnemy enemyPrefab)
        {
            GameObject g = Instantiate(enemyPrefab.gameObject) as GameObject;
            g.SetActive(true);
            g.transform.position = transform.position;
            BaseEnemy e = g.GetComponent<BaseEnemy>();

            e.speed = 5f;
            e.SetPath(NavRoute.ToList());
            e.currentTargetNode = NavRoute[0];
        }
    }
}