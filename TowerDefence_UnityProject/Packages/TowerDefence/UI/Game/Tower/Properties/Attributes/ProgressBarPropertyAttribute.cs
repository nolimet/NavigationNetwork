using System;

namespace TowerDefence.UI.Game.Tower.Properties.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ProgressBarPropertyAttribute : Attribute
    {
        public readonly string MaxValuePropertyName;
        public readonly string MinValuePropertyName;

        public readonly double MinValue;
        public readonly double MaxValue;

        public ProgressBarPropertyAttribute(string maxValuePropertyName, string minValuePropertyName)
        {
            MaxValuePropertyName = maxValuePropertyName;
            MinValuePropertyName = minValuePropertyName;
        }

        public ProgressBarPropertyAttribute(string maxValuePropertyName, double minValue = 0)
        {
            MinValue = minValue;
            MaxValuePropertyName = maxValuePropertyName;
        }

        public ProgressBarPropertyAttribute(double maxValue, string minValuePropertyName)
        {
            MaxValue = maxValue;
            MinValuePropertyName = minValuePropertyName;
        }

        public ProgressBarPropertyAttribute(double maxValue, double minValue = 0)
        {
            MaxValue = maxValue;
            MinValue = minValue;
        }
    }
}