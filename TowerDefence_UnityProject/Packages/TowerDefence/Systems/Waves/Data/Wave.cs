using System;
using Newtonsoft.Json;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public readonly struct Wave
    {
        [JsonProperty("EnemyGroups", Required = Required.Always)]
        public readonly EnemyGroup[] EnemyGroups;

        [JsonConstructor]
        public Wave(EnemyGroup[] enemyGroups)
        {
            EnemyGroups = enemyGroups;
        }

        [Serializable, JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public readonly struct EnemyGroup
        {
            [JsonProperty("EnemyID", Required = Required.Always)]
            public readonly string EnemyID;

            [JsonProperty("EntranceId", Required = Required.Always)]
            public readonly int EntranceId;

            [JsonProperty("ExitId", Required = Required.Always)]
            public readonly int ExitId;

            [JsonProperty("PathID")] public readonly int PathID;

            [JsonProperty("SpawnTime")] public readonly double[] SpawnTime;

            [JsonProperty("EnemyGroupSize")] public readonly ulong? GroupSize;
            [JsonProperty("spawnInterval")] public readonly double? SpawnInterval;
            [JsonProperty("spawnDelay")] public readonly double? SpawnDelay;


            [JsonConstructor]
            public EnemyGroup(string enemyID, int entranceId, int exitId, int pathID, double[] spawnTime, ulong? groupSize, double? spawnInterval, double? spawnDelay)
            {
                EnemyID = enemyID;
                EntranceId = entranceId;
                ExitId = exitId;
                PathID = pathID;

                if (spawnTime is { Length: > 1 } && groupSize == 0)
                {
                    SpawnTime = spawnTime;
                    GroupSize = null;
                    SpawnInterval = null;
                    SpawnDelay = null;
                }
                else
                {
                    SpawnTime = null;
                    GroupSize = groupSize;
                    SpawnInterval = spawnInterval;
                    SpawnDelay = spawnDelay;
                }
            }
        }
    }
}