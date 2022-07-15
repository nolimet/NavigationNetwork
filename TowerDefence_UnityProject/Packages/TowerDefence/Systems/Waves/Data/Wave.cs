using Newtonsoft.Json;
using System;
using UnityEngine;

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

        [Serializable]
        public readonly struct EnemyGroup
        {
            [JsonProperty("EnemyID", Required = Required.Always)]
            public readonly string enemyID;

            [JsonProperty("PathID")]
            public readonly int pathID;

            [JsonProperty("EntranceId")]
            public readonly int entranceId;

            [JsonProperty("EntrancePosition")]
            public readonly Vector2Int entrancePosition;

            [JsonProperty("ExitId")]
            public readonly int exitId;

            [JsonProperty("ExitPosition")]
            public readonly Vector2Int exitPosition;

            [JsonProperty("SpawnTime", Required = Required.Always)]
            public readonly float[] spawnTime;

            [JsonConstructor]
            public EnemyGroup(string enemyID, int pathID, int entranceId, Vector2Int entrancePosition, int exitId, Vector2Int exitPosition, float[] spawnTime)
            {
                this.enemyID = enemyID;
                this.pathID = pathID;
                this.entranceId = entranceId;
                this.entrancePosition = entrancePosition;
                this.exitPosition = exitPosition;
                this.exitId = exitId;
                this.spawnTime = spawnTime;
            }
        }
    }
}