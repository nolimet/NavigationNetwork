using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Systems.Selection;
using UnityEngine;

namespace TowerDefence.Entities
{
    public interface IEntityObjectBase : ISelectable
    {
        public Transform Transform { get; }

        public Vector3 GetWorldPosition();
    }
}