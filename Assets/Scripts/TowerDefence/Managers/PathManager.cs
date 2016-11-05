using UnityEngine;
using System.Collections;
namespace TowerDefence.Managers
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField]
        GameObject nodePrefab,SpawnPointPrefab;
        [SerializeField]
        Transform PathParent;

        void Awake()
        {
            GameManager.instance.onLoadLevel += GameManager_onLoadLevel; ;
        }

        private void GameManager_onLoadLevel()
        {
            BuildPath();
        }

        void BuildPath()
        {
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

        //while index<lenght
        //add new node and set currentnode to newNode
        //
        //at end loop set current node to Endpoint
    }
}