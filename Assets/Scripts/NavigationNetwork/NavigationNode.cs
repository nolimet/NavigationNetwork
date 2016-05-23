﻿using UnityEngine;
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
            if (isEndNode)
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
            if (isEndNode || nonRecivend)
            {
                return;
            }
            //TODO rework pull getting method to make it more efficient.
            float Distance;

            // if (Pull == null)
            // {
            Pull = new Dictionary<int, structs.NavPullObject>();
            //}

            foreach (NavigationNode n in nodes)
            {
                if (!n.isEndNode && n.Pull != null && n.Pull.Count > 0) 
                {
                    foreach (KeyValuePair<int, structs.NavPullObject> keypair in n.Pull)
                    {
                        Distance = Vector3.Distance(position, n.position);

                        if (!Pull.ContainsKey(keypair.Key))
                        {
                            Pull.Add(keypair.Key, new structs.NavPullObject());
                            Pull[keypair.Key] = new structs.NavPullObject(n, keypair.Value.Distance + Distance);
                        }

                        if (keypair.Value.Distance < Pull[keypair.Key].Distance - Distance && keypair.Value.ClosestNode != n)
                        {
                            Pull[keypair.Key] = new structs.NavPullObject(n, Distance + keypair.Value.Distance);
                        }
                    }
                }
                else if(n.isEndNode)
                {
                    if (!Pull.ContainsKey(n.ID))
                    {
                        Pull.Add(n.ID, new structs.NavPullObject(n, Vector3.Distance(position, n.position)));
                    }
                    else
                    {
                        Pull[n.ID] = new structs.NavPullObject(n, Vector3.Distance(position, n.position));
                    }
                }
            }

            //Dictionary<int, structs.NavPullObject> temp = new Dictionary<int, structs.NavPullObject>(Pull);

            //foreach(KeyValuePair<int, structs.NavPullObject> keypair in temp)
            //{
            //    Pull[keypair.Key] = new structs.NavPullObject(keypair.Value.ClosestNode, Vector3.Distance(position, keypair.Value.ClosestNode.position) + keypair.Value.Distance);
            //}

        }

#if UNITY_EDITOR
        //Debug Viewer for connections
        protected override void Update()
        {
            base.Update();

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
        }
#endif
        protected override void SetNameID()
        {
            base.SetNameID();
            if (!isEndNode)
            {
                name = "Node " + ID;
            }
            else
            {
                name = "EndNode " + ID;
            }
        }

    }
}
