using System;

namespace TowerDefence.Entities.Components
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class RequiredComponentAttribute : Attribute
    {
        public readonly Type[] RequiredComponents;

        public RequiredComponentAttribute(params Type[] requiredComponents)
        {
            RequiredComponents = requiredComponents;

            if (!Validate())
            {
                throw new Exception("All types need to be interfaces");
            }
        }

        private bool Validate()
        {
            foreach (var type in RequiredComponents)
            {
                if (!type.IsInterface)
                {
                    return false;
                }
            }

            return true;
        }
    }
}