using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace NavigationNetwork
{
    /// <summary>
    /// Base class for nodes and spawners
    /// </summary>
    public class NavigationBase : MonoBehaviour
    {
        public int Range = 5;
        /// <summary>
        /// int : endpointID
        /// float : distance to that endpoint
        /// </summary>
        public Dictionary<int, structs.NavPullObject> Pull;
        public int ID;
        public bool nonRecivend = false;
        public bool remove;
        public bool isEndNode;
        public string TagName;

        protected Color NodeColor;
        protected List<NavigationNode> nodes = new List<NavigationNode>();
        [SerializeField]
        protected int connections;

        Vector3 _position;
        public Vector3 position { get { return _position; } }

        void Awake()
        {
            SetNameID();

            _position = transform.position;

            Pull = new Dictionary<int, structs.NavPullObject>();
        }



        protected virtual void Start()
        {
            if (NavigationNetworkControler.instance.collectAllNetworkObjects)
            {
                transform.parent = NavigationNetworkControler.instance.gameObject.transform;
            }

            NavUtil.AddnewObject(gameObject);
            NodeColor = Color.green;

            NavigationNetworkControler.OnNetUpdate += GetInRangeNodes;
        }

        protected virtual void OnDestroy()
        {
            NavigationNetworkControler.OnNetUpdate -= GetInRangeNodes;
            NavUtil.RemoveObject(this.gameObject);
        }

        public virtual void GetInRangeNodes(List<NavigationNode> _nodes)
        {

            nodes = new List<NavigationNode>();
            foreach (NavigationNode go in _nodes)
            {
                if (go != null && go != this)
                {
                    float dist = Vector3.Distance(go.position, position);

                    if (dist >= 0 && dist < Range)
                    {
                        nodes.Add(go);
                    }
                }
            }
        }

        protected virtual void Update()
        {
            _position = transform.position;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = NodeColor;
            Gizmos.DrawSphere(transform.position, 0.30f);
        }

        /// <summary>
        ///  setID
        /// </summary>
        protected virtual void SetNameID()
        {
            ID = GetInstanceID();
        }

        public virtual List<NavigationNode> ReturnInRangeNodes()
        {
            return nodes;
        }
    }
}
