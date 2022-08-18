using System;
using System.Collections.Generic;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefence.UI.MainMenu
{
    public class MainMenuController
    {
        private readonly IUIContainers uiContainers;
        private readonly BindingContext bindingContext = new();

        private UIDocumentContainer boundDocumentContainer;
        private VisualElement levelContainer;
        private VisualElement mainMenuContainer;

        public MainMenuController(IUIContainers uiContainers)
        {
            this.uiContainers = uiContainers;

            bindingContext.Bind(uiContainers, x=>x.Containers, OnContainerChanged);
        }

        ~MainMenuController()
        {
            bindingContext.Dispose();
        }

        private void OnContainerChanged(IList<IUIContainer> obj)
        {
            if (obj.TryFind(x => x.Name == "Main", out IUIContainer container) && container is UIDocumentContainer documentContainer)
            {
                if (boundDocumentContainer == documentContainer) return;
                boundDocumentContainer = documentContainer;
                
                var root = documentContainer.Document.rootVisualElement;
                
                var levelSelectionButton = root.Q<Button>("ToLevelSelectionButton");
                levelSelectionButton.clicked += OnLevelSelectionButtonClicked;

                var exitGameButton = root.Q<Button>("ExitGameButton");
                exitGameButton.clicked += OnExitButtonClicked;

                 levelContainer = root.Q("LevelSelection");
                 mainMenuContainer = root.Q("StartUpMenu");
            }
        }

        private void OnExitButtonClicked()
        {
            Application.Quit();
        }

        private void OnLevelSelectionButtonClicked()
        {
            levelContainer.style.display = DisplayStyle.Flex;
            mainMenuContainer.style.display = DisplayStyle.None;
        }
    }
}