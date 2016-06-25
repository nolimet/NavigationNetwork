using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace BuildSystem
{
    public class ControlerStatus : MonoBehaviour
    {

        [SerializeField]
        private Text TickPerSecond, NumberOfNodes, MultiThreadingEnabled,
            CortinuesEnabled, GridBuildTime, GridUpdateTime;

        [SerializeField]
        private NavigationNetwork.NavigationNetworkControler controler;

        void Start()
        {
            if (!controler)
            {
                controler = FindObjectOfType<NavigationNetwork.NavigationNetworkControler>();
            }

            if (!controler)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        void Update()
        {
            TickPerSecond.text = controler.TicksPerSecond.ToString();
            NumberOfNodes.text = NavigationNetwork.NavUtil.CurrentNetworkObjects.ToString();
            GridBuildTime.text = controler.GridBuildTime.ToString();
            GridUpdateTime.text = controler.GridUpdateTime.ToString();
        }
    }
}