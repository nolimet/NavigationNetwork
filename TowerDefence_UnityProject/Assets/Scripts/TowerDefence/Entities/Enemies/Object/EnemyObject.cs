using System;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine;

namespace TowerDefence.Entities.Enemies
{
    public class EnemyObject : MonoBehaviour, IEnemyObject
    {
        public Transform Transform => transform;

        public IEnemyModel EnemyModel => throw new NotImplementedException();

        public float DistanceToTarget() => throw new NotImplementedException();

        public Vector3 GetWorldPosition() => transform.position;
    }
}