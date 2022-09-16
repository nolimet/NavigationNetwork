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

        internal void SerializeComponent(IComponent component)
        {
            type = component.GetType().FullName;
            data = JsonConvert.SerializeObject(component);
        }

        internal IComponent DeserializeComponent()
        {
            return JsonConvert.DeserializeObject(data, Type.GetType(type)) as IComponent;
        }
    }
}