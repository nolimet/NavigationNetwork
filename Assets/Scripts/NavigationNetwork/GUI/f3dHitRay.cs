using UnityEngine;
using System.Collections;
namespace NavigationNetwork
{
    public class f3dHitRay : MonoBehaviour
    {

        private string printName = "[3DHitRay] ";

        private GameObject currentSelected;
        private NavigationBase currentScript;

        [SerializeField]
        private GameObject InGameUI;
        [SerializeField]
        private TextMesh IGUITM; //InGameUITextMesh

        void Update()
        {
            if(Input.GetKey(KeyCode.Mouse0))
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow,20f);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == NavTags.EnergyNode)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
                  //  print(printName);
                   // print(printName+hit.point+'\n'+printName+hit.collider.name);
                    InGameUI.transform.position = hit.point + new Vector3(0, 0.5f, 0);
                    NavigationNode node = hit.collider.gameObject.GetComponent<NavigationNode>();
                    currentScript = node;
                    currentSelected = hit.collider.gameObject;
                }
            }

            if (currentScript != null && currentSelected != null)
            {
                IGUITM.text ="Pull: " + currentScript.Pull.ToString() + '\n' + currentScript.ToString();
            }
        }

        void log(string mess)
        {
            print(printName+mess);
        }
    }
}
