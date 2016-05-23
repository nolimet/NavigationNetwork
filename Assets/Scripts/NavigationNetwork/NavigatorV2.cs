using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NavigationNetwork
{
    public class NavigatorV2 : MonoBehaviour
    {
        public int SenderID;
        public int TargetID;

        public Transform targetTransform;
        public NavigationBase currentTargetNode;
        public NavigationBase finalTargetNode;

        private float journeyLength;
        private float startTime;
        public float speed = 0.1f;

        [SerializeField]
        List<NavigationBase> TargetList = new List<NavigationBase>();
        int index = 0;

        void Start()
        {
            name = "EnergyPacket from " + SenderID + " To " + TargetID;
            float temp = (speed / 2f) * Random.value;
            speed = (speed / 2f) + temp;

            NavigationNetworkControler.OnRebuild += EnergyNetWorkControler_OnRebuild;
        }

        void EnergyNetWorkControler_OnRebuild()
        {
            TargetList = new List<NavigationBase>();
            GetSendList();
            index = 0;
        }

        void OnDestroy() { NavigationNetworkControler.OnRebuild -= EnergyNetWorkControler_OnRebuild; }


        /// <summary>
        ///  made a list of nodes it will visted. It will only look over 120 nodes in total to avoid a infinite loop when it can't find an end node
        /// </summary>
        public void GetSendList()
        {

            if (!currentTargetNode || currentTargetNode.Pull == null || currentTargetNode.Pull.Count == 0) 
                return;

            bool point = false;
            int maxHoops = 240;

            TargetID = currentTargetNode.Pull.ElementAt(new System.Random().Next(currentTargetNode.Pull.Count)).Key;
            TargetList.Add(currentTargetNode);
            while (!point)
            {
                currentTargetNode = currentTargetNode.Pull[TargetID].ClosestNode;

                TargetList.Add(currentTargetNode);

                //add node to nodes to moveto


                //if the currentnode is a end node then stop the movement or if it went through it's maxium number of search nodes
                if (currentTargetNode.isEndNode || maxHoops <= 0)
                {
                    finalTargetNode = currentTargetNode;
                    point = true;
                }
                maxHoops--;
            }

            //set currentarget to the first one it found
            currentTargetNode = TargetList[0];
            targetTransform = currentTargetNode.transform;
        }

        /// <summary>
        ///  used to set the first target the packet wil move to
        /// </summary>
        public bool SentTo(NavigationBase startNode)
        {
            targetTransform = startNode.transform;
            currentTargetNode = startNode.gameObject.GetComponent<NavigationNode>();
            SenderID = startNode.ID;       

            journeyLength = Vector3.Distance(transform.position, targetTransform.position);
            startTime = Time.time;
            GetSendList();

            if (finalTargetNode == null)
                return false;

            currentTargetNode = null;
            return true;
        }

        /// <summary>
        ///  move to current Target
        /// </summary>
        void Update()
        {
            if (targetTransform != null)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(transform.position, targetTransform.position, fracJourney);

                if (Vector3.Distance(transform.position, targetTransform.position) < 0.1f)
                {

                    if (TargetList.Count > 0)
                    {
                        if (currentTargetNode && TargetList.Contains(currentTargetNode))
                        {
                            TargetList.Remove(currentTargetNode);
                        }

                        currentTargetNode = TargetList[0];
                        targetTransform = currentTargetNode.transform;
                        startTime = Time.time;

                        journeyLength = Vector3.Distance(transform.position, targetTransform.position);
                    }
                }
            }


        }

        void OnCollisionEnter(Collision col)
        {
            if (col.collider.tag == NavTags.EnergyNode)
            {
                NavigationNode node = col.gameObject.GetComponent<NavigationNode>();
                if (node == finalTargetNode)
                {
                    Destroy(gameObject, 3f);
                    Destroy(this);
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(GetComponent<Collider>());
                    name = "EnergyPacket Empty";
                }
            }
        }
    }
}
