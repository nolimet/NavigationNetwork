using System.Reflection;
using TowerDefence.Entities.Components;
using TowerDefence.UI.Game.Tower.Properties.Interfaces;

namespace TowerDefence.UI.Game.Tower.Properties.Data
{
    public record TowerProperty : ITowerProperty
    {
        private readonly PropertyInfo propertyInfo;
        private readonly FieldInfo fieldInfo;
        private readonly string label;
        private readonly string suffix;
        private readonly string prefix;

        public string GetValue(IComponent component)
        {
            var value = (propertyInfo?.GetValue(component) ?? fieldInfo?.GetValue(component)) ?? "null";

            if (!string.IsNullOrEmpty(prefix))
                value = $"{prefix}{value}";
            if (!string.IsNullOrEmpty(suffix))
                value = $"{value}{suffix}";

            return $"{label} {value}";
        }

        public TowerProperty(MemberInfo memberInfo, string label = null, string prefix = null, string suffix = null)
        {
            switch (memberInfo)
            {
                case PropertyInfo info:
                    propertyInfo = info;
                    break;
                case FieldInfo info:
                    fieldInfo = info;
                    break;
            }

            this.label = label ?? memberInfo.Name;
            this.prefix = prefix;
            this.suffix = suffix;
        }
    }
}