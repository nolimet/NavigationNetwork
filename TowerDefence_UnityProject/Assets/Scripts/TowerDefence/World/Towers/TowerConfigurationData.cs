using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.World.Towers
{
    [CreateAssetMenu(fileName = "Tower Configuration Data", menuName = "Configuration/Tower Configuration")]
    public class TowerConfigurationData : ScriptableObject
    {
        [SerializeField]
        private Tower[] towers;

        /// <summary>
        /// Id's are in lower case to remove any case sensativity
        /// </summary>
        public IReadOnlyDictionary<string, AssetReference> Towers { get; private set; }

        public AssetReference GetTower(string id)
        {
            if (Towers.TryGetValue(id.ToLower(), out var value))
            {
                return value;
            }
            return null;
        }

        private void OnEnable()
        {
            if (towers != null && towers.Length > 0)
            {
                Towers = towers.ToDictionary(x => x.Id.ToLower(), x => x.Reference);
            }
            else
            {
                Towers = new Dictionary<string, AssetReference>();
            }
        }

        [Serializable]
        public class Tower
        {
            [SerializeField]
            private string id;

            [SerializeField]
            private AssetReference reference;

            public AssetReference Reference => reference;
            public string Id => id;
        }
    }
}