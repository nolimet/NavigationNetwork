using DataBinding;
using System.Collections.Generic;
using System.Linq;
using TowerDefence.Entities.Towers;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Hud.PlaceTower;
using TowerDefence.World.Grid;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Hud
{
    public class HudController : MonoBehaviour
    {
        private readonly BindingContext bindingContext = new();
        private ITowerModels towerModels;

        [SerializeField] private HudDrawerBase[] hudDrawers;

        [Inject]
        public void Inject(ISelectionModel selectionModel, ITowerModels towerModel)
        {
            this.towerModels = towerModel;

            bindingContext.Bind(selectionModel, m => m.Selection, OnSelectionChanged);
        }

        private void OnSelectionChanged(IList<ISelectable> selection)
        {
            if (!selection.Any())
            {
                foreach (var drawer in hudDrawers)
                {
                    drawer.SetActive(false);
                }
                return;
            }

            var selected = selection.First();
            for (int i = 0; i < hudDrawers.Length; i++)
            {
                var drawer = hudDrawers[i];
                bool active = drawer.DrawsType(selected);
                drawer.SetActive(active);
                if (active)
                {
                    switch (drawer)
                    {
                        case TowerHudDrawer towerHud:
                            if (selected is ITowerObject towerObject)
                                towerHud.SetValues(towerObject);
                            else
                                towerHud.SetActive(false);
                            break;

                        case TowerPlaceHudDrawer towerPlaceHud:
                            if (selected is SelectableCell selectableCell && !towerModels.CellHasTower(selectableCell.GridCell))
                                towerPlaceHud.selectedCell = selectableCell;
                            else
                                towerPlaceHud.gameObject.SetActive(false);
                            break;
                    }
                }
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