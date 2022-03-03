using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Components.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Entities.Enemies
{
    [CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "Configuration/Enemy Configuration")]
    public class EnemyConfigurationData : ScriptableObject
    {
        [SerializeField]
        private BaseEnemyConfiguration[] enemyBaseObjects;

        [SerializeField]
        private EnemyComponentConfiguration[] enemies;

        [SerializeField]
        private void OnEnable()
        {
            EnemyBaseObjects = enemyBaseObjects.ToDictionary(x => x.Id, x => x.Reference);
            Enemies = enemies.ToDictionary(x => x.Id, x => x);
        }

        internal IReadOnlyDictionary<string, AssetReferenceT<EnemyObject>> EnemyBaseObjects { get; private set; }
        internal Dictionary<string, EnemyComponentConfiguration> Enemies { get; private set; }

        [System.Serializable]
        private class BaseEnemyConfiguration
        {
            [SerializeField]
            private string id;

            [SerializeField]
            private AssetReferenceT<EnemyObject> reference;

            internal string Id => id;
            internal AssetReferenceT<EnemyObject> Reference => reference;
        }

        [System.Serializable]
        internal class EnemyComponentConfiguration
        {
            [SerializeField] private string id;
            [SerializeField] private string baseId;
            [SerializeField] private ComponentConfigurationObject componentConfiguration;

            internal string Id => id;
            internal string BaseId => baseId;
            internal ComponentConfigurationObject ComponentConfiguration => componentConfiguration;
        }
    }
}