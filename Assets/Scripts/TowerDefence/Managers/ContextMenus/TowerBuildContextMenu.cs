using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
namespace TowerDefence.Managers.ContextMenus
{
    public class TowerBuildContextMenu : MonoBehaviour
    {
        [System.Serializable]
        private struct buttonInformation
        {
            public Towers type;
            public Sprite icon;

            [ReadOnly]
            public Image img;
            [ReadOnly]
            public Text price, name;
        }
        [SerializeField]
        private List<buttonInformation> ButtonData;

        private Button[] buttons;

        [SerializeField]
        private GameObject buttonTemplate;

        void Start()
        {
            GameObject g;
            Image i;
            Button b;
            Text t;

            foreach(buttonInformation n in ButtonData)
            {
                g = Instantiate(buttonTemplate);
                g.SetActive(true);
                g.transform.SetParent(transform,false);
                g.transform.localScale = Vector3.one;

                g.name = n.type.ToString();

                i = g.transform.FindChild("icon").GetComponentInChildren<Image>();

                if (n.icon != null)
                {                   
                    i.sprite = n.icon;
                }
                else
                {
                    t = g.GetComponentInChildren<Text>();
                    t.text = n.type.ToString();
                    i.gameObject.SetActive(false);
                    t.gameObject.SetActive(true);
                }

                b = GetComponentInChildren<Button>();
                UnityEngine.Events.UnityAction newEvent = new UnityEngine.Events.UnityAction(delegate
                {
                    TowerDefence.Managers.BuildManager.instance.PlaceTower(n.type);
                    Debug.Log(n.type);
                });

                b.onClick.AddListener(newEvent);

            }
        }
    }
}