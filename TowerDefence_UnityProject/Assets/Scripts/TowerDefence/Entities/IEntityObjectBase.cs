using TowerDefence.Systems.Selection;
using UnityEngine;

namespace TowerDefence.Entities
{
    public interface IEntityObjectBase : ISelectable
    {
        public string Name { get; set; }
        public Transform Transform { get; }

        public Vector3 GetWorldPosition();

        public void Destroy() => Object.Destroy(Transform.gameObject);

        public void Tick();
    }
}