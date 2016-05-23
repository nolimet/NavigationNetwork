using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace NavigationNetwork
{
    /// <summary>
    ///  Globals for the energy system.
    /// </summary>
    public class NavUtil : MonoBehaviour
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

            NavigatorV1 packetScript = energyPacket.GetComponent<NavigatorV1>();
            packetScript.speed = Speed;
            packetScript.SentTo(target, Energy, senderID, targetID);
        }

       /// <summary>
       /// Create a naviator that will navigate it's way throug the network
       /// </summary>
       /// <param name="Sender">The Object that send this obj</param>
       /// <param name="Target">The target it move towards</param>
       /// <param name="Speed">The speed it will move at</param>
       /// <returns>Was it succesfull in finding a route</returns>
        public static bool SendNavigatorV2(NavigationBase Sender, NavigationBase Target, float Speed = 0.4f)
        {
            GameObject energyPacket;
            energyPacket = Instantiate(Resources.Load("EnergyPacketV2"), Sender.transform.position, Quaternion.identity) as GameObject;

            if (packageParent != null)
                energyPacket.transform.parent = packageParent;

            NavigatorV2 packetScript = energyPacket.GetComponent<NavigatorV2>();
            packetScript.speed = Speed;

            //Create route and destory self id unable to find route
            if (!packetScript.SentTo(Sender, Target.ID))
            {
                Destroy(packetScript.gameObject);
                return false;
            }
            return true;
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
