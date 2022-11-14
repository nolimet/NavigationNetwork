using System;
using System.Collections.Generic;
using DataBinding;
using TowerDefence.Input;
using TowerDefence.Packages.TowerDefence.SceneLoading;
using TowerDefence.UI.Models;
using TowerDefence.World.Grid;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.PauseMenu
{
    public sealed class PauseMenuController : IDisposable
    {
        private const string GameHUDContainerId = "GameUI-HUD";
        private const string PauseMenuId = "PopupMenu";
        private const string BackToMenuId = "BackToMenu";


        private readonly IUIContainers uiContainers;
        private readonly BindingContext bindingContext = new();
        private readonly UIInputActions uiInputActions;
        private readonly SceneReferences sceneReferences;
        private readonly GridWorld gridWorld;

        private VisualElement pauseMenu;
        private Button returnToMenuButton;

        internal PauseMenuController(IUIContainers uiContainers, UIInputActions uiInputActions, SceneReferences sceneReferences, GridWorld gridWorld)
        {
            this.uiContainers = uiContainers;
            this.uiInputActions = uiInputActions;
            this.sceneReferences = sceneReferences;
            this.gridWorld = gridWorld;

            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainerChanged);

            uiInputActions.Main.Enable();
            uiInputActions.Main.OpenPauseMenu.Enable();
            uiInputActions.Main.OpenPauseMenu.performed += OnOpenPauseMenuPressed;
        }

        private void OnOpenPauseMenuPressed(InputAction.CallbackContext obj)
        {
            pauseMenu.style.display = pauseMenu.style.display.value switch
            {
                DisplayStyle.Flex => DisplayStyle.None,
                DisplayStyle.None => DisplayStyle.Flex,
                _ => pauseMenu.style.display
            };
        }

        private void OnUIContainerChanged(IList<IUIContainer> _)
        {
            UnBind();
            Bind();

            void Bind()
            {
                if (!uiContainers.TryGetContainer(GameHUDContainerId, out UIDocumentContainer uiDocumentContainer)) return;

                var root = uiDocumentContainer.Document.rootVisualElement;

                pauseMenu = root.Q(PauseMenuId);
                returnToMenuButton = root.Q<Button>(BackToMenuId);
                returnToMenuButton.clicked += OnReturnToMenuClicked;
            }

            void UnBind()
            {
                if (returnToMenuButton is not null)
                {
                    returnToMenuButton.clicked -= OnReturnToMenuClicked;
                }
            }
        }

        private async void OnReturnToMenuClicked()
        {
            returnToMenuButton.SetEnabled(false);
            uiInputActions.Main.OpenPauseMenu.Disable();

            gridWorld.DestroyWorld();

            await sceneReferences.MainMenuScene.LoadSceneAsync();
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
        }
    }
}