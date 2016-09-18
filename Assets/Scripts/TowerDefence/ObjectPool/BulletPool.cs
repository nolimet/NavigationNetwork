using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TowerDefence.Projectile;
namespace TowerDefence
{
    public class BulletPool : MonoBehaviour
    {

        public static BulletPool instance;
        public delegate void RemoveObject(TowerProjectileBase item);
        public event RemoveObject onRemove;

        public List<TowerProjectileBase> ActivePool, InActivePool;

        public static void RemoveBullet(TowerProjectileBase item)
        {
            if (instance && instance.ActivePool.Contains(item))
            {
                instance.ActivePool.Remove(item);
                instance.InActivePool.Add(item);

                item.gameObject.SetActive(false);
            }
        }

        public static TowerProjectileBase GetBullet(BulletType Type)
        {
            if (instance)
            {
                TowerProjectileBase w;
                if (instance.InActivePool.Any(e => e.Type == Type))
                {
                    w = instance.InActivePool.First(e => e.Type == Type);

                    instance.InActivePool.Remove(w);
                    instance.ActivePool.Add(w);

                    w.gameObject.SetActive(true);
                }
                else
                {
                    GameObject g = Instantiate(Resources.Load("Weapons/" + Type.ToString()), Vector3.zero, Quaternion.identity) as GameObject;
                    w = g.GetComponent<TowerProjectileBase>();

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
    }
}