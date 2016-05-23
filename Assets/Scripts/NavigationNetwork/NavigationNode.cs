using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace NavigationNetwork
{
    public class NavigationNode : NavigationBase
    {
        private int waitedTicks;
        private int controlerTPS = NavUtil.MaxTPS;
        public List<NavigationNode> SenderList = new List<NavigationNode>();

        private List<int> RevievedID = new List<int>();

        protected override void Start()
        {
            base.Start();
            SetNameID();
            if (endNode)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }

            

           // EnergyNetWorkControler.OnPowerSend += sendPower;
            NavigationNetworkControler.OnPullUpdate += getPull;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
           // EnergyNetWorkControler.OnPowerSend -= sendPower;
            NavigationNetworkControler.OnPullUpdate -= getPull;
        }

        public override void GetInRangeNodes(List<NavigationNode> _nodes)
        {
            base.GetInRangeNodes(_nodes);
        }


        public virtual void getPull()
        {
            if (endNode && !nonRecivend) 
            {
                structs.NavPullObject pullObj;
                //TODO rework pull getting method to make it more efficient.

                if(Pull == null)
                {
                    Pull = new Dictionary<int, structs.NavPullObject>();
                }

                foreach (NavigationNode n in nodes)
                {
                    if (n.Pull != null)
                    {
                        foreach (KeyValuePair<int, structs.NavPullObject> keypair in n.Pull)
                        {
                            if (!Pull.ContainsKey(keypair.Key))
                            {
                                Pull.Add(keypair.Key, new structs.NavPullObject());
                            }

                            pullObj = Pull[keypair.Key];

                            if (!pullObj.ClosestNode)
                            {

                            }
                        }
                    }
                }
            }
        }

        protected override void Update()
        {
            base.Update();
#if UNITY_EDITOR
            if (!nonRecivend)
            {
                try
                {
                    for (int i = 0; i < nodes.Count; i++)
                    {
                        if (nodes[i] != null && !nodes[i].nonRecivend && !RevievedID.Contains(nodes[i].ID))
                            Debug.DrawLine(position, nodes[i].position, Color.yellow);
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }

            }
            #endif
        }

        protected override void SetNameID()
        {
            base.SetNameID();
            this.name = "Node " + ID;
        }

    }
}
