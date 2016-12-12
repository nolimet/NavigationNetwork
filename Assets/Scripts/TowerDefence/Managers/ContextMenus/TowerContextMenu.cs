using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefence.Managers.ContextMenus
{
    public class TowerContextMenu : MonoBehaviour
    {
        BaseTower currentTower;
        public void Open(TowerDefence.BaseTower tower)
        {
            transform.position = tower.ContextMenuOffset + (Vector2)tower.transform.position;
            gameObject.SetActive(true);
            currentTower = tower;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            currentTower = null;
        }

        public void Move()
        {
            Managers.PlacementManager.instance.onBeginPlace(currentTower.gameObject, false);
        }
    }
}