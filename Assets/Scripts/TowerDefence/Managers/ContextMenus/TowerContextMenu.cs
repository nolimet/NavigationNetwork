﻿using System.Collections;
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

        public BaseTower currentTower { get { return _currentTower; } }
        BaseTower _currentTower;        
        [SerializeField]
        GameObject ContainerContextItems = null;
        [SerializeField]
        GameObject TemplateButton = null;
        [SerializeField]
        GameObject DropMenuGroup;
        [SerializeField]
        GameObject ToggleTemplate;
        [SerializeField]
        GameObject DropDownMenuButton;

        GameObject TowerRadius;
        void Awake()
        {
            Close();
            _instance = this;
        }

        public void Open(TowerDefence.BaseTower tower)
        {
            if (TowerRadius)
                Destroy(TowerRadius);
            
            while (ContainerContextItems.transform.childCount > 0)
            {
                DestroyImmediate(ContainerContextItems.transform.GetChild(0).gameObject);
            }

            while(DropMenuGroup.transform.childCount > 0)
            {
                DestroyImmediate(DropMenuGroup.transform.GetChild(0).gameObject);
            }

            transform.position = tower.ContextMenuOffset + (Vector2)tower.transform.position;
            tower.AddContextItems(ContainerContextItems, TemplateButton);
            tower.AddDropDownMenuItems(DropMenuGroup, ToggleTemplate,DropDownMenuButton.transform.position);
            gameObject.SetActive(true);

            DropMenuGroup.SetActive(false);
            _currentTower = tower;

            TowerRadius = Util.DrawCircle.Draw(_currentTower.transform, new Color(0.2941176470588235f, 0.8196078431372549f, 0.4f), tower.range);
        }

        public void OpenDropDown()
        {
            DropMenuGroup.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            _currentTower = null;
            Destroy(TowerRadius);
            TowerRadius = null;
        }

        public void Move()
        {
            Managers.PlacementManager.instance.onBeginPlace(_currentTower.gameObject, false);
        }
    }
}