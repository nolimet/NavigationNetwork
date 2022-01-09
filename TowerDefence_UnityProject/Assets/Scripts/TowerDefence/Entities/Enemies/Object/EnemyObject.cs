using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies;
using UnityEngine;

namespace TowerDefence.Entities.Enemies
{
    public class EnemyObject : MonoBehaviour, IEnemyObject
    {
        public Transform Transform => transform;

        public float DistanceToTarget() => throw new NotImplementedException();

        public Vector3 GetWorldPosition() => transform.position;
    }
}
