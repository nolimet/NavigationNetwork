using System.Collections.Generic;
using System.Text;
using DataBinding;
using TMPro;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace TowerDefence.UI.Game.Tower.Properties
{
    public class TestTowerPropertyDrawer : MonoBehaviour
    {
        private const string containerId = "GameUI-HUD";
        private const string elementId = "PropertiesInfo";

        private readonly BindingContext bindingContext = new();

        [SerializeField] private TextMeshProUGUI textfield;
        private TowerPropertiesExtractor propertiesExtractor;
        private IUIContainers uiContainers;
        private Label propertiesUI;

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
            propertiesUI = root.Q<Label>(elementId);
        }

        private void OnTowersChanged()
        {
            StringBuilder sb = new();

            foreach (var tower in propertiesExtractor.Towers)
            {
                sb.AppendLine($"{tower.Name}:");
                foreach (var component in tower.Components)
                {
                    sb.AppendLine($"\t{component.Name}");
                    foreach (var property in component.Properties) sb.AppendLine($"\t\t{property.Label} : {property.GetValue(component.Component)}");
                }
            }

            if (propertiesUI is not null) propertiesUI.text = sb.ToString();
            textfield.text = sb.ToString();
        }

        private void Update()
        {
            OnTowersChanged();
        }

        private void OnDestroy()
        {
            propertiesExtractor.TowersChanged -= OnTowersChanged;
            propertiesExtractor = null;
            bindingContext.Dispose();
        }
    }
}