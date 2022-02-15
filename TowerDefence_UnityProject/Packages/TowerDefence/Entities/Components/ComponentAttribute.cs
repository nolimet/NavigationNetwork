using System;

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

            for (int i = 0; i < restirctions.Length; i++)
            {
                if (restirctions[i] == other && restirctions[i] != self)
                {
                    return false;
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