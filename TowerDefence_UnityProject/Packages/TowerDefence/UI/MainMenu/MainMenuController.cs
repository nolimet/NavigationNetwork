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

        private async void OnContainerChanged(IList<IUIContainer> obj)
        {
            await new WaitForEndOfFrame();
            
            if (!obj.TryFind(x => x.Name == "Main", out var container) ||
                container is not UIDocumentContainer documentContainer) return;
            
            if (boundDocumentContainer == documentContainer) return;
            boundDocumentContainer = documentContainer;

            Debug.Log(documentContainer);
            Debug.Log(documentContainer.Document);
            Debug.Log(documentContainer.Document.rootVisualElement);
            
            var root = documentContainer.Document.rootVisualElement;
            mainMenuContainer = root.Q("StartUpMenu");
            levelContainer = root.Q("LevelSelection");
            
            var levelSelectionButton = mainMenuContainer.Q<Button>("ToLevelSelectionButton");
            levelSelectionButton.clicked += OnLevelSelectionButtonClicked;

            var exitGameButton = mainMenuContainer.Q<Button>("ExitGameButton");
            exitGameButton.clicked += OnExitButtonClicked;

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