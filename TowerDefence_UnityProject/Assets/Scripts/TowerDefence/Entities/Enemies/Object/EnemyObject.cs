using System;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine;

namespace TowerDefence.Entities.Enemies
{
    public class EnemyObject : MonoBehaviour, IEnemyObject
    {
        public Transform Transform => transform;

        public IEnemyModel Model { get; private set; }
        public string Name { get => this.name; set => this.name = value; }

        public void Damage(double damage)
        {
            Model.Health -= damage;
        }

        public float DistanceToTarget() => throw new NotImplementedException();

        public Vector3 GetWorldPosition() => transform.position;

        public void Setup(IEnemyModel enemyModel)
        {
            this.Model = enemyModel;
        }

        public void Tick()
        {
            Model.Obj.WalkPath();
        }
    }
}