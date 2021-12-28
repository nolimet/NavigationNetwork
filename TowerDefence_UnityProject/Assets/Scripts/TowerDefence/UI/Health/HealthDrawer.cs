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
            if (targetEnemy)
            {
                healthbarImage.fillAmount = (float)(targetEnemy.CurrentHealth / targetEnemy.MaxHealth);
                transform.position = targetEnemy.transform.position + targetEnemy.HealthBarOffset;
            }
        }

        public bool TargetIsAlive()
        {
            return targetEnemy;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<HealthDrawer>
        {
            [Inject]
            private UIContainer uiContainer;

            public HealthDrawer Create(EnemyBase targetEnemy)
            {
                var newHealthBar = base.Create();

                newHealthBar.transform.SetParent(uiContainer.WorldUIContainer, false);
                newHealthBar.targetEnemy = targetEnemy;

                return newHealthBar;
            }
        }
    }
}