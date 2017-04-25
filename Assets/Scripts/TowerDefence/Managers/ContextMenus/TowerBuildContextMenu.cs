using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
using TowerDefence.Utils;
namespace TowerDefence.Managers.ContextMenus
{
    public class TowerBuildContextMenu : MonoBehaviour
    {
       
        [SerializeField]
        private List<buttonInformation> ButtonData;

        [SerializeField]
        private GameObject buttonTemplate;

        void Start()
        {
            GameObject g;
            for (int l = 0; l < ButtonData.Count; l++)
            {
                g = Instantiate(buttonTemplate);
                g.SetActive(true);
                g.transform.SetParent(transform,false);
                g.transform.localScale = Vector3.one;

                g.name = ButtonData[l].type.ToString();

                ButtonData[l] = g.GetComponent<TowerBuyButton>().setup(ButtonData[l]);
            }
        }
    }
}