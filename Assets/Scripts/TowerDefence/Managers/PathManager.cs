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
        GameObject nodePrefab = null, SpawnPointPrefab = null;
        [SerializeField]
        Transform PathParent = null;
        [SerializeField]
        Transform VisualPathParent = null;
        [SerializeField]
        Material PathMaterial = null;

        [SerializeField]
        List<GameObject> _NodeList;
        List<Bounds> _nodeBounds;


        void Awake()
        {
            GameManager.instance.onLoadLevel += GameManager_onLoadLevel;
            instance = this;

            if(VisualPathParent == null)
            {
                VisualPathParent = (new GameObject("VisualPathParent")).transform;
            }
        }

        private void GameManager_onLoadLevel()
        {
            BuildPath();
            BuildBounds();
        }
        void BuildPath()
        {
            _NodeList = new List<GameObject>();
            int l = GameManager.currentLevel.path.nodes.Length;
            PathBuilderHelperClass hlper = null;
            Utils.NodeData nd;
            
            GameObject g = null;

            for (int i = 0; i < l; i++)
            {
                nd = GameManager.currentLevel.path.nodes[i];
                if (nd.NodeType == NodeTypes.Spawnpoint)
                {
                    g = Instantiate(SpawnPointPrefab) as GameObject;
                }
                else if(nd.NodeType == NodeTypes.Regular||nd.NodeType == NodeTypes.Endpoint)
                {
                    g = Instantiate(nodePrefab) as GameObject;
                }

                _NodeList.Add(g);

                g.SetActive(true);

                hlper = g.AddComponent<PathBuilderHelperClass>();
                hlper.nodeData = nd;

                g.transform.position = nd.Location;
                g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, 0);
                g.transform.SetParent(PathParent);
        
                if(nd.NodeType == NodeTypes.Endpoint)
                {
                    g.GetComponent<NavigationNetwork.NavigationForcedConnectionNode>().isEndNode = true;
                }
                
                             
            }

            l = GameManager.currentLevel.path.connections.Length;
            Utils.NodeConnectionData ncd;
            NavigationNetwork.NavigationForcedConnectionNode nodeA, nodeB;
            Enemies.SpawnPoint sp;
            for (int i = 0; i < l; i++)
            {
                ncd = GameManager.currentLevel.path.connections[i];
                hlper = _NodeList[ncd.nodeA].GetComponent<PathBuilderHelperClass>();

                nodeB = _NodeList[ncd.nodeB].GetComponent<NavigationNetwork.NavigationForcedConnectionNode>();
                if (hlper.nodeData.NodeType == NodeTypes.Regular || hlper.nodeData.NodeType == NodeTypes.Endpoint)
                {
                    nodeA = _NodeList[ncd.nodeA].GetComponent<NavigationNetwork.NavigationForcedConnectionNode>();                   
                    nodeA.Connections = new List<NavigationNetwork.NavigationNode> { nodeB };

                    AddVisual(nodeA.transform, nodeB.transform);
                }
                else
                {
                    sp = _NodeList[ncd.nodeA].GetComponent<Enemies.SpawnPoint>();
                    sp.first = nodeB;
                    sp.last = _NodeList[hlper.nodeData.lastNode].GetComponent<NavigationNetwork.NavigationForcedConnectionNode>();

                    AddVisual(sp.transform, nodeB.transform);
                }


            }
        }

        void BuildBounds()
        {
            int l = GameManager.currentLevel.path.connections.Length;

            Utils.NodeConnectionData ncd;

            _nodeBounds = new List<Bounds>();
            for (int i = 0; i < l; i++)
            {
                ncd = GameManager.currentLevel.path.connections[i];
                _nodeBounds.Add(Common.getBounds(new Transform[] { _NodeList[ncd.nodeA].transform, _NodeList[ncd.nodeB].transform }));
            }
        }

        void AddVisual(Transform a, Transform b)
        {
            GameObject g = new GameObject("line");
            g.transform.SetParent(VisualPathParent);
            LineRenderer r = g.AddComponent<LineRenderer>();
            r.useWorldSpace = true;
            r.SetPositions(new Vector3[] { a.position, b.position });

            r.startWidth = 0.5f;
            r.endWidth = 0.5f;

            r.material = PathMaterial;
        }
    }
}