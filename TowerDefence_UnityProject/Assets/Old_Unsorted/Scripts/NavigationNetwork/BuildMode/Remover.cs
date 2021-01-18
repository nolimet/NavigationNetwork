using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NavigationNetwork;
namespace NavigationNetwork.Build
{
    public class Remover : MonoBehaviour
    {
        [SerializeField]
        Rect posButton;
        [SerializeField]
        ParticleSystem particle;

        List<GameObject> targets = new List<GameObject>();

        void Start()
        {
            posButton.x = Screen.width - posButton.width -60;
            posButton.y = 40 + posButton.height;
        }

        void Update()
        {
            targets = new List<GameObject>();
            foreach (GameObject g in NavUtil.NetWorkObjects)
            {
                if (Vector3.Distance(transform.position, g.transform.position) < 2)
                {
                    targets.Add(g);
                }
            }
        }

        void RemoveObjects()
        {
            foreach (GameObject g in targets)
            {
                if (Vector3.Distance(transform.position, g.transform.position) < 2)
                {
                    Destroy(g);
                }
            }
        }

        void OnGUI()
        {
            if (GUI.Button(new Rect(posButton.x, posButton.y, 150, 20), "Remove items inRange"))
            {
                RemoveObjects();
                particle.Emit(500);
            }
        }
    }
}