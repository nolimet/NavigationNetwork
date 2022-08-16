using System;
using System.Collections.Generic;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Systems.WorldLoader;
using TowerDefence.Systems.WorldLoader.Data;
using TowerDefence.Systems.WorldLoader.Models;
using TowerDefence.UI.MainMenu.UIElements;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefence.UI.MainMenu.LevelDisplay
{
    internal class LevelDisplayController : IDisposable
    {
        private readonly IWorldDataModel worldDataModel;
        private readonly BindingContext bindingContext = new(true);
        private UIDocumentContainer documentContainer;
        private VisualElement levelsContainer;

        private readonly List<LevelSelectionButton> levelSelectionButtons = new();

        public LevelDisplayController(IWorldDataModel worldDataModel, IUIContainers uiContainers)
        {
            this.worldDataModel = worldDataModel;

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
            levelsContainer = document.Q("Levels")?.Q("unity-content-container");

            if (levelsContainer == null) throw new NullReferenceException();
            foreach (var level in levels)
            {
                var newButton = CreateNewButton(level.DisplayName, level.RelativeLevelPath);
                levelSelectionButtons.Add(newButton);
                levelsContainer.Add(newButton);
            }

            LevelSelectionButton CreateNewButton(string text, string relativePath)
            {
                var buttonElement = new LevelSelectionButton(relativePath)
                {
                    text = text
                };

                buttonElement.OnCallback += OnButtonClicked;

                return buttonElement;
            }
        }

        private void OnButtonClicked(string relativePath)
        {
            worldDataModel.LevelName = relativePath;
            worldDataModel.LevelType = LevelType.lvl;

            foreach (var x in levelSelectionButtons)
            {
                x.SetEnabled(x.CallbackValue != relativePath);
            }
        }

        public void Dispose() => bindingContext.Dispose();
    }
}