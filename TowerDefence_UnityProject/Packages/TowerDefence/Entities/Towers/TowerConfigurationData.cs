using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Components.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Entities.Towers
{
    [CreateAssetMenu(fileName = "Tower Configuration Data", menuName = "Configuration/Tower Configuration")]
    public class TowerConfigurationData : ScriptableObject
    {
        [field: SerializeField] internal AssetReferenceT<GameObject> TowerBase { get; private set; }
        [SerializeField] private Tower[] towers = new Tower[0];

        /// <summary>
        /// Id's are in lower case to remove any case sensativity
        /// </summary>
        internal IReadOnlyDictionary<string, AssetReferenceT<ComponentConfigurationObject>> Towers { get; private set; }

        internal AssetReferenceT<ComponentConfigurationObject> GetTower(string id)
        {
            if (Towers.TryGetValue(id.ToLower(), out var value))
            {
                return value;
            }
            return null;
        }

        private void OnEnable()
        {
            if (towers.Any())
            {
                Towers = towers.ToDictionary(x => x.Id.ToLower(), x => x.Reference);
            }
            else
            {
                Towers = new Dictionary<string, AssetReferenceT<ComponentConfigurationObject>>();
            }
        }

        [Serializable]
        private class Tower
        {
            [SerializeField]
            private string id;

            [SerializeField]
            private AssetReferenceT<ComponentConfigurationObject> reference;

            internal AssetReferenceT<ComponentConfigurationObject> Reference => reference;
            internal string Id => id;
        }
    }
}