using System.Collections.Generic;
using TowerDefence.Entities.Towers.Components;
using TowerDefence.Entities.Towers.Components.Damage;
using TowerDefence.Entities.Towers.Components.TargetFinders;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Builder.Data
{
    [CreateAssetMenu(menuName = "Entities/Towers")]
    internal class TowerConfigurationObject : ScriptableObject
    {
        [SerializeField]
        internal List<TowerComponentData> components;

        [ContextMenu("Test-Write")]
        private void TestWrite()
        {
            List<ITowerComponent> components = new();
            if (this.components == null)
            {
                this.components = new();
            }

            components.Add(new NearestTargetFinder());
            components.Add(new DamageAllTargets(10, 10));

            foreach (var component in components)
            {
                var componentData = new TowerComponentData();
                componentData.SerializeTowerComponent(component);
                this.components.Add(componentData);
            }
        }

        [ContextMenu("Test-Read")]
        private void TestRead()
        {
            List<ITowerComponent> components = new();

            if (this.components != null)
            {
                foreach (var component in this.components)
                {
                    components.Add(component.DeserializeTowerComponent());
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