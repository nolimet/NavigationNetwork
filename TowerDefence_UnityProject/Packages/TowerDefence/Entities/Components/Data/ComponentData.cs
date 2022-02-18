using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace TowerDefence.Entities.Components.Data
{
    [Serializable]
    internal class ComponentData
    {
        [SerializeField] internal string type;
        [SerializeField] internal string data;

        internal void SerializeComponent(IComponent component)
        {
            type = component.GetType().ToString();
            data = JsonConvert.SerializeObject(component);
        }

        internal IComponent DeserializeComponent()
        {
            return JsonConvert.DeserializeObject(data, Type.GetType(type)) as IComponent;
        }
    }
}