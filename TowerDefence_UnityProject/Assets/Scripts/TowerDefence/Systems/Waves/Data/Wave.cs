using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public readonly EnemyGroup[] enemyGroups;

    public class EnemyGroup
    {
        public readonly string enemyID;
        public readonly int pathID;
        public readonly float[] spawnTime;
    }
}