using System;

namespace TowerDefence.UI.Game.Tower.Properties.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class UIPropertyAttribute : Attribute
    {
        public readonly string PrettyName;
        public readonly string Suffix;
        public readonly string Prefix;

        public UIPropertyAttribute(string prettyName = null, string suffix = null, string prefix = null)
        {
            PrettyName = prettyName;
            Suffix = suffix;
            Prefix = prefix;
        }
    }
}