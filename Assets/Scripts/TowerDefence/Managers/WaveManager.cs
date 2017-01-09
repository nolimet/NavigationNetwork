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
        public static WaveManager instance
        {
            get
            {
                if (_instance)
                    return _instance;
                _instance = FindObjectOfType<WaveManager>();
                if (_instance)
                    return _instance;

                Debug.LogError("NO WaveManager FOUND! Check what is calling it");
                return null;
            }
        }
        static WaveManager _instance;

        [SerializeField]
        List<SpawnPoint> SpawnPoints;
        [SerializeField]
        Wave currentWave;

        /// <summary>
        /// Path  the end point you wane go to
        /// [Start point, End point]
        /// </summary>
        [SerializeField]
        List<NavigationBase>[] NavRoute;

        void Awake()
        {
            GameManager.instance.onLoadLevel += Instance_onLoadLevel;
            GameManager.instance.onStartWave += Instance_onStartWave;
        }

        private void Instance_onLoadLevel()
        {
            SpawnPoints = new List<SpawnPoint>();

            NavRoute = new List<NavigationBase>[0];
        }

        private void Instance_onStartWave()
        {           
            currentWave = GameManager.currentLevel.waves[GameManager.currentWave];

            if (NavRoute.Length == 0)
            {
                UpdateNavRoute();
            }
        }

        public void UpdateNavRoute()
        {
            if (SpawnPoints == null || SpawnPoints.Count == 0)
            {
                SpawnPoints = FindObjectsOfType<Enemies.SpawnPoint>().ToList();
                SpawnPoints = SpawnPoints.Where(x => x.isActiveAndEnabled == true).ToList(); //to filter out and disabled spawnpoints;
            }

            int l = SpawnPoints.Count;
            NavRoute = new List<NavigationBase>[l];

            BaseEnemy e = ObjectPools.EnemyPool.GetObj("base");
            for (int i = 0; i < l; i++)
            {
                e.currentTargetNode = SpawnPoints[i].first;
                e.TargetID = SpawnPoints[i].last.ID;

                SpawnPoints[i].path = e.GetPath();
            }
        }

        public void setupSpawnPoints()
        {

        }

        void Update()
        {
            if (!GameManager.isPaused)
            {
                foreach(SpawnPoint p in SpawnPoints)
                {
                    p._Update();
                }
            }
        }

    }
}