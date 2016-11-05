using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class CannonTower : BaseTower
    {
        private float fireDelay;
        private float weaponCooldown;

        protected override void Start()
        {
            base.Start();

            fireDelay = 60f / fireRate;
        }

        protected override void HasTarget()
        {      
            Fire_Update();
        }

        protected override void Update()
        {
            base.Update();

            if (weaponCooldown > 0)
            {
                weaponCooldown -= Time.deltaTime;
            }
        }

        protected virtual void Fire_Update()
        {
            if (weaponCooldown <= 0)
            {
                if (Target != null)
                {
                    if (Target.VirtualHealth > 0)
                    {
                        Target.TakeVirtualDamage(Damage);
                        weaponCooldown = fireDelay;
                        Shoot();
                    }
                }
            }
        }

        protected void Shoot()
        {
            Projectile.TowerProjectileBase Bullet = ObjectPools.BulletPool.GetObj(BulletType.Base);
            Bullet.setDamage(Damage);
            Bullet.setSpeed(10f);
            Bullet.setTarget(Target);
            Bullet.gameObject.SetActive(true);
            Bullet.transform.position = fireWorldPosition;
            
        }
    }
}