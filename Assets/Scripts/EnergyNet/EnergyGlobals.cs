using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace EnergyNet
{
    public class EnergyGlobals : MonoBehaviour
    {
        static public bool useLightParticles = true;
        static public float RealTPS = 0;
        public const int MaxTPS = 20;
        static public int LastNetworkObjectCount = -1;
        static public int CurrentNetworkObjects = 0;
        static public List<GameObject> NetWorkObjects = new List<GameObject>();
        static public Transform packageParent = null;
        static public bool createdPackageParent = false;

        public static void SendPackage(Transform sender, Transform target, int senderID, int targetID, int Energy, float Speed = 0.1f, bool forceFancyParticle = false)
        {
            GameObject energyPacket;
            if (forceFancyParticle)
            {
                energyPacket = Instantiate(Resources.Load("EnergyPacket"), sender.position, Quaternion.identity) as GameObject;
            }
            else if (!useLightParticles)
            {
                energyPacket = Instantiate(Resources.Load("EnergyPacket"), sender.position, Quaternion.identity) as GameObject;
            }
            else
            {
                energyPacket = Instantiate(Resources.Load("EnergyPacket_Light"), sender.position, Quaternion.identity) as GameObject;
            }

            if (packageParent != null)
                energyPacket.transform.parent = packageParent;

            EnergyPacket packetScript = energyPacket.GetComponent<EnergyPacket>();
            packetScript.speed = Speed;
            packetScript.SentTo(target, Energy, senderID, targetID);
        }

        public static void AddnewObject(GameObject NewObject)
        {
            NetWorkObjects.Add(NewObject);
            CurrentNetworkObjects++;
        }
    }
}
