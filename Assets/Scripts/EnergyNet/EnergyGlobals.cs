using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace EnergyNet
{
    /// <summary>
    ///  Globals for the energy system.
    /// </summary>
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

        /// <summary>
        ///  Legacy Package sende function. It sends a package to a node
        /// </summary>
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
        /// <summary>
        ///  Send a smart package to a endnode. It looks for the endnode and moves to that
        /// </summary>
        public static bool SendPackageV2(Transform sender, Transform target, int senderID, int targetID, int Energy, float Speed = 0.4f, bool forceFancyParticle = false)
        {
            GameObject energyPacket;
            energyPacket = Instantiate(Resources.Load("EnergyPacketV2"), sender.position, Quaternion.identity) as GameObject;

            if (packageParent != null)
                energyPacket.transform.parent = packageParent;

            EnergyPacketV2 packetScript = energyPacket.GetComponent<EnergyPacketV2>();
            packetScript.speed = Speed;
            bool gotFinalNode = packetScript.SentTo(target, Energy, senderID, targetID);
            if (!gotFinalNode)
            {
                Destroy(packetScript.gameObject);
            }
            return gotFinalNode;
        }

        /// <summary>
        ///  Add a new object to the network objects
        /// </summary>
        public static void AddnewObject(GameObject NewObject)
        {
            NetWorkObjects.Add(NewObject);
            CurrentNetworkObjects++;
        }
        /// <summary>
        ///  Remove a object from network objects
        /// </summary>
        public static void RemoveObject(GameObject OldObject)
        {
            NetWorkObjects.Remove(OldObject);
            CurrentNetworkObjects--;
        }
    }
}
