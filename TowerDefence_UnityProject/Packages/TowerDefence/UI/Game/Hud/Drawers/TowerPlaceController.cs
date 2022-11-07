using System.Collections.Generic;
using DataBinding;
using NoUtil.Extentsions;
using TowerDefence.UI.Models;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Game.Hud.Drawers
{
    public class TowerPlacerController
    {
        private const string containerId = "game_ui";
        private const string towerPlaceContainerId = "places_tower_container";

        private readonly BindingContext bindingContext = new();
        private IUIContainer activeContainer;
        private VisualElement towerPlaceContainer;

        public TowerPlacerController(IUIContainers uiContainers)
        {
            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainersChanged);
        }

        private void OnUIContainersChanged(IList<IUIContainer> uiContainers)
        {
            if (activeContainer is not null)
            {
                UnBind();
            }

            if (uiContainers.TryFind(x => x.Name == containerId, out var container) && container is UIDocumentContainer documentContainer)
            {
                var root = documentContainer.Document.rootVisualElement;
                towerPlaceContainer = root.Q(towerPlaceContainerId);
            }

            void UnBind()
            {
            }
        }
    }
}