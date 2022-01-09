using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.World.Towers
{
    public class TowerObject : MonoBehaviour ,ITowerObject
    {
        public Transform Transform => transform;

        public Vector2 GetGridPosition() => throw new NotImplementedException("TODO Implement grid system");

        public Vector3 GetWorldPosition() => transform.position;
    }
}
