using System;

namespace TowerDefence.Entities.Towers.Builder
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    internal sealed class TowerComponentAttribute : Attribute
    {
    }
}