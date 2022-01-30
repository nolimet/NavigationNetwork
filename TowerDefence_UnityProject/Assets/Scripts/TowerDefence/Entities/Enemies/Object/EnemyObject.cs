using DataBinding;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Enemies.Components;
using TowerDefence.Entities.Enemies.Models;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefence.Entities.Enemies
{
    public class EnemyObject : MonoBehaviour, IEnemyObject
    {
        public Transform Transform => transform;
        public IEnemyModel Model { get; private set; }
        public string Name { get => this.name; set => this.name = value; }

        public UnityAction<IEnemyObject> DeathAction { get; private set; }

        private readonly List<ITickableEnemyComponent> tickableComponents = new();
        private readonly BindingContext bindingContext = new(true);
        

        [SerializeField] private Vector2 healthbarOffset = Vector2.zero;

        public void Damage(double damage)
        {
            Model.Health -= damage;
        }

        public float DistanceToTarget() => 0f;

        public Vector3 GetWorldPosition() => transform.position;

        public void Setup(IEnemyModel enemyModel, UnityAction<IEnemyObject> outOfHealthAction)
        {
            this.Model = enemyModel;
            Model.HealthOffset = healthbarOffset;
            this.DeathAction = outOfHealthAction;

            bindingContext.Bind(enemyModel, m => m.Components, OnComponentsChanged);
            bindingContext.Bind(enemyModel, m => m.Health, OnHealthChanged);
        }

        private void OnHealthChanged(double health)
        {
            if (health <= 0)
            {
                DeathAction?.Invoke(this);
            }
        }

        private void OnComponentsChanged(IList<IEnemyComponent> components)
        {
            tickableComponents.Clear();
            tickableComponents.AddRange(components.Where(x => x is ITickableEnemyComponent).Cast<ITickableEnemyComponent>().OrderBy(x => x.TickPriority));
        }

        public void Tick()
        {
            tickableComponents.ForEach(x => x.Tick());
        }

        private void OnDestroy()
        {
            Model.HealthBar.Destroy();
            bindingContext.Dispose();
        }
    }
}