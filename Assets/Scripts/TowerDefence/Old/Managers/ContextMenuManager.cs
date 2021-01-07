using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Managers.ContextMenus;
    
namespace TowerDefence.Managers
{
    public class ContextMenuManager : MonoBehaviour
    {
        [SerializeField]
        TowerContextMenu TowerMenu;

        void Awake()
        {
            InputManager.instance.onRightMouseClick += OnClick;
            PlacementManager.instance.onStartPlacing += Instance_onStartPlacing;
        }

        private void Instance_onStartPlacing()
        {
            InputManager.instance.onRightMouseClick -= OnClick;
            PlacementManager.instance.onEndPlacing += Instance_onEndPlacing;
            PlacementManager.instance.onStartPlacing -= Instance_onStartPlacing;
        }

        private void Instance_onEndPlacing()
        {
            InputManager.instance.onRightMouseClick += OnClick;
            PlacementManager.instance.onEndPlacing -= Instance_onEndPlacing;
            PlacementManager.instance.onStartPlacing += Instance_onStartPlacing;
        }

        void OnClick()
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case TagManager.Tower:
                        TowerMenu.Open(hit.collider.gameObject.GetComponent<TowerDefence.BaseTower>());
                        break;
                }
            }
            else
            {
                TowerMenu.Close();
            }
        }
    }
}