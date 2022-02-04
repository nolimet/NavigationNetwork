using System.Collections.Generic;
using TowerDefence.Entities.Towers.Components;
using TowerDefence.Entities.Towers.Components.Damage;
using TowerDefence.Entities.Towers.Components.TargetFinders;
using UnityEngine;

namespace TowerDefence.Entities.Components.Data
{
    [CreateAssetMenu(menuName = "Entities/ComponentObject")]
    internal class ComponentConfigurationObject : ScriptableObject
    {
        [SerializeField]
        internal List<ComponentData> components;

        [ContextMenu("Test-Write")]
        private void TestWrite()
        {
            List<IComponent> components = new();
            if (this.components == null)
            {
                this.components = new();
            }

            components.Add(new NearestTargetFinder());
            components.Add(new DamageAllTargets());

            foreach (var component in components)
            {
                var componentData = new ComponentData();
                componentData.SerializeComponent(component);
                this.components.Add(componentData);
            }
        }

        [ContextMenu("Test-Read")]
        private void TestRead()
        {
            List<IComponent> components = new();

            if (this.components != null)
            {
                foreach (var component in this.components)
                {
                    components.Add(component.DeserializeComponent());
                }

                foreach (var component in components)
                {
                    if (component != null)
                        Debug.Log(component.GetType());
                    else
                        Debug.Log(component);
                }
            }
        }
    }
}