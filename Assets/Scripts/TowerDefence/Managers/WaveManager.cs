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
        public int EnemiesLeft { get { return _EnemiesLeft; } }
        int _EnemiesLeft;
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
            GameManager.instance.onStateChange += Instance_onStartWave;
            ObjectPools.EnemyPool.instance.onRemove += EnemyPool_onRemove;
        }

        private void EnemyPool_onRemove(BaseEnemy item)
        {
            _EnemiesLeft--;
        }

        private void Instance_onLoadLevel()
        {
            SpawnPoints = new List<SpawnPoint>();

            NavRoute = new List<NavigationBase>[0];
        }

        private void Instance_onStartWave(GameState state)
        {
            if (state != GameState.playing)
                return;
            currentWave = GameManager.currentLevel.waves[GameManager.currentWave];

            if (NavRoute.Length == 0)
            {
                UpdateNavRoute();
            }
            string s;
            foreach(SpawnPoint p in SpawnPoints)
            {
                s = p.pathBuilderData.nodeData.nodeTag;
                p.spawnGroup = currentWave.groups.First(x => x.SpawnPointName == s);
            }

            _EnemiesLeft = currentWave.groups.Sum(x => x.spawnAmount);
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

        void Update()
        {
            if (!GameManager.isPaused && GameManager.currentGameState == GameState.playing)
            {
                foreach(SpawnPoint p in SpawnPoints)
                {
                    p._Update();
                }
                if (EnemiesLeft <= 0)
                {
                    GameManager.instance.EndWave();
                }
            }
        }

    }
}