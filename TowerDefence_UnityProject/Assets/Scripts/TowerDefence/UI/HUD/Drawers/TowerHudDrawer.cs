using System.Linq;
using TMPro;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Components;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.UI.HUD
{
    public class TowerHudDrawer : MonoBehaviour
    {
        [SerializeField] private string rangeLabel, damageLabel, towerTypeLabel;
        [SerializeField] private BaseHudValueItem rangeField, damageField, towerType;

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetValues(ITowerObject tower)
        {
            rangeField.SetValue(tower.Model.Range.ToString());

            var damageComponent = tower.Model.Components.FirstOrDefault(x => x is IDamageComponent) as IDamageComponent;
            damageField.SetValue((damageComponent?.DamagePerSecond ?? 0d).ToString());

            towerType.SetValue(tower.Name);
        }
    }
}