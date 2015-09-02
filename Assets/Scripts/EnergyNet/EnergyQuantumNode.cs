using UnityEngine;
using System.Collections;

namespace EnergyNet
{
    public class EnergyQuantumNode : EnergyNode
    {

        public bool IsSender = false;
        public EnergyQuantumNode TwinNode;
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
            this.name = "QuantumNode " + ID;
            if (IsSender && TwinNode != null)
            {
                name += " Sender to " + TwinNode.ID;
                endPoint = true;
                nonRecivend = false;
                TwinNode.MaxStorage = MaxStorage;
                TwinNode.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
            }
            else
            {
                name += " QuantumNode Reciever";
                endPoint = false;
                nonRecivend = true;
            }
        }

        public override void sendPower()
        {
            
            if (!IsSender)
            {
                base.sendPower();
                return;
            }
            if (Storage >= MaxStorage)
            {
                EnergyGlobals.SendPackage(transform, TwinNode.transform, ID, TwinNode.ID, Mathf.FloorToInt( Storage),0.05f,true);
                Storage = 0;
            }
        }

        protected override void Update()
        {
            base.Update();
            if (Storage < MaxStorage / 0.8f)
            {
                maxLight = (8f / MaxStorage) * Storage;
                if (maxLight == maxLightLast)
                {
                    lightStart = GetComponent<Light>().intensity;
                    pointLight.intensity = Mathf.Lerp(lightStart, maxLight, Time.time / 5f);
                }
                particleRate = Storage * 0.8f;
                particles.emissionRate = particleRate;

            }
            
        }

        public override void GetInRangeNodes(System.Collections.Generic.List<EnergyNode> _nodes)
        {
            base.GetInRangeNodes(_nodes);

        }
            
    }
}