﻿using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Utility;
using UnityEngine;

namespace TowerDefence.UI.Hud
{
    internal class TowerHudDrawer : HudDrawerBase<ITowerObject>
    {
        [SerializeField] private string rangeLabel, damageLabel, towerTypeLabel;
        [SerializeField] private BaseHudValueItem rangeField, damageField, towerType;

        private void Awake()
        {
            rangeField.SetLabel(rangeLabel);
            damageField.SetLabel(damageLabel);
            towerType.SetLabel(towerTypeLabel);
        }

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void SetValues(ITowerObject tower)
        {
            var towerSettings = tower.Model.Components.GetComponent<TowerSettings>();

            rangeField.SetValue(towerSettings.Range.ToString());
            towerType.SetValue(towerSettings.Name);

            var damageComponent = tower.Model.Components.GetComponent<IDamageComponent>();
            var damagePerSecond = damageComponent?.DamagePerSecond ?? 0d;
            damageField.SetValue(damagePerSecond.ToString("0.#"));
        }
    }
}