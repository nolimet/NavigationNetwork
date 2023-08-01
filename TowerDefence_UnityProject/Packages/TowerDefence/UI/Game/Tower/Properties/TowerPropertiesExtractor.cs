using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataBinding;
using ModestTree;
using NoUtil.Extentsions;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Components.PowerComponents;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Game.Tower.Properties.Attributes;
using TowerDefence.UI.Game.Tower.Properties.Data;
using TowerDefence.UI.Game.Tower.Properties.Interfaces;

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
            var towerProperties = new List<ITowerProperty>();
            var type = component.GetType();
            const BindingFlags bindingFlags = BindingFlags.Default | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public;

            var propertyInfos = type.GetProperties(bindingFlags);
            var fieldInfos = type.GetFields(bindingFlags);

            foreach (var propertyInfo in propertyInfos) towerProperties.Add(GetProperty(propertyInfo));
            foreach (var fieldInfo in fieldInfos) towerProperties.Add(GetProperty(fieldInfo));

            towerProperties = towerProperties.Where(x => x is not null).ToList();

            return new TowerComponent(component, towerProperties);

            ITowerProperty GetProperty(MemberInfo memberInfo)
            {
                var atts = memberInfo.GetCustomAttributes(true);
                if (memberInfo.HasAttribute(typeof(HiddenPropertyAttribute))) return null;

                if (memberInfo.HasAttribute(typeof(UIPropertyAttribute))) return null; //TODO implement when having way to validate if member is private or not

                if (atts.TryFind(x => x is ProgressBarPropertyAttribute, out var att) && att is ProgressBarPropertyAttribute ppa)
                {
                    var hasMinAtt = propertyInfos.TryFind(x => x.Name == ppa.MinValuePropertyName, out MemberInfo minInfo)
                                    || fieldInfos.TryFind(x => x.Name == ppa.MinValuePropertyName, out minInfo);
                    var hasMaxAtt = propertyInfos.TryFind(x => x.Name == ppa.MaxValuePropertyName, out MemberInfo maxInfo) ||
                                    fieldInfos.TryFind(x => x.Name == ppa.MaxValuePropertyName, out maxInfo);

                    return new TowerSliderProperty(ppa.MinValue, ppa.MaxValue, minInfo, maxInfo, memberInfo);
                }

                return new TowerProperty(memberInfo);
            }
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
        }
    }
}