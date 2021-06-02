using TowerDefence.World.Path;
using UnityEngine;
using UnityEngine.Events;
using static TowerDefence.World.Path.Data.PathWorldData;

namespace TowerDefence.Entities.Enemies
{
    public abstract class EnemyBase : WalkerBase
    {
        private UnityAction<EnemyBase> ReachedEndAction;

        [SerializeField] private double currentHealth = 0;
        [SerializeField] private double maxHealth = 0;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void Setup(UnityAction<EnemyBase> ReachedEndAction, AnimationCurve3D path)
        {
            this.ReachedEndAction = ReachedEndAction;
            this.SetPath(path);
        }

        public override void ReachedEnd()
        {
            ReachedEndAction?.Invoke(this);
        }

        public bool IsDead
        {
            get => currentHealth <= 0;
        }
    }
}