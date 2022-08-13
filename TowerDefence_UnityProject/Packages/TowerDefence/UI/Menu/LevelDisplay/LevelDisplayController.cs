using System;
using System.Collections.Generic;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Systems.WorldLoader;
using TowerDefence.Systems.WorldLoader.Data;
using TowerDefence.UI.Models;
using TowerDefence.UI.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Menu.LevelDisplay
{
    internal class LevelDisplayController : IDisposable
    {
        private readonly WorldLoadController worldLoadController;
        private readonly BindingContext bindingContext = new(true);
        private UIDocumentContainer documentContainer;
        private VisualElement levelsContainer;

        private List<LevelSelectionButton> levelSelectionButtons = new();

        public LevelDisplayController(WorldLoadController worldLoadController, IUIContainers uiContainers)
        {
            this.worldLoadController = worldLoadController;

            bindingContext.Bind(uiContainers, x => x.Containers, OnContainersChanged);
        }

        private void OnContainersChanged(IList<IUIContainer> obj)
        {
            if (!obj.TryFind(x => x.Name == "Main", out var container) || container is not UIDocumentContainer uiDocumentContainer) return;
            documentContainer = uiDocumentContainer;
            DelayedUIUpdate();
        }

        private async void DelayedUIUpdate()
        {
            await new WaitForSeconds(.2f);
            UpdateUI();
        }

        private void UpdateUI()
        {
            var levels = LevelMetadata.LoadLevels();

            var document = documentContainer.Document.rootVisualElement;
            Debug.Log(documentContainer);
            Debug.Log(documentContainer.Document);
            Debug.Log(document);
            levelsContainer = document.Q("Levels")?.Q("unity-content-container");

            if (levelsContainer == null) throw new NullReferenceException();
            foreach (var level in levels)
            {
                levelsContainer.Add(CreateNewButton(level.DisplayName, level.RelativeLevelPath));
            }

            VisualElement CreateNewButton(string text, string relativePath)
            {
                var buttonElement = new LevelSelectionButton(relativePath)
                {
                    text = text
                };

                buttonElement.callback += OnButtonClicked;

                return buttonElement;
            }
        }

        private void OnButtonClicked(string relativePath)
        {
            worldLoadController.SetLevel(relativePath, LevelType.lvl);

            levelSelectionButtons.ForEach(x => x.SetEnabled(x.callbackValue != relativePath));

            levelsContainer.SetEnabled(false);
            levelsContainer.visible = false;
        }

        public void Dispose() => bindingContext.Dispose();
    }
}