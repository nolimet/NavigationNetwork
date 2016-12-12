using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Util;
namespace TowerDefence.Managers
{
    public class PathManager : MonoBehaviour
    {
        public static List<GameObject> NodeList { get { return instance._NodeList; } }
        public static List<Bounds> NodeBounds { get { return instance._nodeBounds; } }
        static PathManager instance;
        [SerializeField]
        GameObject nodePrefab,SpawnPointPrefab;
        [SerializeField]
        Transform PathParent;

        [SerializeField]
        List<GameObject> _NodeList;
        List<Bounds> _nodeBounds;
        void Awake()
        {
            GameManager.instance.onLoadLevel += GameManager_onLoadLevel;
            instance = this;
        }

        private void GameManager_onLoadLevel()
        {
            BuildPath();
            BuildBounds();
        }

        void BuildPath()
        {
            _NodeList = new List<GameObject>();
            int l = GameManager.currentLevel.path.NodeLocations.Length;
            NavigationNetwork.NavigationForcedConnectionNode first = null;
            NavigationNetwork.NavigationForcedConnectionNode cn1 = null;
            NavigationNetwork.NavigationForcedConnectionNode cn2 = null;
            Enemies.EnemySpawnPoint spawnPoint = null;
            GameObject g = null;

            for (int i = 0; i < l; i++)
            {
                if (!g)
                {

                    g = Instantiate(SpawnPointPrefab) as GameObject;
                    g.SetActive(true);

                    g.transform.position = GameManager.currentLevel.path.NodeLocations[i];
                    g.transform.SetParent(PathParent);

                    spawnPoint = g.GetComponent<Enemies.EnemySpawnPoint>();
                }
                else
                {
                    g = Instantiate(nodePrefab) as GameObject;
                    g.SetActive(true);

                    g.transform.position = GameManager.currentLevel.path.NodeLocations[i];
                    g.transform.SetParent(PathParent);

                    cn1 = g.GetComponent<NavigationNetwork.NavigationForcedConnectionNode>();

                    if (cn2)
                    {
                        cn2.Connections = new System.Collections.Generic.List<NavigationNetwork.NavigationNode> { cn1 };
                    }
                    else
                    {
                        first = cn1;
                    }
                    cn2 = cn1;
                }

                _NodeList.Add(g);               
            }

            if (cn1)
            {
                cn1.isEndNode = true;
            }

            if(spawnPoint)
            {
                spawnPoint.FirstNode = first;
                spawnPoint.LastNode = cn1;
            }
        }

        void BuildBounds()
        {
            int l = _NodeList.Count - 1;
            _nodeBounds = new List<Bounds>();
            for (int i = 0; i < l; i++)
            {
                _nodeBounds.Add(Common.getBounds(new Transform[] { _NodeList[i].transform, _NodeList[i + 1].transform }));
            }
        }
        //while index<lenght
        //add new node and set currentnode to newNode
        //
        //at end loop set current node to Endpoint
    }
}