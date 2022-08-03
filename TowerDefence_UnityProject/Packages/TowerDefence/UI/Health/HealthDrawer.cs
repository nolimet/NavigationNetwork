using DataBinding;
using TowerDefence.Entities.Enemies;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace TowerDefence.UI.Health
{
    public class HealthDrawer : MonoBehaviour
    {
        [SerializeField]
        private Image healthbarImage;

        private IEnemyObject target;
        private UnityAction<HealthDrawer> destroyedAction;

        private readonly BindingContext bindingContext = new(true);

        private void Start()
        {
            bindingContext.Bind(target.Model, x => x.Health, OnHealthChanged);
        }

        private void OnDestroy()
        {
            bindingContext.Dispose();
        }

        private void OnHealthChanged(double _)
        {
            healthbarImage.fillAmount = (float)(target.Model.Health / target.Model.MaxHealth);
        }

        public void UpdateHealthBar()
        {
            transform.position = target.Transform.position + target.Model.HealthOffset;
        }

        public void Destroy()
        {
            if (gameObject)
                Destroy(gameObject);
            destroyedAction?.Invoke(this);
        }

        public class Factory : PlaceholderFactory<HealthDrawer>
        {
            [Inject]
            private UIContainer uiContainer;

            public HealthDrawer Create(IEnemyObject target, UnityAction<HealthDrawer> destroyedAction)
            {
                var newHealthBar = base.Create();

                newHealthBar.transform.SetParent(uiContainer.WorldUIContainer, false);
                newHealthBar.target = target;
                newHealthBar.destroyedAction = destroyedAction;

                return newHealthBar;
            }
        }
    }
}