using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Enemies;
using TowerDefence.Utils;
using NavigationNetwork;
namespace TowerDefence.Managers
{
    /// <summary>
    /// Controls enemy spawns
    /// </summary>
    public class WaveManager : MonoBehaviour
    {
        [SerializeField]
        EnemySpawnPoint[] SpawnPoints;
        [SerializeField]
        NavigationNode[] EndPoints;
        [SerializeField]
        Wave currentWave;

        [SerializeField]
        List<List<NavigationBase>> NavRoute;

        void Awake()
        {
            GameManager.instance.onLoadLevel += Instance_onLoadLevel;
            GameManager.instance.onStartWave += Instance_onStartWave;
        }

        private void Instance_onLoadLevel()
        {
            Invoke("UpdateNavRoute",3f);
        }

        private void Instance_onStartWave()
        {           
            currentWave = GameManager.currentLevel.waves[GameManager.currentWave];      
        }

        public void UpdateNavRoute()
        {
            SpawnPoints = FindObjectsOfType<Enemies.EnemySpawnPoint>();
            SpawnPoints = SpawnPoints.Where(x => x.isActiveAndEnabled == true).ToArray(); //to filter out and disabled spawnpoints;

            EndPoints = FindObjectsOfType<NavigationNetwork.NavigationNode>();
            EndPoints = EndPoints.Where(x => x.isEndNode == true).ToArray();

            int l1 = SpawnPoints.Length;
            int l2 = EndPoints.Length;
            NavRoute = new List<List<NavigationBase>>();

            BaseEnemy e = ObjectPools.EnemyPool.GetObj("base");
            for (int i = 0; i < l1; i++)
            {
                NavRoute.Add(new List<NavigationBase>());
                for (int j = 0; j < l2; j++)
                {
                    e.currentTargetNode = SpawnPoints[i].FirstNode;
                    e.TargetID = EndPoints[j].ID;

                    NavRoute[i] = e.GetPath();
                }
            }
        }

        void Update()
        {
            
        }

    }
}