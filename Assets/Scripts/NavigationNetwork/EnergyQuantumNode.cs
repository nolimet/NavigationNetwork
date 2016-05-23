using UnityEngine;
using System.Collections;

namespace NavigationNetwork
{
    public class NavigationTeleporter : NavigationNode
    {
        //TODO this requires a complete rework
        public bool IsSender = false;
        public NavigationTeleporter TwinNode;
        public Vector3 RotSpeed;
        public ParticleSystem particles;
        public Light pointLight;
        private float particleRate = 0;
        private float maxLight = 0;
        private float lightStart = 0f;
        private float maxLightLast = -1f;

        protected override void Start()
        {
            base.Start();
            this.name = "TeleportNode " + ID;
            if (IsSender && TwinNode != null)
            {
                name += " Sender to " + TwinNode.ID;
                nonRecivend = false;
                TwinNode.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
            }
            else
            {
                name += " QuantumNode Reciever";
                nonRecivend = true;
            }
        }

        public override void GetInRangeNodes(System.Collections.Generic.List<NavigationNode> _nodes)
        {
            base.GetInRangeNodes(_nodes);

        }
            
    }
}