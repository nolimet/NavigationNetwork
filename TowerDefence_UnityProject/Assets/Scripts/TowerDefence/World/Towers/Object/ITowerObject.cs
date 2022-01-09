using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TowerDefence.World.Towers
{
    public interface ITowerObject
    {
        Vector3 GetWorldPosition();
        Vector2 GetGridPosition();
        Transform Transform { get; }
    }
}
