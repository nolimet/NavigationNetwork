using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace EnergyNet
{
    public class EnergyBase : MonoBehaviour
    {

        public int MaxStorage = 10;
        public float Storage = 0;
        public int Range = 5;
        public float Pull = 0;
        public int ID;
        public bool nonRecivend = false;
        public bool remove;

        [SerializeField]
        public bool StaticPull = false;
        protected Color NodeColor;
        protected List<EnergyNode> nodes = new List<EnergyNode>();
        [SerializeField]
        protected int connections;

        Vector3 _position;
        public Vector3 position { get { return _position; } }

        void Awake()
        {
            ID = Mathf.FloorToInt(Random.Range(0, 10000000));

            _position = transform.position;
        }



        protected virtual void Start()
        {
            transform.parent = EnergyNetWorkControler.instance.gameObject.transform;

            EnergyGlobals.AddnewObject(gameObject);
            NodeColor = Color.green;
            if (GetComponent<Renderer>() != null)
            {
                GetComponent<Renderer>().material.color = NodeColor;
            }

            EnergyNetWorkControler.OnNetUpdate += GetInRangeNodes;
        }

        protected virtual void OnDestroy()
        {
            EnergyNetWorkControler.OnNetUpdate -= GetInRangeNodes;
            EnergyGlobals.RemoveObject(this.gameObject);
        }

        public virtual void GetInRangeNodes(List<EnergyNode> _nodes)
        {
            
            nodes = new List<EnergyNode>();
            foreach (EnergyNode go in _nodes)
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

        /// <summary>
        ///  setID
        /// </summary>
        protected virtual void SetNameID()
        {
            ID = GetInstanceID();
        }

        public virtual List<EnergyNode> ReturnInRangeNodes()
        {
            return nodes;
        }
    }
}
