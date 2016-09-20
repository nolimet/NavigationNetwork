using UnityEngine;
using System.Collections.Generic;
using System.Collections;
namespace NavigationNetwork
{
    public class NavigationForcedConnectionNode : NavigationNode
    {
        public List<NavigationNode> Connections;

        protected override void Start()
        {
            base.Start();
            Range = 999999;
        }

        public override void GetInRangeNodes(List<NavigationNode> _nodes)
        {
            base.GetInRangeNodes(Connections);
        }

        public override List<NavigationNode> ReturnInRangeNodes()
        {
            return Connections;
        }

        protected override void SetNameID()
        {
            ID = GetInstanceID();

            name = "Fixed Connection Node : " + ID.ToString();
        }
    }
}