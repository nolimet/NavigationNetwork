using System;

namespace TowerDefence.Entities.Components
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    internal sealed class ComponentAttribute : Attribute
    {
    }
}