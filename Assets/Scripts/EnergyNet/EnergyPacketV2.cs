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

        public Transform targetTransform;
        public EnergyNode currentTargetNode;
        public EnergyNode finalTargetNode;

        private float journeyLength;
        private float startTime;
        public float speed = 0.1f;

        [SerializeField]
        List<EnergyNode> TargetList = new List<EnergyNode>();
        int index = 0;

        void Start()
        {
            name = "EnergyPacket from " + SenderID + " To " + TargetID;
            GetComponent<ParticleSystem>().emissionRate = Energy * 5;
            float temp = (speed / 2f) * Random.value;
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
            bool point = false;
            int maxHoops = 240;
            while (!point)
            {
                if (currentTargetNode.SenderList.Count == 0)
                    return;

                if (currentTargetNode.SenderList.Count > 0)
                    currentTargetNode = currentTargetNode.SenderList[Mathf.FloorToInt(new System.Random().Next(currentTargetNode.SenderList.Count - 1))];
                else
                    currentTargetNode = currentTargetNode.SenderList[0];

                TargetList.Add(currentTargetNode);

                if (currentTargetNode.endPoint || maxHoops <= 0)
                {
                    finalTargetNode = currentTargetNode;
                    point = true;
                }
                maxHoops--;
            }

            currentTargetNode = TargetList[0];
            targetTransform = currentTargetNode.transform;
        }
        /// <summary>
        ///  used to set the first target the packet wil move to
        /// </summary>
        public void SentTo(Transform newNode, float _Energy, int currentNodeID, int _TargetID)
        {
            targetTransform = newNode;
            currentTargetNode = newNode.gameObject.GetComponent<EnergyNode>();
            SenderID = currentNodeID;
            Energy = _Energy;
            GetComponent<ParticleSystem>().emissionRate = Energy * 5;
            TargetID = _TargetID;

            journeyLength = Vector3.Distance(transform.position, targetTransform.position);
            startTime = Time.time;
            GetSendList();

            currentTargetNode = null;
            targetTransform = newNode;
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
                        if (TargetList.Contains(currentTargetNode))
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
            if (col.collider.tag == EnergyTags.EnergyNode)
            {
                EnergyNode node = col.gameObject.GetComponent<EnergyNode>();
                //Debug.Log("HitID: " + targetNode.ID + " LookingFor: " + TargetID);
                if (node == finalTargetNode)
                {
                    currentTargetNode.receive(Energy, SenderID);
                    Destroy(gameObject, 3f);
                    Destroy(this);
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(GetComponent<Collider>());
                    GetComponent<ParticleSystem>().emissionRate = 0;
                    name = "EnergyPacket Empty";
                    Energy = 0;
                }
            }
        }
    }
}
