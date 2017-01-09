using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefence.Managers.ContextMenus
{
    public class TowerContextMenu : MonoBehaviour
    {
        public static TowerContextMenu instance
        {
            get
            {
                if (_instance)
                    return _instance;
                _instance = FindObjectOfType<TowerContextMenu>();
                if (_instance)
                    return _instance;

                Debug.LogError("NO TowerContextMenu FOUND! Check what is calling it");
                return null;
            }
        }
        static TowerContextMenu _instance;

        BaseTower _currentTower;
        public BaseTower currentTower { get { return _currentTower; } }
        [SerializeField]
        GameObject ContainerContextItems = null;
        [SerializeField]
        GameObject TemplateButton = null;
        void Start()
        {
            Close();
        }

        public void Open(TowerDefence.BaseTower tower)
        {
            while (ContainerContextItems.transform.childCount > 0)
            {
                DestroyImmediate(ContainerContextItems.transform.GetChild(0).gameObject);
            }

            transform.position = tower.ContextMenuOffset + (Vector2)tower.transform.position;
            tower.AddContextItems(ContainerContextItems, TemplateButton);
            gameObject.SetActive(true);
            _currentTower = tower;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _currentTower = null;
        }

        public void Move()
        {
            Managers.PlacementManager.instance.onBeginPlace(_currentTower.gameObject, false);
        }
    }
}