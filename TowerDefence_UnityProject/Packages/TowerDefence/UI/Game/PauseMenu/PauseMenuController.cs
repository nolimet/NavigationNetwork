using System;
using System.Collections.Generic;
using DataBinding;
using TowerDefence.Input;
using TowerDefence.Packages.TowerDefence.SceneLoading;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Models;
using TowerDefence.World.Grid;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.PauseMenu
{
    public sealed class PauseMenuController : IDisposable
    {
        private const string GameHUDContainerId = "GameUI-PauseMenu";
        private const string PauseMenuId = "PopupMenu";
        private const string BackToMenuId = "BackToMenu";

        private readonly IUIContainers uiContainers;
        private readonly BindingContext bindingContext = new();
        private readonly InputActions _inputActions;
        private readonly SceneReferences sceneReferences;
        private readonly GridWorld gridWorld;

        private VisualElement pauseMenu;
        private Button returnToMenuButton;

        internal PauseMenuController(IUIContainers uiContainers, InputActions inputActions, SceneReferences sceneReferences, GridWorld gridWorld)
        {
            this.uiContainers = uiContainers;
            _inputActions = inputActions;
            this.sceneReferences = sceneReferences;
            this.gridWorld = gridWorld;

            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainerChanged);

            inputActions.UI.Enable();
            inputActions.UI.OpenPauseMenu.Enable();
            inputActions.UI.OpenPauseMenu.performed += OnOpenPauseMenuPressed;
        }

        private void OnOpenPauseMenuPressed(InputAction.CallbackContext obj)
        {
            if (pauseMenu is null)
            {
                Debug.LogError("No pause-menu");
                return;
            }

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

                var root = uiDocumentContainer.VisualRoot;
                if (root is null) return;

                pauseMenu = root.Q(PauseMenuId);

                pauseMenu.style.display = DisplayStyle.None;

                returnToMenuButton = root.Q<Button>(BackToMenuId);
                if (returnToMenuButton is not null)
                {
                    returnToMenuButton.clicked += OnReturnToMenuPressed;
                }
            }

            void UnBind()
            {
                if (returnToMenuButton is not null)
                {
                    returnToMenuButton.clicked -= OnReturnToMenuPressed;
                }
            }
        }

        private async void OnReturnToMenuPressed()
        {
            returnToMenuButton.SetEnabled(false);
            _inputActions.UI.OpenPauseMenu.Disable();

            gridWorld.DestroyWorld();

            await sceneReferences.LoadMainMenuScene();
        }

        public void Dispose()
        {
            bindingContext?.Dispose();
            _inputActions.UI.OpenPauseMenu.performed -= OnOpenPauseMenuPressed;
            if (returnToMenuButton is not null)
            {
                returnToMenuButton.clicked -= OnReturnToMenuPressed;
                returnToMenuButton.SetEnabled(true);
            }

            Debug.Log($"Disposed of {GetType()}");
        }
    }
}