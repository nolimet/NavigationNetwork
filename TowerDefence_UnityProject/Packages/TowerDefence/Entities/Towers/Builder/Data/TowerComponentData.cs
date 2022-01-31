using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using TowerDefence.Entities.Towers.Components;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Builder.Data
{
    [Serializable]
    internal class TowerComponentData
    {
        [SerializeField] internal string type;
        [SerializeField] internal byte[] componentData;

        internal void SerializeTowerComponent(ITowerComponent component)
        {
            type = component.GetType().ToString().Replace("TowerDefence.Entities.Towers.Components.", "");
            using (var memoryStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, component);
                componentData = memoryStream.ToArray();
            }
        }

        internal ITowerComponent DeserializeTowerComponent()
        {
            ITowerComponent result = null;
            try
            {
                using (var memoryStream = new MemoryStream(this.componentData))
                {
                    var formatter = new BinaryFormatter();
                    result = formatter.Deserialize(memoryStream) as ITowerComponent; ;
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