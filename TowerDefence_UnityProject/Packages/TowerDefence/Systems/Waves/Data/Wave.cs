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

            [JsonProperty("SpawnTime")] public readonly double[] spawnTime;

            [JsonProperty("EnemyGroupSize")] public readonly ulong? groupSize;
            [JsonProperty("spawnInterval")] public readonly double? spawnInterval;
            [JsonProperty("spawnDelay")] public readonly double? spawnDelay;


            [JsonConstructor]
            public EnemyGroup(string enemyID, int entranceId, int exitId, int pathID, double[] spawnTime, ulong? groupSize, double? spawnInterval, double? spawnDelay)
            {
                this.enemyID = enemyID;
                this.entranceId = entranceId;
                this.exitId = exitId;
                this.pathID = pathID;

                if (spawnTime is { Length: > 1 } && groupSize == 0)
                {
                    this.spawnTime = spawnTime;
                    this.groupSize = null;
                    this.spawnInterval = null;
                    this.spawnDelay = null;
                }
                else
                {
                    this.spawnTime = null;
                    this.groupSize = groupSize;
                    this.spawnInterval = spawnInterval;
                    this.spawnDelay = spawnDelay;
                }
            }
        }
    }
}