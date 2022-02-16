using System;
using System.Linq;

namespace TowerDefence.Entities.Components
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    internal sealed class ComponentAttribute : Attribute
    {
        public readonly ComponentType ComponentType;
        public readonly Type[] restirctions;
        public readonly bool AllowMultiple = false;

        public ComponentAttribute(ComponentType componentType, params Type[] restirctions)
        {
            this.ComponentType = componentType;
            this.restirctions = restirctions;
        }

        internal bool AnyRestrictionsMatch(Type self, Type other)
        {
            if (!AllowMultiple && other == self)
            {
                return true;
            }

            foreach (var restriction in restirctions)
            {
                if (restriction == other && restriction != self)
                {
                    return true;
                }
                if (restriction.IsInterface)
                {
                    var interfaces = self.GetInterfaces();
                    if (interfaces.Any() && interfaces.Contains(restriction))
                    {
                        return true;
                    }
                }
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