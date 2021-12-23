using Newtonsoft.Json;
using System;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public readonly struct Wave
    {
        [JsonProperty("EnemyGroups", NullValueHandling = NullValueHandling.Ignore)]
        public readonly EnemyGroup[] enemyGroups;

        public Wave(EnemyGroup[] enemyGroups)
        {
            this.enemyGroups = enemyGroups;
        }

        [Serializable]
        public readonly struct EnemyGroup
        {
            [JsonProperty("EnemyID", Required = Required.Always)]
            public readonly string enemyID;

            [JsonProperty("PathID", Required = Required.Always)]
            public readonly int pathID;

            [JsonProperty("SpawnTime", Required = Required.Always)]
            public readonly float[] spawnTime;

            public EnemyGroup(string enemyID, int pathID, float[] spawnTime)
            {
                this.enemyID = enemyID;
                this.pathID = pathID;
                this.spawnTime = spawnTime;
            }
        }
    }
}