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

        private IEnemyModel target;

        private BindingContext bindingContext = new(true);

        private void Start()
        {
            bindingContext.Bind(target, x => x.Health, OnHealthChanged);
        }

        private void OnDestroy()
        {
            bindingContext.Dispose();
        }

        private void OnHealthChanged(double _)
        {
            healthbarImage.fillAmount = (float)(target.Health / target.MaxHealth);
        }

        public void UpdateHealthBar()
        {
            if (target.Obj)
            {
                transform.position = target.Transform.position + target.HealthOffset;
            }
        }

        public bool TargetIsAlive()
        {
            return target.Obj;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<HealthDrawer>
        {
            [Inject]
            private UIContainer uiContainer;

            public HealthDrawer Create(IEnemyModel target)
            {
                var newHealthBar = base.Create();

                newHealthBar.transform.SetParent(uiContainer.WorldUIContainer, false);
                newHealthBar.target = target;

                return newHealthBar;
            }
        }
    }
}