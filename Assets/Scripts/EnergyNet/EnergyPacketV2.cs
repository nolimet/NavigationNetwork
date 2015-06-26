using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace EnergyNet
{
    public class EnergyPacketV2 : MonoBehaviour
    {

        private float Energy = 0;
        public int SenderID;
        public int TargetID;
        public Transform target;
        private Vector3 startPos;
        private float journeyLength;
        private float startTime;
        public float speed = 0.1f;

        List<EnergyNode> TargetList = new List<EnergyNode>();
        int index = 0;

        void Start()
        {
            name = "EnergyPacket from " + SenderID + " To " + TargetID;
			particleSystem.emissionRate = Energy*5;
            float temp = (speed / 2f)*Random.value;
            speed = (speed / 2f) + temp;
            if (Energy == 0)
            {
                Destroy(this.gameObject);
            }

            EnergyNetWorkControler.OnRebuild += EnergyNetWorkControler_OnRebuild;
		}

        void EnergyNetWorkControler_OnRebuild()
        {
            TargetList = new List<EnergyNode>();
            GetSendList();
            index = 0;
        }

        void OnDestroy() { EnergyNetWorkControler.OnRebuild -= EnergyNetWorkControler_OnRebuild; }


        /// <summary>
        ///  made a list of nodes it will visted. It will only look over 120 nodes in total to avoid a infinite loop when it can't find an end node
        /// </summary>
        public void GetSendList()
        {
            EnergyNode node = target.gameObject.GetComponent<EnergyNode>();
            bool point = false;
            int maxHoops = 120;
            while(!point)
            {
                if (node.SenderList.Count == 0)
                    return;

                if (node.SenderList.Count > 0)
                    node = node.SenderList[Mathf.FloorToInt(Random.Range(0, node.SenderList.Count))];
                else
                    node = node.SenderList[0];

                TargetList.Add(node);

                if (node.endPoint || maxHoops <= 0)
                    point = true;
                maxHoops--;
            }
            
        }
        /// <summary>
        ///  used to set the first target the packet wil move to
        /// </summary>
        public void SentTo(Transform newNode, float _Energy, int currentNodeID,int _TargetID)
        {
            target = newNode;
            SenderID = currentNodeID;
            startPos = transform.position;
            Energy = _Energy;
            particleSystem.emissionRate = Energy*5;
            TargetID = _TargetID;
            journeyLength = Vector3.Distance(startPos, target.position);
            startTime = Time.time;
            GetSendList();
        }

        /// <summary>
        ///  move to current Target
        /// </summary>
        void Update()
        {
            if (target != null)
            {
                float distCovered = (Time.time - startTime) * speed;
                float fracJourney = distCovered / journeyLength;
                transform.position=Vector3.Lerp(transform.position, target.position, fracJourney);
            }
        }

        void OnCollisionEnter(Collision col)
        {
           // Debug.Log("test");
            if (col.collider.tag == EnergyTags.EnergyNode)
            {
                EnergyNode node = col.gameObject.GetComponent<EnergyNode>();
                //Debug.Log("HitID: " + node.ID + " LookingFor: " + TargetID);
                if (node.ID == TargetID)
                {
                    if (index < TargetList.Count)
                    {
                        target = TargetList[index].transform;
                        TargetID = TargetList[index].ID;
                        index++;

                        startTime = Time.time;
                        journeyLength = Vector3.Distance(startPos, target.position);
                    }
                    else
                    {
                        node.receive(Energy, SenderID);
                        Destroy(gameObject, 3f);
                        Destroy(this);
                        Destroy(rigidbody);
                        Destroy(collider);
                        particleSystem.emissionRate = 0;
                        name = "EnergyPacket Empty";
                        Energy = 0;
                    }
                }
            }
        }
    }
}
