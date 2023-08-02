using System.Reflection;
using TowerDefence.Entities.Components;
using TowerDefence.UI.Game.Tower.Properties.Interfaces;

namespace TowerDefence.UI.Game.Tower.Properties.Data
{
    public record TowerProperty : ITowerProperty
    {
        private readonly PropertyInfo propertyInfo;
        private readonly FieldInfo fieldInfo;
        public readonly string Label;

        public string GetValue(IComponent component)
        {
            return $"{Label} {(propertyInfo?.GetValue(component) ?? fieldInfo?.GetValue(component)) ?? "null"}";
        }

        public TowerProperty(MemberInfo memberInfo, string label = null)
        {
            fieldInfo = null;
            propertyInfo = null;
            switch (memberInfo)
            {
                case PropertyInfo info:
                    propertyInfo = info;
                    break;
                case FieldInfo info:
                    fieldInfo = info;
                    break;
            }

            Label = string.IsNullOrEmpty(label) ? label : memberInfo.Name;
        }
    }
}