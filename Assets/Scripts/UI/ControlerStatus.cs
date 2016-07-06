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
        private RectTransform GridUpdateTimeGauge;
        private Vector2 GridUpdateTimeGaugeOrignalSize;

        [SerializeField]
        private NavigationNetwork.NavigationNetworkControler controler;

        private float MaxUpdateTime, UpdateDisplayTime;

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

            GridUpdateTimeGaugeOrignalSize = GridUpdateTimeGauge.sizeDelta;
        }

        void Update()
        {
            TickPerSecond.text = controler.TicksPerSecond.ToString();
            NumberOfNodes.text = NavigationNetwork.NavUtil.CurrentNetworkObjects.ToString();
            GridBuildTime.text = controler.GridBuildTime.ToString();
            MultiThreadingEnabled.text = controler.ThreadActive.ToString();

            if (controler.GridUpdateTime != 0)
            {
                if (controler.GridUpdateTime > MaxUpdateTime)
                {
                    MaxUpdateTime = controler.GridUpdateTime;
                }

                UpdateDisplayTime = controler.GridUpdateTime;

                GridUpdateTime.text = controler.GridUpdateTime.ToString();
                
            }

           

            if (controler.GridUpdateTime < UpdateDisplayTime)
            {
                UpdateDisplayTime -= MaxUpdateTime * (Time.deltaTime / 2f);

                if (UpdateDisplayTime < 0.01f)
                {
                    UpdateDisplayTime = 0;

                }
            }

            if (UpdateDisplayTime != 0)
                GridUpdateTimeGauge.sizeDelta = new Vector2(GridUpdateTimeGaugeOrignalSize.x * (UpdateDisplayTime /MaxUpdateTime ), GridUpdateTimeGaugeOrignalSize.y);
            else
                GridUpdateTimeGauge.sizeDelta = new Vector2(0, GridUpdateTimeGaugeOrignalSize.y);
        }
    }
}