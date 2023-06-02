using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.Utilities;
using TowerDefence.Entities.Components;

namespace TowerDefence.EditorScripts.Entities.Components
{
    public class RequiredComponentValidator
    {
        private readonly Dictionary<Type, Type[]> requiredTypeLookup;
        private readonly Dictionary<Type, Type[]> substituteLookup;

        public RequiredComponentValidator(Dictionary<string, Type> types)
        {
            var requiredCompType = typeof(RequiredComponentAttribute);
            var allTypes = types.Values;
            requiredTypeLookup = new Dictionary<Type, Type[]>();
            foreach (var type in types.Values)
            {
                var att = type.GetCustomAttribute(requiredCompType, true);
                if (att is RequiredComponentAttribute rca)
                {
                    requiredTypeLookup.Add(type, rca.RequiredComponents);
                }
            }

            var collectedTypes = types.Values.ToDictionary(x => x, x => x.GetInterfaces());
            Dictionary<Type, List<Type>> protoLookup = new();
            foreach (var (type, interfaceTypes) in collectedTypes)
            {
                foreach (var interfaceType in interfaceTypes)
                {
                    if (!protoLookup.ContainsKey(interfaceType))
                    {
                        protoLookup.Add(interfaceType, new List<Type>());
                    }

                    protoLookup[interfaceType].Add(type);
                }
            }

            substituteLookup = protoLookup.ToDictionary(x => x.Key, x => x.Value.ToArray());
        }

        public ICollection<Type> GetMissingTypes(Type type, ICollection<Type> assignedTypes)
        {
            List<Type> requiredTypes = new();
            if (requiredTypeLookup.TryGetValue(type, out var required))
            {
                requiredTypes.AddRange(required);
            }

            var interfaces = type.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (requiredTypeLookup.TryGetValue(@interface, out required))
                {
                    requiredTypes.AddRange(required);
                }
            }

            foreach (var baseClass in type.GetBaseClasses())
            {
                if (requiredTypeLookup.TryGetValue(baseClass, out required))
                {
                    requiredTypes.AddRange(required);
                }
            }

            if (requiredTypes.Count == 0)
                return Array.Empty<Type>();

            return requiredTypes.Distinct()
                .Where
                (
                    requiredType =>
                    {
                        if (assignedTypes.Contains(requiredType))
                            return false;
                        return !assignedTypes.Any(assignedType => assignedType.GetInterfaces().Contains(requiredType));
                    }
                )
                .ToArray();
        }

        public bool ValidateComponents(ICollection<Type> assignedTypes)
        {
            return assignedTypes.All(x => GetMissingTypes(x, assignedTypes).Count == 0);
        }
    }
}