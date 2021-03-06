﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace NavigationNetwork
{
    public class NavigatorV1 : MonoBehaviour
    {

        private float Energy = 0;
        public int SenderID;
        public int TargetID;
        public Transform target;
        private Vector3 startPos;
        private float journeyLength;
        private float startTime;
        public float speed = 0.1f;

        void Start()
        {
            name = "EnergyPacket from " + SenderID + " To " + TargetID;
            float temp = (speed / 2f)*Random.value;
            speed = (speed / 2f) + temp;
            if (Energy == 0)
            {
                Destroy(this.gameObject);
            }
		}

        public void SentTo(Transform newNode, float _Energy, int currentNodeID,int _TargetID)
        {
            target = newNode;
            SenderID = currentNodeID;
            startPos = transform.position;
            Energy = _Energy;
            TargetID = _TargetID;
            journeyLength = Vector3.Distance(startPos, target.position);
            startTime = Time.time;
            
        }

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
            if (col.collider.tag == NavTags.EnergyNode)
            {
                NavigationNode node = col.gameObject.GetComponent<NavigationNode>();
                //Debug.Log("HitID: " + node.ID + " LookingFor: " + TargetID);
                if (node.ID == TargetID)
                {
                    Destroy(gameObject,3f);
                    Destroy(this);
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(GetComponent<Collider>());
                    name = "EnergyPacket Empty";
                    Energy = 0;
                }
            }
        }
    }
}
