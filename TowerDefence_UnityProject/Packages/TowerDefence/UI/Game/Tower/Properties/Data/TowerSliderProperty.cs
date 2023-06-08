using System;
using System.Reflection;
using Sirenix.Utilities;
using TowerDefence.Entities.Components;
using TowerDefence.UI.Game.Tower.Properties.Interfaces;

namespace TowerDefence.UI.Game.Tower.Properties.Data
{
    public readonly struct TowerSliderProperty : ITowerProperty
    {
        public TowerSliderProperty(double minValue, double maxValue, MemberInfo minValueType, MemberInfo maxValueType, MemberInfo valueType)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.minValueType = minValueType;
            this.maxValueType = maxValueType;
            this.valueType = valueType;
            Label = valueType.GetNiceName();
        }

        private readonly double minValue;
        private readonly double maxValue;
        private readonly MemberInfo minValueType;
        private readonly MemberInfo maxValueType;
        private readonly MemberInfo valueType;
        public readonly string Label;

        public double GetMinValue(IComponent component) => GetValue(minValueType, minValue, component);

        public double GetMaxValue(IComponent component) => GetValue(maxValueType, maxValue, component);

        public double GetSliderValue(IComponent component) => GetValue(valueType, 0, component);

        public string GetValue(IComponent component) => $"{GetSliderValue(component)} / {GetMaxValue(component)}";

        private double GetValue(MemberInfo dynamicValue, double defaultValue, IComponent component)
        {
            if (dynamicValue is null)
                return defaultValue;

            var val = dynamicValue.GetMemberValue(component);
            return val switch
            {
                double d => d,
                float f => f,
                int i => i,
                short s => s,
                long l => l,
                decimal dec => (double)dec,
                _ => throw new NotSupportedException($"{dynamicValue.GetType()} is not supported in the slider must be number")
            };
        }
    }
}