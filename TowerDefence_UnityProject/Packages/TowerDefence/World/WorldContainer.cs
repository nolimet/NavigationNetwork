﻿using UnityEngine;

namespace TowerDefence.World
{
    public sealed class WorldContainer : MonoBehaviour
    {
        public Transform PathContainer { get; private set; }
        public Transform TowerContainer { get; private set; }
        public Transform EnemyContainer { get; private set; }
        public Transform TileContainer { get; private set; }

        public void DoSetup()
        {
            PathContainer = new GameObject("Path Container").transform;
            TowerContainer = new GameObject("Turret Container").transform;
            EnemyContainer = new GameObject("Enemy Container").transform;
            TileContainer = new GameObject("Tile Container").transform;

            PathContainer.SetParent(transform, false);
            TowerContainer.SetParent(transform, false);
            EnemyContainer.SetParent(transform, false);
            TileContainer.SetParent(transform, false);
        }
    }
}