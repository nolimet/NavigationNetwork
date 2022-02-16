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
        [SerializeField] internal byte[] componentData;

        internal void SerializeComponent(IComponent component)
        {
            type = component.GetType().ToString().Replace("TowerDefence.Entities.", "");
            using (var memoryStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, component);
                componentData = memoryStream.ToArray();
            }
        }

        internal IComponent DeserializeComponent()
        {
            IComponent result = null;
            try
            {
                using (var memoryStream = new MemoryStream(this.componentData))
                {
                    var formatter = new BinaryFormatter();
                    result = formatter.Deserialize(memoryStream) as IComponent; ;
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                return default;
            }
            return result;
        }
    }
}