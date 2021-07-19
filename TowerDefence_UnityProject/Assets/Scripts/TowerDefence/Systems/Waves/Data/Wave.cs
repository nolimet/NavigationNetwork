using System;

namespace TowerDefence.Systems.Waves.Data
{
    [Serializable]
    public class Wave
    {
        public readonly EnemyGroup[] enemyGroups = new EnemyGroup[0];

        public Wave(EnemyGroup[] enemyGroups)
        {
            this.enemyGroups = enemyGroups;
        }

        [Serializable]
        public class EnemyGroup
        {
            public readonly string enemyID = string.Empty;
            public readonly int pathID = 0;
            public readonly float[] spawnTime = new float[0];

            public EnemyGroup(string enemyID, int pathID, float[] spawnTime)
            {
                this.enemyID = enemyID;
                this.pathID = pathID;
                this.spawnTime = spawnTime;
            }
        }
    }
}