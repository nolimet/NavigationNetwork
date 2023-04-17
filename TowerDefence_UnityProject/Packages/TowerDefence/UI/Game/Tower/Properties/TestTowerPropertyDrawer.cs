using System.Text;
using TMPro;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Game.Tower.Properties
{
    public class TestTowerPropertyDrawer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textfield;
        private TowerPropertiesExtractor propertiesExtractor;

        [Inject]
        public void Inject(TowerPropertiesExtractor propertiesExtractor)
        {
            this.propertiesExtractor = propertiesExtractor;
            propertiesExtractor.TowersChanged += OnTowersChanged;
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

            textfield.text = sb.ToString();
        }

        private void OnDestroy()
        {
            propertiesExtractor.TowersChanged -= OnTowersChanged;
            propertiesExtractor = null;
        }
    }
}
