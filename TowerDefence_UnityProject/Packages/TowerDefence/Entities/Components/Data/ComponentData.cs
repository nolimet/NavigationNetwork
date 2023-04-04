using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefence.Entities.Components.Data
{
    [Serializable]
    internal sealed class ComponentData
    {
        [SerializeField] internal string TypeName;

        public Type Type => Type.GetType(TypeName);

        [FormerlySerializedAs("data")] [SerializeField]
        internal string Data;
#if UNITY_EDITOR
        [SerializeReference] internal IComponent SerializedComponent;
#endif

        internal void SerializeComponent(IComponent component)
        {
            TypeName = component.GetType().AssemblyQualifiedName;
            Data = JsonConvert.SerializeObject(component);
        }

#if UNITY_EDITOR
        internal void SetReferenceValue()
        {
            SerializedComponent = DeserializeComponent();
        }
#endif

        internal IComponent DeserializeComponent()
        {
            if (string.IsNullOrWhiteSpace(TypeName)) throw new NullReferenceException($"Type name is empty or null: {TypeName ?? "null"}");
            var type = Type;

            if (type is null) throw new NullReferenceException($"Type is null. Type cannot be null. Type name {TypeName}");
            return JsonConvert.DeserializeObject(Data, type) as IComponent;
        }
    }
}