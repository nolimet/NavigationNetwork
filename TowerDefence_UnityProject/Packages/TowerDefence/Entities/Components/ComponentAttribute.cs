using System;

namespace TowerDefence.Entities.Components
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    internal sealed class ComponentAttribute : Attribute
    {
        public readonly ComponentType ComponentType;
        public ComponentAttribute(ComponentType componentType)
        {
            this.ComponentType = componentType;
        }
    }

    enum ComponentType
    {
        Enemy,
        Tower
    }
}