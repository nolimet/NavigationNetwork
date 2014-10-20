using UnityEngine;
using System.Collections;
namespace EnergyNet
{
    public class f3dHitRay : MonoBehaviour
    {

        private string printName = "[3DHitRay] ";

        private GameObject currentSelected;
        private EnergyBase currentScript;

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
                if (Physics.Raycast(ray, out hit, 100) && hit.collider.tag == EnergyTags.EnergyNode)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
                  //  print(printName);
                   // print(printName+hit.point+'\n'+printName+hit.collider.name);
                    InGameUI.transform.position = hit.point + new Vector3(0, 0.5f, 0);
                    EnergyNode node = hit.collider.gameObject.GetComponent<EnergyNode>();
                    currentScript = node;
                    currentSelected = hit.collider.gameObject;
                }
            }

            if (currentScript != null && currentSelected != null)
            {
                IGUITM.text = "StorageMax: " + currentScript.MaxStorage.ToString() + '\n' + "Storage: " + currentScript.Storage.ToString() + '\n' + "Pull: " + currentScript.Pull.ToString() + '\n' + currentScript.ToString();
            }
        }

        void log(string mess)
        {
            print(printName+mess);
        }
    }
}
