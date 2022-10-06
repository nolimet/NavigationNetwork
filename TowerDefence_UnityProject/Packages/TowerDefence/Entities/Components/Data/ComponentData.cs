using Newtonsoft.Json;
using System;
using UnityEngine;

namespace TowerDefence.Entities.Components.Data
{
    [Serializable]
    internal sealed class ComponentData
    {
        [SerializeField] internal string type;
        [SerializeField] internal string data;
#if UNITY_EDITOR
        [SerializeReference] internal IComponent SerializedComponent;
#endif
        
        internal void SerializeComponent(IComponent component)
        {
            type = component.GetType().FullName;
            data = JsonConvert.SerializeObject(component);
        }

        internal void SetReferenceValue()
        {
            SerializedComponent = DeserializeComponent();
        }

        internal IComponent DeserializeComponent()
        {
            if (string.IsNullOrWhiteSpace(this.type)) return default;
            var type = Type.GetType(this.type);
            
            if (type is null) return default;
            return JsonConvert.DeserializeObject(data, type) as IComponent;
        }
    }
}