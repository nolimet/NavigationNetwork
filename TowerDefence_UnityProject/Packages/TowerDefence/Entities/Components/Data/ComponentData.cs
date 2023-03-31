using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefence.Entities.Components.Data
{
    [Serializable]
    internal sealed class ComponentData
    {
        [FormerlySerializedAs("Type")] [SerializeField]
        internal string TypeName;

        public Type Type => Type.GetType(TypeName);

        [FormerlySerializedAs("data")] [SerializeField]
        internal string Data;
#if UNITY_EDITOR
        [SerializeReference] internal IComponent SerializedComponent;
#endif

        internal void SerializeComponent(IComponent component)
        {
            TypeName = component.GetType().ToString();
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
            if (string.IsNullOrWhiteSpace(TypeName)) return default;
            var type = Type;

            if (type is null) return default;
            return JsonConvert.DeserializeObject(Data, type) as IComponent;
        }
    }
}