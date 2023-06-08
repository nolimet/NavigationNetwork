using System;
using System.Collections.Generic;
using DataBinding;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Game.Tower.Properties.Data;
using TowerDefence.UI.Game.Tower.Properties.Interfaces;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace TowerDefence.UI.Game.Tower.Properties
{
    public class TowerPropertyDrawer : MonoBehaviour
    {
        private const string containerId = "GameUI-HUD";
        private const string elementId = "Properties";

        private readonly BindingContext bindingContext = new();
        private readonly Dictionary<ITowerProperty, VisualElement> PropertyLookup = new();

        private TowerPropertiesExtractor propertiesExtractor;
        private IUIContainers uiContainers;
        private VisualElement propertiesUIContainer;

        [Inject]
        public void Inject(TowerPropertiesExtractor propertiesExtractor, IUIContainers uiContainers)
        {
            this.propertiesExtractor = propertiesExtractor;
            this.uiContainers = uiContainers;

            propertiesExtractor.TowersChanged += OnTowersChanged;
            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainersChanged);
        }

        private void OnUIContainersChanged(IList<IUIContainer> containers)
        {
            if (!uiContainers.TryGetContainer(containerId, out UIDocumentContainer uiContainer)) return;
            var root = uiContainer.VisualRoot;
            propertiesUIContainer = root.Q(elementId);
        }

        private void OnTowersChanged()
        {
            if (propertiesUIContainer is null) return;

            propertiesUIContainer.Clear();
            PropertyLookup.Clear();
            foreach (var tower in propertiesExtractor.Towers)
            {
                propertiesUIContainer.Add(new Label
                {
                    text = tower.Name,
                    style =
                    {
                        unityFontStyleAndWeight = FontStyle.Bold
                    }
                });

                foreach (var component in tower.Components)
                {
                    propertiesUIContainer.Add(new Label
                    {
                        text = $"{component.Name}",
                        style =
                        {
                            paddingLeft = 10,
                            unityFontStyleAndWeight = FontStyle.Bold
                        }
                    });

                    foreach (var property in component.Properties)
                    {
                        switch (property)
                        {
                            case TowerProperty tp:
                                var newLabel = new Label
                                {
                                    text = tp.GetValue(component.Component),
                                    style =
                                    {
                                        paddingLeft = 20
                                    }
                                };

                                propertiesUIContainer.Add(newLabel);
                                PropertyLookup.Add(property, newLabel);
                                break;

                            case TowerSliderProperty tsp:
                                var progressBar = new ProgressBar
                                {
                                    value = (float)tsp.GetSliderValue(component.Component),
                                    lowValue = (float)tsp.GetMinValue(component.Component),
                                    highValue = (float)tsp.GetMaxValue(component.Component),
                                    title = tsp.GetValue(component.Component),
                                    style =
                                    {
                                        paddingLeft = 20
                                    }
                                };
                                propertiesUIContainer.Add(progressBar);
                                PropertyLookup.Add(property, progressBar);
                                break;
                                throw new NotSupportedException(property.GetType().Name + " is not handled yet");
                        }
                    }
                }
            }
        }

        private void UpdateProperties()
        {
            foreach (var tower in propertiesExtractor.Towers)
            foreach (var (comp, properties) in tower.Components)
            foreach (var property in properties)
            {
                if (PropertyLookup.TryGetValue(property, out var visualElement))
                {
                    switch (property)
                    {
                        case TowerProperty tp:
                            var label = visualElement as Label;
                            label!.text = tp.GetValue(comp);
                            break;
                        case TowerSliderProperty tsp:
                            var slider = visualElement as ProgressBar;
                            slider!.value = (float)tsp.GetSliderValue(comp);
                            slider.lowValue = (float)tsp.GetMinValue(comp);
                            slider.highValue = (float)tsp.GetMaxValue(comp);
                            slider.title = tsp.GetValue(comp);
                            break;
                    }
                }
            }
        }

        private void Update()
        {
            //OnTowersChanged();
            UpdateProperties();
        }

        private void OnDestroy()
        {
            propertiesExtractor.TowersChanged -= OnTowersChanged;
            propertiesExtractor = null;
            bindingContext.Dispose();
        }
    }
}