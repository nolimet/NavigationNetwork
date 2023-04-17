using System;
using System.Collections.Generic;
using System.Reflection;
using DataBinding;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.PowerComponents;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Game.Tower.Properties.Data;

namespace TowerDefence.UI.Game.Tower.Properties
{
    public class TowerPropertiesExtractor : IDisposable
    {
        public event Action TowersChanged;

        private readonly BindingContext bindingContext = new();
        private readonly List<Data.Tower> towers = new();

        public IReadOnlyList<Data.Tower> Towers => towers;

        public TowerPropertiesExtractor(ISelectionModel selectionModel)
        {
            bindingContext.Bind(selectionModel, x => x.Selection, OnSelectionChanged);
        }

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            towers.Clear();
            foreach (var selectable in selection)
                if (selectable is ITowerObject towerObject)
                    towers.Add(ExtractTower(towerObject.Model, towerObject.Name));

            TowersChanged?.Invoke();
        }

        private Data.Tower ExtractTower(ITowerModel towerModel, string name)
        {
            List<TowerComponent> towerComponents = new();
            foreach (var component in towerModel.Components)
            {
                if (component is ITargetFindComponent or IAttackVisualizer or TowerVisualSettings or PowerVisualDrawer or IPowerTargetFinder) continue;

                towerComponents.Add(ExtractTowerComponent(component));
            }

            return new Data.Tower(name, towerModel, towerComponents);
        }

        private TowerComponent ExtractTowerComponent(IComponent component)
        {
            var towerProperties = new List<TowerProperty>();
            var type = component.GetType();
            const BindingFlags bindingFlags = BindingFlags.Default | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public;

            var propertyInfos = type.GetProperties(bindingFlags);
            var fieldInfos = type.GetFields(bindingFlags);

            foreach (var propertyInfo in propertyInfos) towerProperties.Add(new TowerProperty(propertyInfo));
            foreach (var fieldInfo in fieldInfos) towerProperties.Add(new TowerProperty(fieldInfo));

            return new TowerComponent(component, towerProperties);
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
        }
    }

    namespace Data
    {
        public record Tower(string Name, ITowerModel Model, IReadOnlyList<TowerComponent> Components)
        {
            public string Name { get; } = Name;
            public ITowerModel Model { get; } = Model;
            public IReadOnlyList<TowerComponent> Components { get; } = Components;
        }

        public record TowerComponent(IComponent Component, IReadOnlyList<TowerProperty> Properties)
        {
            public string Name { get; } = Component.GetType().Name;
            public IComponent Component { get; } = Component;
            public IReadOnlyList<TowerProperty> Properties { get; } = Properties;
        }

        public readonly struct TowerProperty
        {
            private readonly PropertyInfo propertyInfo;
            private readonly FieldInfo fieldInfo;
            public readonly string Label;

            public string GetValue(IComponent component)
            {
                return (propertyInfo?.GetValue(component) ?? fieldInfo?.GetValue(component))?.ToString() ?? "null";
            }

            public TowerProperty(MemberInfo memberInfo)
            {
                fieldInfo = null;
                propertyInfo = null;
                switch (memberInfo)
                {
                    case PropertyInfo info:
                        propertyInfo = info;
                        break;
                    case FieldInfo info:
                        fieldInfo = info;
                        break;
                }

                Label = memberInfo.Name;
            }
        }
    }
}
