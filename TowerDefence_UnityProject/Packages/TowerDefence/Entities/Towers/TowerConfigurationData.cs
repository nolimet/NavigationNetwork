using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TowerDefence.Entities.Components.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Entities.Towers
{
    [CreateAssetMenu(fileName = "Tower Configuration Data", menuName = "Configuration/Tower Configuration")]
    public sealed class TowerConfigurationData : ScriptableObject
    {
        [field: SerializeField] internal AssetReferenceT<GameObject> TowerBase { get; private set; }
        [SerializeField] private Tower[] towers = new Tower[0];

        /// <summary>
        /// Id's are in lower case to remove any case sensativity
        /// </summary>
        internal IReadOnlyDictionary<string, Tower> Towers { get; private set; }

        internal async Task<ComponentConfigurationObject> GetTowerAsync(string id)
        {
            if (Towers.TryGetValue(id.ToLower(), out var value))
            {
                return await value.GetComponentConfiguration();
            }
            return null;
        }

        private void OnEnable()
        {
            if (towers.Any())
            {
                Towers = towers.ToDictionary(x => x.Id.ToLower(), x => x);
            }
            else
            {
                Towers = new Dictionary<string, Tower>();
            }
        }

        [Serializable]
        internal class Tower
        {
            [SerializeField]
            private string id;

            [SerializeField]
            private AssetReferenceT<ComponentConfigurationObject> reference;

            private ComponentConfigurationObject configurationObject;

            internal string Id => id;

            public async Task<ComponentConfigurationObject> GetComponentConfiguration()
            {
                if (!configurationObject)
                {
                    var handle = reference.LoadAssetAsync();
                    await handle;
                    configurationObject = handle.Result;
                }
                return configurationObject;
            }
        }
    }
}