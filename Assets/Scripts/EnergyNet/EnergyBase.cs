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
        public int Pull = 0;
        public int ID;
        public bool nonRecivend = false;

        [SerializeField]
        public bool StaticPull = false;
        protected Color NodeColor;
        protected List<EnergyNode> nodes = new List<EnergyNode>();
        protected EnergyNetWorkControler controler;
        public int connections;

        protected virtual void Start()
        {
            controler = EnergyNetWorkControler.GetThis();
            transform.parent = controler.gameObject.transform;
            if (controler = null)
            {
                Destroy(this);
            }

            EnergyGlobals.AddnewObject(gameObject);
            NodeColor = DebugGrid.randomColor();
            if (renderer != null)
            {
                renderer.material.color = NodeColor;
            }
        }

        public virtual void GetInRangeNodes(List<EnergyNode>_nodes)
        {
            if (ID == 0)
            {
                ID = Mathf.FloorToInt(Random.Range(0, 10000000));
            }
            nodes = new List<EnergyNode>();
            foreach (EnergyNode go in _nodes)
            {
                if (go != null && go != this.gameObject)
                {
                    float dist = Vector3.Distance(go.transform.position, transform.position);

                    if (dist >= 0 && dist < Range)
                    {
                        nodes.Add(go.gameObject.GetComponent<EnergyNode>());
                    }
                }
            }
        }

       protected virtual void Update()
        {
            if (!nonRecivend)
            {
                int l = nodes.Count;
                for (int i = 0; i < l; i++)
                {
                    if (nodes[i]!=null&&!nodes[i].nonRecivend)
                        Debug.DrawLine(transform.position, nodes[i].transform.position, Color.yellow);
                }
            }
        }

       protected virtual void SetNameID()
       {
           ID = Mathf.FloorToInt(Random.Range(0, 10000000));
       }
    }
}
