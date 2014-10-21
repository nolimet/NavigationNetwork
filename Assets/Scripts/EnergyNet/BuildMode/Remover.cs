using UnityEngine;
using System.Collections;
using EnergyNet;
namespace EnergyNet.Build
{
    public class Remover : MonoBehaviour
    {
        [SerializeField]
        GameObject Target;

        [SerializeField]
        Rect posButton;
        [SerializeField]
        ParticleSystem particle;

        void Start()
        {
            posButton.xMin = Screen.width - posButton.xMax;
            posButton.yMin = 40 + posButton.yMax;
        }

        void OnTriggerEnter(Collider col)
        {
            string cTag = col.collider.gameObject.tag;
            Debug.Log("TEST");
            if (cTag == EnergyTags.EnergyGenartor || cTag == EnergyTags.EnergyNode)
            {
                Target = col.collider.gameObject;
            }
        }

        void OnGUI()
        {
            if (Target != null)
            {
                if (GUI.Button(new Rect(posButton.x,posButton.y,150,20), "Destory " + Target.tag))
                {
                    Destroy(Target);
                    Target = null;
                    particle.Emit(500);
                }
            }
        }
    }
}