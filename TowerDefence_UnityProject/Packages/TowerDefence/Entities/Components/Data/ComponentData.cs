using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;

namespace TowerDefence.Entities.Components.Data
{
    [Serializable]
    internal sealed class ComponentData
    {
        [FormerlySerializedAs("type")] [SerializeField]
        internal string Type;

        [FormerlySerializedAs("data")] [SerializeField]
        internal string Data;
#if UNITY_EDITOR
        [SerializeReference] internal IComponent SerializedComponent;
#endif

        internal void SerializeComponent(IComponent component)
        {
            Type = component.GetType().FullName;
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
            if (string.IsNullOrWhiteSpace(Type)) return default;
            var type = System.Type.GetType(Type);

            if (type is null) return default;
            return JsonConvert.DeserializeObject(Data, type) as IComponent;
        }
    }
}