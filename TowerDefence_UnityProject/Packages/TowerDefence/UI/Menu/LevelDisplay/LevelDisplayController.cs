using DataBinding;
using NoUtil.Extentsions;
using System;
using System.Collections.Generic;
using TowerDefence.Systems.WorldLoad;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

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
            if (obj.TryFind(x => x.Name == "Main", out var container) && container is UIDocumentContainer uiDocumentContainer)
            {
                documentContainer = uiDocumentContainer;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            
            var document = documentContainer.Document.rootVisualElement;
            var levelsContainer = document.Q("Levels")?.Q("unity-content-container");
            if (levelsContainer == null) throw new NullReferenceException();
            
            var newButton = new UnityEngine.UIElements.Button();
            levelsContainer.Add(newButton);
            Debug.Log(levelsContainer);

            VisualElement CreateNewButton(string text)
            {
                var buttonElement = new Button();//TODO add callback action
                var textElement = new TextElement
                {
                    text = text
                };

                buttonElement.Add(textElement);

                return buttonElement;
            }
        }

        public void Dispose() => bindingContext.Dispose();
    }
}