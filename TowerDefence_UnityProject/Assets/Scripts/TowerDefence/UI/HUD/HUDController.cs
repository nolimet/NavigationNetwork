using DataBinding;
using System.Collections.Generic;
using TowerDefence.Systems.Selection;
using TowerDefence.Systems.Selection.Models;
using UnityEngine;
using Zenject;

namespace TowerDefence.UI.HUD
{
    public class HUDController : MonoBehaviour
    {
        private ISelectionModel selectionModel;
        private readonly BindingContext bindingContext = new();

        private TowerHudDrawer towerHud;

        [Inject]
        public void Inject(ISelectionModel selectionModel)
        {
            this.selectionModel = selectionModel;
            bindingContext.Bind(selectionModel, m => m.Selection, OnSelectionChanged);
        }

        private void OnSelectionChanged(IList<ISelectable> obj)
        {
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