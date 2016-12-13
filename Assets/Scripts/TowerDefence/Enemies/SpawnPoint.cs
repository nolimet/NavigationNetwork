using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TowerDefence.Enemies
{
    public class SpawnPoint : MonoBehaviour
    {
        public NavigationNetwork.NavigationNode first;
        public NavigationNetwork.NavigationNode last;

        List<NavigationNetwork.NavigationBase> NavRoute;

        public void UpdateNavRoute()
        {
            BaseEnemy e = ObjectPools.EnemyPool.GetObj("base");
            e.currentTargetNode = first;
            e.TargetID = last.ID;

            NavRoute = e.GetPath();
        }

        public void Spawn()
        {

        }
    }
}