using System;

namespace TowerDefence.UI.Game.Tower.Properties.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class UIPropertyAttribute : Attribute
    {
        private readonly string prettyName;

        public UIPropertyAttribute(string prettyName = "")
        {
            this.prettyName = prettyName;
        }

        public bool TryGetPrettyName(out string prettyName)
        {
            prettyName = this.prettyName;
            return !string.IsNullOrWhiteSpace(this.prettyName);
        }
    }
}