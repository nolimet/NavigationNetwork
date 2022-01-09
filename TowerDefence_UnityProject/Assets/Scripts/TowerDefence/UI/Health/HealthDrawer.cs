using DataBinding;
using System;
using TowerDefence.Entities.Enemies;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowerDefence.UI.Health
{
    public class HealthDrawer : MonoBehaviour
    {
        [SerializeField]
        private Image healthbarImage;

        private IEnemyBaseModel target;

        private BindingContext bindingContext = new(true);

        private void Start()
        {
            bindingContext.Bind(target, x => x.health, OnHealthChanged);
        }

        private void OnDestroy()
        {
            bindingContext.Dispose();
        }

        private void OnHealthChanged(double _)
        {
            healthbarImage.fillAmount = (float)(target.health / target.maxHealth);
        }

        public void UpdateHealthBar()
        {
            if (target.obj)
            {
                transform.position = target.transform.position + target.healthOffset;
            }
        }

        public bool TargetIsAlive()
        {
            return target.obj;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<HealthDrawer>
        {
            [Inject]
            private UIContainer uiContainer;

            public HealthDrawer Create(IEnemyBaseModel target)
            {
                var newHealthBar = base.Create();

                newHealthBar.transform.SetParent(uiContainer.WorldUIContainer, false);
                newHealthBar.target = target;

                return newHealthBar;
            }
        }
    }
}