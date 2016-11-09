using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TowerDefence.Enemies;
namespace TowerDefence.ObjectPools
{
    public class EnemyPool : MonoBehaviour
    {

        public static EnemyPool instance;
        public delegate void RemoveObject(BaseEnemy item);
        public event RemoveObject onRemove;

        public List<BaseEnemy> ActivePool, InActivePool;
        [Tooltip("Name needs to be same as enum type")]
        public PrefabContainer[] EnemyPrefabs = new PrefabContainer[0];

        public static void RemoveObj(BaseEnemy item)
        {
            if (instance && instance.ActivePool.Contains(item))
            {
                instance.ActivePool.Remove(item);
                instance.InActivePool.Add(item);

                item.gameObject.SetActive(false);
            }
        }

        public static BaseEnemy GetObj(string Type)
        {
            if (instance)
            {
                BaseEnemy w;
                if (instance.InActivePool.Any(e => e.typeName == Type))
                {
                    w = instance.InActivePool.First(e => e.typeName == Type);

                    instance.InActivePool.Remove(w);
                    instance.ActivePool.Add(w);

                    w.gameObject.SetActive(true);
                }
                else
                {
    
                    //GameObject g = Instantiate(Resources.Load("Weapons/" + Type.ToString()), Vector3.zero, Quaternion.identity) as GameObject;
                    PrefabContainer prefab = instance.EnemyPrefabs.FirstOrDefault(e => e.script.typeName == Type);
                    GameObject g = Instantiate(prefab.gameObject) as GameObject;
                    w = g.GetComponent<BaseEnemy>();

                    instance.ActivePool.Add(w);
                    w.transform.SetParent(instance.transform);
                }

                return w;
            }
            return null;
        }

        public void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        public void Update()
        {
            if (instance == null)
                instance = this;
        }

        [System.Serializable]
        public struct PrefabContainer
        {
            public BaseEnemy script;
            public GameObject gameObject;
        }
    }
}