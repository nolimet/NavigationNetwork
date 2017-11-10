using UnityEngine;
using System.Collections;

namespace TowerDefence
{
    public class RocketTower : BaseTower
    {
        private float fireDelay;
        private float weaponCooldown;

        protected override void Start()
        {
            base.Start();
            _type = "Rocket";
            fireDelay = 60f / _fireRate;

            _hasFireRate = false;
        }

        protected override void HasTarget()
        {
            if (weaponCooldown <= 0)
            {
                if (Target != null)
                {
                    if (Target.VirtualHealth > 0)
                    {
                        Target.TakeVirtualDamage(_damage);
                        weaponCooldown = fireDelay;

                        Projectile.TowerProjectileBase Bullet = ObjectPools.BulletPool.GetObj(BulletType.Base);
                        Bullet.setDamage(_damage);
                        Bullet.setSpeed(20f);
                        Bullet.setTarget(Target);
                        Bullet.gameObject.SetActive(true);
                        Bullet.transform.position = fireWorldPosition;

                        Target = null;
                    }
                }
            }
        }

        public override void IUpdate()
        {
            base.IUpdate();

            if (weaponCooldown > 0)
            {
                weaponCooldown -= Time.deltaTime;
            }
        }
    }
}