using System;
using System.Collections.Generic;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.Packages.TowerDefence.SceneLoading;
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
        private readonly SceneReferences sceneReferences;
        private Button loadLevelButton;

        public LevelDisplayController(IWorldDataModel worldDataModel, IUIContainers uiContainers, SceneReferences sceneReferences)
        {
            this.worldDataModel = worldDataModel;
            this.sceneReferences = sceneReferences;
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
            levelsContainer = document.Q<ListView>("Levels").Q("unity-content-container");

            if (levelsContainer is null) throw new NullReferenceException();
            foreach (var level in levels)
            {
                var newButton = CreateNewButton(level.DisplayName, level.RelativeLevelPath);
                levelSelectionButtons.Add(newButton);
                levelsContainer.contentContainer.Add(newButton);
            }


            loadLevelButton = document.Q<Button>("LoadLevelButton");
            loadLevelButton.clicked += OnLoadLevelClicked;
            loadLevelButton.SetEnabled(false);

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

        private async void OnLoadLevelClicked()
        {
            levelsContainer.SetEnabled(false);
            await sceneReferences.GameScene.LoadSceneAsync();
        }

        private void OnButtonClicked(string relativePath)
        {
            worldDataModel.LevelName = relativePath;
            worldDataModel.LevelType = LevelType.lvl;

            foreach (var x in levelSelectionButtons)
            {
                x?.SetEnabled(x.CallbackValue != relativePath);
            }
            loadLevelButton.SetEnabled(true);
        }

        public void Dispose() => bindingContext.Dispose();
    }
}