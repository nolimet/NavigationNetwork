using System.Collections.Generic;
using System.Linq;
using DataBinding;
using TowerDefence.Entities.Towers.Models;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.UI.Game.Hud_old.Drawers;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Game.Hud_old
{
    public sealed class HudController : MonoBehaviour
    {
        private readonly BindingContext bindingContext = new();

        [SerializeField] private HudDrawerBase[] hudDrawers;

        [Inject]
        public void Inject(ISelectionModel selectionModel, ITowerModels towerModel)
        {
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
                drawer.SetValue(selected);
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