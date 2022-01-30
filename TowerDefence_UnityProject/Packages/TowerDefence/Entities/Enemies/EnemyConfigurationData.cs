﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Entities.Enemies
{
    [CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "Configuration/Enemy Configuration")]
    public class EnemyConfigurationData : ScriptableObject
    {
        [SerializeField]
        private EnemyConfiguration[] enemies;

        private void OnEnable()
        {
            Enemies = enemies.ToDictionary(x => x.Id, x => x.Reference);
        }

        public IReadOnlyDictionary<string, AssetReference> Enemies { get; private set; }

        [System.Serializable]
        public class EnemyConfiguration
        {
            [SerializeField]
            private string id;

            [SerializeField]
            private AssetReference reference;

            public string Id => id;
            public AssetReference Reference => reference;
        }
    }
}