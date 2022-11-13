using System.Collections.Generic;
using DataBinding;
using TowerDefence.Input;
using TowerDefence.UI.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.PauseMenu
{
    public class PauseMenuController
    {
        private const string GameHUDContainerId = "GameUI-HUD";
        private const string PauseMenuId = "PopupMenu";

        private readonly IUIContainers uiContainers;
        private readonly BindingContext bindingContext = new();
        private readonly UIInputActions uiInputActions;

        private VisualElement pauseMenu;

        public PauseMenuController(IUIContainers uiContainers, UIInputActions uiInputActions)
        {
            Debug.Log("Bleep bloop");
            this.uiContainers = uiContainers;
            this.uiInputActions = uiInputActions;

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
            }

            void UnBind()
            {
            }
        }
    }
}