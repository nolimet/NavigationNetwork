using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace NavigationNetwork
{
    public class NavigatorV2 : MonoBehaviour
    {
        public int SenderID;
        public int TargetID;

        public Transform targetTransform;
        public NavigationNode currentTargetNode;
        public NavigationNode finalTargetNode;

        private float journeyLength;
        private float startTime;
        public float speed = 0.1f;

        [SerializeField]
        List<NavigationNode> TargetList = new List<NavigationNode>();
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
            TargetList = new List<NavigationNode>();
            GetSendList();
            index = 0;
        }

        void OnDestroy() { NavigationNetworkControler.OnRebuild -= EnergyNetWorkControler_OnRebuild; }


        /// <summary>
        ///  made a list of nodes it will visted. It will only look over 120 nodes in total to avoid a infinite loop when it can't find an end node
        /// </summary>
        public void GetSendList()
        {
            bool point = false;
            int maxHoops = 240;
            while (!point)
            {
                //check if the current node has any connections
                if (currentTargetNode.SenderList.Count == 0)
                    return;

                ///check again just to be sure and pick a random node to move to
                if (currentTargetNode.SenderList.Count > 0)
                    currentTargetNode = currentTargetNode.SenderList[Mathf.FloorToInt(new System.Random().Next(currentTargetNode.SenderList.Count - 1))];
                //else get the first node it can send to
                else
                    currentTargetNode = currentTargetNode.SenderList[0];

                //add node to nodes to moveto
                TargetList.Add(currentTargetNode);

                //if the currentnode is a end node then stop the movement or if it went through it's maxium number of search nodes
                if (currentTargetNode.endNode || maxHoops <= 0)
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
        public bool SentTo(NavigationBase startNode, int _TargetID)
        {
            targetTransform = startNode.transform;
            currentTargetNode = startNode.gameObject.GetComponent<NavigationNode>();
            SenderID = startNode.ID;
            GetComponent<ParticleSystem>().emissionRate = 10;
            TargetID = _TargetID;

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
            // Debug.Log("test");
            if (col.collider.tag == NavTags.EnergyNode)
            {
                NavigationNode node = col.gameObject.GetComponent<NavigationNode>();
                //Debug.Log("HitID: " + targetNode.ID + " LookingFor: " + TargetID);
                if (node == finalTargetNode)
                {
                    Destroy(gameObject, 3f);
                    Destroy(this);
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(GetComponent<Collider>());
                    GetComponent<ParticleSystem>().emissionRate = 0;
                    name = "EnergyPacket Empty";
                }
            }
        }
    }
}
