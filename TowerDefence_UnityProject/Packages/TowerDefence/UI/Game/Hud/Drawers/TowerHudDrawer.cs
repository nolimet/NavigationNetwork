using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Components.BaseComponents;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Systems.Selection;
using TowerDefence.Utility;
using UnityEngine;

namespace TowerDefence.UI.Game.Hud
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

        public override void SetValue(ISelectable selectable)
        {
            if (selectable is not ITowerObject tower)
                return;

            var towerSettings = tower.Model.Components.GetComponent<TowerSettings>();

            rangeField.SetValue(towerSettings.Range.ToString());
            towerType.SetValue(towerSettings.Name);

            if (tower.Model.Components.TryGetComponent<IDamageComponent>(out var damageComponent))
            {
                var damagePerSecond = damageComponent?.DamagePerSecond ?? 0d;
                damageField.SetValue(damagePerSecond.ToString("0.#"));
            }
        }
    }
}