using TowerDefence.World.Path;
using UnityEngine;
using UnityEngine.Events;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies
{
    public abstract class EnemyBase : WalkerBase
    {
        private UnityAction<EnemyBase> reachedEndAction;
        private UnityAction<EnemyBase> outOfHealthAction;

        [SerializeField] private double currentHealth = 0;
        [SerializeField] private double maxHealth = 0;

        public double CurrentHealth => currentHealth;
        public double MaxHealth => maxHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        //TODO add sources or types support
        public virtual void ApplyDamage(double damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                outOfHealthAction?.Invoke(this);
            }
        }

        public void Setup(UnityAction<EnemyBase> reachedEndAction, UnityAction<EnemyBase> outOfHealthAction, AnimationCurve3D path)
        {
            this.reachedEndAction = reachedEndAction;
            this.outOfHealthAction = outOfHealthAction;
            this.SetPath(path);
        }

        public override void ReachedEnd()
        {
            reachedEndAction?.Invoke(this);
        }
    }
}