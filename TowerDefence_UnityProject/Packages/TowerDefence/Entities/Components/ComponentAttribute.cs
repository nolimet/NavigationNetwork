using System;
using System.Linq;
using UnityEngine;

namespace TowerDefence.Entities.Components
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    internal sealed class ComponentAttribute : Attribute
    {
        public readonly ComponentType ComponentType;
        private readonly Type[] restrictions;
        public bool AllowMultiple = false;

        public ComponentAttribute(ComponentType componentType, params Type[] restrictions)
        {
            ComponentType = componentType;
            this.restrictions = restrictions;
        }

        internal bool AnyRestrictionsMatch(Type self, Type other)
        {
            if (!AllowMultiple && self.Equals(other))
                return true;

            foreach (var restriction in restrictions)
            {
                if (restriction.Equals(other) || other is null)
                    return true;

                var interfaces = other.GetInterfaces();
                if (interfaces.Any() && interfaces.Contains(restriction))
                    return true;
            }

            return false;
        }
    }

    internal enum ComponentType
    {
        Enemy,
        Tower
    }
}