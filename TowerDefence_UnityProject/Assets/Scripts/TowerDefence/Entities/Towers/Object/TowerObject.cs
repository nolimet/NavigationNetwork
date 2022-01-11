using System;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers
{
    public class TowerObject : MonoBehaviour, ITowerObject
    {
        public Transform Transform => transform;

        public ITowerModel TowerModel => throw new NotImplementedException();

        public Vector2 GetGridPosition() => throw new NotImplementedException("TODO Implement grid system");

        public Vector3 GetWorldPosition() => transform.position;
    }
}