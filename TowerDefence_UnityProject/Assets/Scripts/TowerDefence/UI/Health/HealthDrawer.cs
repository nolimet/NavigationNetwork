using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Entities.Enemies;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefence.UI.Health
{
    public class HealthDrawer : MonoBehaviour
    {
        [SerializeField]
        private Image healthbarImage;

        private EnemyBase targetEnemy;

        public void UpdateHealthBar()
        {
            healthbarImage.fillAmount = (float)(targetEnemy.CurrentHealth / targetEnemy.MaxHealth);
        }

        public class Factory : PlaceholderFactory<HealthDrawer>
        {
            public HealthDrawer Create(EnemyBase targetEnemy)
            {
                var newHealthBar = base.Create();
                newHealthBar.targetEnemy = targetEnemy;

                return newHealthBar;
            }
        }
    }
}