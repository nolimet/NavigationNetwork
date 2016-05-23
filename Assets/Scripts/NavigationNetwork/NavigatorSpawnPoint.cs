using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NavigationNetwork
{
    public class NavigatorSpawnPoint : NavigationBase
    {
        public bool activated = true;
        private int waitedTicks;
        private int controlerTPS;

        protected override void Start()
        {
            base.Start();
            name = "Spawner: " + ID;

            NavigationNetworkControler.OnPowerSend += sendPower;

            controlerTPS = NavUtil.MaxTPS;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            NavigationNetworkControler.OnPowerSend -= sendPower;
        }

        public override void GetInRangeNodes(List<NavigationNode> _nodes)
        {
            base.GetInRangeNodes(_nodes);
        }

        public void sendPower()
        {
            waitedTicks++;
            if (waitedTicks >= controlerTPS)
            {
                waitedTicks = 0;
                int l = nodes.Count;
                for (int i = 0; i < l; i++)
                {
                    NavUtil.SendNavigatorV2(this, nodes[i]);
                }
            }
        }
    }
}