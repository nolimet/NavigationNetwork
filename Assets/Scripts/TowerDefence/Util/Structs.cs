using UnityEngine;
using System.Collections;
namespace TowerDefence.Utils
{
    [System.Serializable]
    public struct Level
    {
        public string LevelName;
        public Wave[] waves;
        public Path path;
    }

    [System.Serializable]
    public struct Wave
    {
        public int waveCountDownTimer;
        public WaveSpawnGroup[] groups;
    }

    [System.Serializable]
    public struct WaveSpawnGroup
    {
        public string enemy;
        public float Speed;
        public float SpawnDelay;
        public int spawnAmount;
    }

    [System.Serializable]
    public struct SpawnAbleEnemy
    {
        //public GameObject gameObject
        //{
        //    get { return _gameObject; }
        //    set { value = _gameObject; if (refrenceName == "" || refrenceName == null) refrenceName = value.name; }
        //}
        public GameObject gameObject;

        public string refrenceName;     
        
        public void AutoName()
        {
            refrenceName = gameObject.name;
        }  
    }

    [System.Serializable]
    public struct Path
    {
        public NodeData[] nodes;
        public NodeConnectionData[] connections;
    }

    [System.Serializable]
    public struct NodeData
    {
        public Vector3 Location;
        public NodeTypes NodeType;
        /// <summary>
        /// used to identify a node;
        /// </summary>
        public string nodeTag;
        /// <summary>
        /// used for spawnPoints
        ///  so they know what is there endpoint
        /// </summary>
        public int lastNode;
    }

    [System.Serializable]
    public struct NodeConnectionData
    {
        public int nodeA;
        public int nodeB;
    }
}