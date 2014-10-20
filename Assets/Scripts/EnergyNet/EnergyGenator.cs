using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace EnergyNet
{
    public class EnergyGenator : EnergyBase
    {

        private List<int> RevievedID = new List<int>();
        public int generation = 3;
        public bool activated = true;
        public int transferRate = 5;
        private int waitedTicks;
        private int controlerTPS = EnergyGlobals.MaxTPS;

        public float Fuel = 10f;
        public int maxFuel = 20;
        public float EnergyDensity = 10f;
        public bool useFuel = false;

        protected override void Start()
        {
            base.Start();
            //controler.AddNewGenerator(this);
          //  MaxStorage = 200;
            name = "Energy Genarator: " + ID;

        }

        public override void GetInRangeNodes(List<EnergyNode> _nodes)
        {
            base.GetInRangeNodes(_nodes);

            if (!useFuel)
            {
                Storage = MaxStorage;
            }
        }

        public void Genarate()
        {
            if (useFuel && activated && Storage != MaxStorage && Fuel > 0) 
            {
                float fuelperEnergy = 1f / EnergyDensity;
                if (MaxStorage - generation < Storage)
                {
                    Fuel -= fuelperEnergy * (MaxStorage - Storage);
                    Storage = MaxStorage;
                }
                else
                {
                    Fuel -= generation * fuelperEnergy;
                    Storage += generation;
                }
            }
        }

        public void sendPower()
        {
            waitedTicks++;
            if (waitedTicks >= controlerTPS)
            {
                waitedTicks = 0;
                if (useFuel && Storage > 0 && transferRate > 0 || !useFuel && activated && Storage > 0 && transferRate > 0)
                {
                    //check receiver's storage
                    int l = nodes.Count;
                    for (int i = 0; i < l; i++)
                    {
                        if (Storage >= transferRate)
                        {
                            EnergyGlobals.SendPackage(transform, nodes[i].transform, ID, nodes[i].ID, transferRate);
                                Storage -= transferRate;
                        }
                    }
                }
            }
        }
    }
}