using DataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Towers.Components;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.Entities.Towers
{
    public class TowerObject : MonoBehaviour, ITowerObject
    {
        private readonly BindingContext bindingContext = new(true);

        private readonly List<ITickableTowerComponent> tickableComponents = new();

        public Transform Transform => transform;

        public string Name { get => this.name; set => this.name = value; }

        public ITowerModel Model { get; private set; }

        public Vector2 GetGridPosition() => throw new NotImplementedException("TODO Implement grid system");

        public Vector3 GetWorldPosition() => transform.position;

        public void Setup(ITowerModel model)
        {
            this.Model = model;

            bindingContext.Bind(model, m => m.Components, OnComponentsChanged);
        }

        private void OnComponentsChanged(IList<ITowerComponent> obj)
        {
            tickableComponents.Clear();
            tickableComponents.AddRange(obj.Where(x => x is ITickableTowerComponent).Cast<ITickableTowerComponent>().OrderBy(x => x.TickPriority));
        }

        public void Tick()
        {
            tickableComponents.ForEach(x => x.Tick());
        }

        private void OnDestroy()
        {
            bindingContext.Dispose();
        }
    }
}