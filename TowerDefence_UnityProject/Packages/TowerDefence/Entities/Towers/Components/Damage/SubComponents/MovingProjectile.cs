using TowerDefence.Entities.Enemies;
using UnityEngine;

namespace TowerDefence.Entities.Towers.Components.Damage.SubComponents
{
    public class MovingProjectile : MonoBehaviour
    {
        private const float minDist = .1f;

        private float maxSpeed;
        private IEnemyObject target;
        private double damage;

        public void Setup(IEnemyObject target, double damage, float maxSpeed)
        {
            this.target = target;
            this.damage = damage;
            this.maxSpeed = maxSpeed;

            target.DeathAction += OnTargetDeath;
        }

        private void OnTargetDeath(IEnemyObject arg0)
        {
            if (gameObject)
                Destroy(gameObject);
        }

        private void Update()
        {
            var targetPos = target.GetWorldPosition();
            var selfPos = transform.position;
            selfPos = Vector3.MoveTowards(selfPos, targetPos, maxSpeed * Time.deltaTime);
            transform.position = selfPos;

            if ((targetPos - selfPos).sqrMagnitude < minDist)
            {
                target.Damage(damage);
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (target != null)
            {
                target.DeathAction -= OnTargetDeath;
            }
        }
    }
}