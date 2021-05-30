using UnityEngine;

namespace TowerDefence.World
{
    public class WorldContainer : MonoBehaviour
    {
        public Transform PathContainer { get; private set; }
        public Transform TurretContainer { get; private set; }

        public void DoSetup()
        {
            PathContainer = new GameObject("Path Container").transform;
            TurretContainer = new GameObject("Turret Container").transform;

            PathContainer.SetParent(transform, false);
            TurretContainer.SetParent(transform, false);
        }
    }
}