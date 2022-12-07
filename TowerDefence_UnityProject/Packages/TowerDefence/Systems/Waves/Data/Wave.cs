using System;
using Newtonsoft.Json;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public readonly struct Wave
    {
        [JsonProperty("EnemyGroups", Required = Required.Always)]
        public readonly EnemyGroup[] enemyGroups;

        [JsonConstructor]
        public Wave(EnemyGroup[] enemyGroups)
        {
            this.enemyGroups = enemyGroups;
        }

        [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public readonly struct EnemyGroup
        {
            [JsonProperty("EnemyID", Required = Required.Always)]
            public readonly string enemyID;

            [JsonProperty("EntranceId", Required = Required.Always)]
            public readonly int entranceId;

            [JsonProperty("ExitId", Required = Required.Always)]
            public readonly int exitId;

            [JsonProperty("PathID")] public readonly int pathID;

            [JsonProperty("SpawnTime")] public readonly float[] spawnTime;

            [JsonProperty("EnemyGroupSize")] public readonly long? enemyGroupSize;
            [JsonProperty("spawnInterval")] public readonly double? spawnInterval;
            [JsonProperty("spawnDelay")] public readonly double? spawnDelay;

            [JsonConstructor]
            public EnemyGroup(string enemyID, int pathID, int entranceId, int exitId, float[] spawnTime)
            {
                this.enemyID = enemyID;
                this.pathID = pathID;
                this.entranceId = entranceId;
                this.exitId = exitId;
                this.spawnTime = spawnTime;
            }
        }
    }
}