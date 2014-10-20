using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace EnergyNet
{
    public class EnergyPathFinder : MonoBehaviour
    {

        Transform currentTarget;
        EnergyBase origen;
        List<EnergyBase> visted;

        void Update()
        {

        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == EnergyTags.EnergyNode)
            {

            }
        }
    }
}