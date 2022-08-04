using DataBinding;
using NoUtil.Extentsions;
using System;
using System.Collections.Generic;
using TowerDefence.Systems.WorldLoad;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Menu.LevelDisplay
{
    internal class LevelDisplayController : IDisposable
    {
        private readonly WorldLoadController worldLoadController;
        private readonly BindingContext bindingContext = new(true);
        private UIDocumentContainer documentContainer;

        public LevelDisplayController(WorldLoadController worldLoadController, IUIContainers uiContainers)
        {
            this.worldLoadController = worldLoadController;

            bindingContext.Bind(uiContainers, x => x.Containers, OnContainersChanged);
        }

        private void OnContainersChanged(IList<IUIContainer> obj)
        {
            if (obj.TryFind(x => x.Name == "Main", out var container) && container is UIDocumentContainer documentContainer)
            {
                this.documentContainer = documentContainer;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            var document = documentContainer.Document.rootVisualElement;
            var levelsContainer = document.Q("Levels")?.Q("unity-content-container");

            Debug.Log(levelsContainer);
        }

        public void Dispose() => bindingContext.Dispose();
    }
}