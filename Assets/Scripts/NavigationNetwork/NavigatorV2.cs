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

        public NavigationBase currentTargetNode;
        public NavigationBase finalTargetNode;

        private float journeyLength;
        private float startTime;
        public float speed = 3f;

        [SerializeField]
        List<NavigationBase> TargetList = new List<NavigationBase>();

        protected virtual void Start()
        {
            name = "EnergyPacket from " + SenderID + " To " + TargetID;

            NavigationNetworkControler.OnRebuild += EnergyNetWorkControler_OnRebuild;
        }

        protected virtual void EnergyNetWorkControler_OnRebuild()
        {
            TargetList = new List<NavigationBase>();
            GetSendList();
        }

        void OnDestroy() { NavigationNetworkControler.OnRebuild -= EnergyNetWorkControler_OnRebuild; }

        /// <summary>
        ///  The max route will only be to what ever the maxHoops is set. This is to avoid a infite loop in the while loop. It only acts as an exit condition
        /// </summary>
        public virtual void GetSendList()
        {
            if (!currentTargetNode || currentTargetNode.Pull == null || currentTargetNode.Pull.Count == 0)
                return;
            TargetList = GetPath();
            //set currentarget to the first one it found
            currentTargetNode = TargetList[0];
        }

        /// <summary>
        ///  used to set the first target the packet wil move to
        /// </summary>
        public virtual bool SentTo(NavigationBase startNode)
        {
            currentTargetNode = startNode.gameObject.GetComponent<NavigationNode>();
            SenderID = startNode.ID;       

            journeyLength = Vector3.Distance(transform.position, startNode.position);
            startTime = Time.time;
            GetSendList();

            if (finalTargetNode == null)
                return false;

            return true;
        }

        public virtual void SetPath(List<NavigationBase> path)
        {
            TargetList = path;
        }

        public virtual List<NavigationBase> GetPath()
        {
            List<NavigationBase> tmp = new List<NavigationBase>();

            bool point = false;
            int maxHoops = 240;

            TargetID = currentTargetNode.Pull.ElementAt(new System.Random().Next(currentTargetNode.Pull.Count)).Key;
            tmp.Add(currentTargetNode);
            while (!point)
            {
                currentTargetNode = currentTargetNode.Pull[TargetID].ClosestNode;

                tmp.Add(currentTargetNode);

                //add node to nodes to moveto


                //if the currentnode is a end node then stop the movement or if it went through it's maxium number of search nodes
                if (currentTargetNode.isEndNode || maxHoops <= 0)
                {
                    finalTargetNode = currentTargetNode;
                    point = true;
                }
                maxHoops--;
            }

            return tmp;
        }

        /// <summary>
        ///  move to current Target
        /// </summary>
        protected virtual void Update()
        {
            if (currentTargetNode != null)
            {
                //float distCovered = (Time.time - startTime) * speed;
                //float fracJourney = distCovered / journeyLength;
                //transform.position = Vector3.Lerp(transform.position, currentTargetNode.position, fracJourney);

                transform.Translate((currentTargetNode.position - transform.position).normalized * speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, currentTargetNode.position) < 0.1f)
                {

                    if (TargetList.Count > 0)
                    {
                        if (currentTargetNode && TargetList.Contains(currentTargetNode))
                        {
                            TargetList.Remove(currentTargetNode);
                        }

                        currentTargetNode = TargetList[0];
                        startTime = Time.time;

                        journeyLength = Vector3.Distance(transform.position, currentTargetNode.position);
                    }
                }
            }
        }

       protected virtual void OnCollisionEnter(Collision col)
        {
            if (col.collider.tag == NavTags.EnergyNode)
            {
                NavigationNode node = col.gameObject.GetComponent<NavigationNode>();
                if (node == finalTargetNode)
                {
                    Destroy(gameObject);
                    Destroy(this);
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(GetComponent<Collider>());
                    name = "EnergyPacket Empty";
                }
            }
        }
    }
}
