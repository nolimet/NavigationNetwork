using DataBinding;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Towers;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        private readonly BindingContext bindingContext = new();
        private TowerHudDrawer towerHud;

        [Inject]
        public void Inject(ISelectionModel selectionModel, TowerHudDrawer towerHud)
        {
            this.towerHud = towerHud;

            bindingContext.Bind(selectionModel, m => m.Selection, OnSelectionChanged);
        }

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            if (!selection.Any())
            {
                towerHud.SetActive(false);
                return;
            }

            var first = selection.First();

            if (first is ITowerObject tower)
            {
                towerHud.SetActive(true);
                towerHud.SetValues(tower);
            }
        }

        private void OnEnable()
        {
            bindingContext.Enable();
        }

        private void OnDisable()
        {
            bindingContext.Disable();
        }

        private void OnDestroy()
        {
            bindingContext.Dispose();
        }
    }
}