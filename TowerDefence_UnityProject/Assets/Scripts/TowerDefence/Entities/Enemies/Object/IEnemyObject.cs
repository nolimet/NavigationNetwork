using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.Entities.Enemies
{
    public interface IEnemyObject
    {
        public Transform Transform { get; }
        public Vector3 GetWorldPosition();
        public float DistanceToTarget();
    }
}
