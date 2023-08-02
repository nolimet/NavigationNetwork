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
            const BindingFlags bindingFlags = BindingFlags.Default | BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var propertyInfos = type.GetProperties(bindingFlags);
            var fieldInfos = type.GetFields(bindingFlags);

            foreach (var propertyInfo in propertyInfos) towerProperties.Add(GetProperty(propertyInfo));
            foreach (var fieldInfo in fieldInfos) towerProperties.Add(GetProperty(fieldInfo));

            towerProperties = towerProperties.Where(x => x is not null).ToList();

            return new TowerComponent(component, towerProperties);

            ITowerProperty GetProperty(MemberInfo memberInfo)
            {
                if (memberInfo.HasAttribute(typeof(HiddenPropertyAttribute))) return null;
                var attrs = memberInfo.GetCustomAttributes(true);


                if (attrs.TryFind(x => x is ProgressBarPropertyAttribute, out var attr) && attr is ProgressBarPropertyAttribute ppa)
                {
                    if (!propertyInfos.TryFind(x => x.Name == ppa.MinValuePropertyName, out MemberInfo minInfo))
                        if (!fieldInfos.TryFind(x => x.Name == ppa.MinValuePropertyName, out minInfo))
                            minInfo = null;
                    if (!propertyInfos.TryFind(x => x.Name == ppa.MaxValuePropertyName, out MemberInfo maxInfo))
                        if (!fieldInfos.TryFind(x => x.Name == ppa.MaxValuePropertyName, out maxInfo))
                            maxInfo = null;

                    return new TowerSliderProperty(ppa.MinValue, ppa.MaxValue, minInfo, maxInfo, memberInfo);
                }

                if (MemberIsPublic(memberInfo))
                {
                    return new TowerProperty(memberInfo);
                }

                if (attrs.TryFind(x => x is UIPropertyAttribute, out attr) && attr is UIPropertyAttribute uiP)
                {
                    return new TowerProperty(memberInfo, uiP.PrettyName, uiP.Prefix, uiP.Suffix);
                }

                return null;
            }

            bool MemberIsPublic(MemberInfo info) =>
                info switch
                {
                    FieldInfo field => field.IsPublic,
                    PropertyInfo property => property.GetMethod.IsPublic,
                    _ => false
                };
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
        }
    }
}