﻿using System;
using System.Linq;
using TMPro;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Components;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;

namespace TowerDefence.UI.Hud
{
    public class TowerHudDrawer : MonoBehaviour
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
            var towerSettings = tower.Model.Components.FirstOrDefault(x => x is TowerSettings) as TowerSettings;

            rangeField.SetValue(towerSettings.Range.ToString());
            towerType.SetValue(towerSettings.Name);

            var damageComponent = tower.Model.Components.FirstOrDefault(x => x is IDamageComponent) as IDamageComponent;
            var damagePerSecond = damageComponent?.DamagePerSecond ?? 0d;
            damageField.SetValue(damagePerSecond.ToString("0.#"));
        }
    }
}