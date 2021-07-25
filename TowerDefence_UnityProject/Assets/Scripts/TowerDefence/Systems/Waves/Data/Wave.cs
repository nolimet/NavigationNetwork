using System;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public readonly struct Wave
    {
        public readonly EnemyGroup[] enemyGroups;

        public Wave(EnemyGroup[] enemyGroups)
        {
            this.enemyGroups = enemyGroups;
        }

        [Serializable]
        public readonly struct EnemyGroup
        {
            public readonly string enemyID;
            public readonly int pathID;
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