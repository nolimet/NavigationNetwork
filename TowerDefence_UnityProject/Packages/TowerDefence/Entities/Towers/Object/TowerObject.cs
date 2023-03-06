using System.Collections.Generic;
using System.Linq;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.World.Grid.Data;
using UnityEngine;

namespace TowerDefence.Entities.Towers
{
    public sealed class TowerObject : MonoBehaviour, ITowerObject
    {
        private readonly BindingContext bindingContext = new();

        private readonly List<ITickableComponent> tickableComponents = new();

        public Transform Transform => transform;
        public bool ExistsInWorld => gameObject;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public ITowerModel Model { get; private set; }

        internal IGridCell OccupiedCell { get; private set; }

        public Vector2Int GetGridPosition() => OccupiedCell?.Position ?? Vector2Int.zero;

        public Vector3 GetWorldPosition() => transform.position;

        internal void Setup(ITowerModel model, IGridCell cell)
        {
            Model = model;
            OccupiedCell = cell;
            cell.SetStructure(true);

            bindingContext.Bind(model, m => m.Components, OnComponentsChanged);
        }

        private void OnComponentsChanged(IList<IComponent> obj)
        {
            tickableComponents.Clear();
            tickableComponents.AddRange(obj.Where(x => x is ITickableComponent).Cast<ITickableComponent>().OrderBy(x => x.TickPriority));

            if (obj.TryFind(x => x is TowerSettings, out var component) && component is TowerSettings towerSettings)
            {
                OccupiedCell.SetStructure(towerSettings.IsSolid);
            }
        }

        public void Tick()
        {
            tickableComponents.ForEach(x => x.Tick());
        }

        private void OnDestroy()
        {
            OccupiedCell.SetStructure(false);
            bindingContext.Dispose();
        }
    }
}