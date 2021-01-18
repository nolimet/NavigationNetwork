using UnityEngine;
using System.Collections;
namespace NavigationNetwork.Build
{
    public class Builder : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particels;

        public enum Type {
            node,
            genarator,
            endPoint
        }

        public void create(Type T)
        {
            switch (T)
            {
                case Type.node:
                    Instantiate(Resources.Load("BuildObjects/Node"),transform.position,Quaternion.identity);
                    break;

                case Type.genarator:
                    Instantiate(Resources.Load("BuildObjects/Genarator"), transform.position, Quaternion.identity);
                    break;

                case Type.endPoint:
                     GameObject g = (GameObject)Instantiate(Resources.Load("BuildObjects/EndPoint"), transform.position, Quaternion.identity);
                     NavigationNetwork.NavigationNode n = g.GetComponent<NavigationNetwork.NavigationNode>();
                    n.isEndNode = true;
                    break;
            }

            particels.Emit(300);
        }
    }
}