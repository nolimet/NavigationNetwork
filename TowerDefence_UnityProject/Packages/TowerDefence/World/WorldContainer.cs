using UnityEngine;

namespace TowerDefence.World
{
    public class WorldContainer : MonoBehaviour
    {
        public Transform PathContainer { get; private set; }
        public Transform TurretContainer { get; private set; }
        public Transform EnemyContainer { get; private set; }

        public void DoSetup()
        {
            PathContainer = new GameObject("Path Container").transform;
            TurretContainer = new GameObject("Turret Container").transform;
            EnemyContainer = new GameObject("Enemy Container").transform;

            PathContainer.SetParent(transform, false);
            TurretContainer.SetParent(transform, false);
            EnemyContainer.SetParent(transform, false);
        }
    }
}