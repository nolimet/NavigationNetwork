using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.World.Path;
using UnityEngine;

namespace TowerDefence.Entities.Enemies
{
    public abstract class EnemyBase : WalkerBase
    {
        private Action ReachedEndAction;

        [SerializeField] private double currentHealth = 0;
        [SerializeField] private double maxHealth = 0;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public override void ReachedEnd()
        {
            ReachedEndAction?.Invoke();
        }

        public bool IsDead
        {
            get => currentHealth <= 0;
        }
    }
}