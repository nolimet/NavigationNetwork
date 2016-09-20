using UnityEngine;
using System.Collections;

namespace TowerDefence.Projectile
{
    public class TowerProjectileBase : MonoBehaviour
    {
        protected float Damage;
        protected float Speed;

        protected BaseEnemy Target;

        protected BulletType type;
        public BulletType Type { get { return type; } }


        public virtual void setTarget(BaseEnemy Target)
        {
            this.Target = Target;
        }

        public virtual void setDamage(float Damage)
        {
            this.Damage = Damage;
        }

        public virtual void setSpeed(float Speed)
        {
            this.Speed = Speed;
        }

        public virtual void Update()
        {
            if (CheckTargetIsAlive())
            {
                TargetCheck_Update();
                Move_Update();
            }
        }

        public virtual void TargetCheck_Update()
        {
            if(Vector2.Distance(transform.position,Target.transform.position)<0.5f)
            {
                Target.TakeDamage(Damage);
                Remove();
            }
        }

        Vector2 PositionDelta;
        public virtual void Move_Update()
        {
            PositionDelta = Target.transform.position - transform.position;

            PositionDelta = Util.Common.AngleToVector(Util.Common.VectorToAngle(PositionDelta));

            transform.Translate(PositionDelta * Speed * Time.deltaTime);
        }

        public virtual void Remove()
        {
            BulletPool.RemoveObj(this);
        }

        public virtual bool CheckTargetIsAlive()
        {
            if (!Target)
            {
                Remove();
                return false;
            }
            return true;
        }
    }
}