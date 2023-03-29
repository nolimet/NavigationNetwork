using System;
using System.Linq;

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
            if (!AllowMultiple && other == self)
                return true;

            foreach (var restriction in restrictions)
            {
                if (restriction == other && restriction != self)
                    return true;

                if (!restriction.IsInterface) continue;

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