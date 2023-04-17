using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Towers.Components.Damage;
using TowerDefence.Entities.Towers.Components.TargetFinders;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefence.Entities.Components.Data
{
    [CreateAssetMenu(menuName = "Entities/ComponentObject")]
    internal sealed class ComponentConfigurationObject : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }

        [FormerlySerializedAs("components")] [SerializeField]
        internal List<ComponentData> Components = new();

        [FormerlySerializedAs("type")] [SerializeField]
        internal ComponentType Type;

        [ContextMenu("Test-Write")]
        private void TestWrite()
        {
            List<IComponent> components = new();
            if (Components == null)
            {
                Components = new List<ComponentData>();
            }

            components.Add(new NearestTargetFinder());
            components.Add(new DamageAllTargets());

            foreach (var component in components)
            {
                var componentData = new ComponentData();
                componentData.SerializeComponent(component);
                Components.Add(componentData);
            }
        }

        [ContextMenu("Test-Read")]
        private void TestRead()
        {
            if (Components == null) return;

            var components = Components.Select(component => component.DeserializeComponent()).ToList();

            foreach (var component in components)
            {
                if (component is not null)
                    Debug.Log(component.GetType());
                else
                    Debug.LogError("missing component!");
            }
        }
    }
}