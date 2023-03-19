using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataBinding;
using TowerDefence.Systems.LevelEditor.Managers;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Models;
using UnityEngine.UIElements;

namespace TowerDefence.Systems.LevelEditor.UI
{
    public class LevelEditorUIController
    {
        private const string UIDocumentId = "LevelEditorUI";

        private readonly IUIContainers uiContainers;
        private readonly WorldEditorManager worldEditorManager;
        private readonly LevelEditorController levelEditorController;
        private readonly BindingContext uiContainerBindingContext = new();

        private BindingContext bindingContext;

        private Button updateGridButton;

        public LevelEditorUIController(IUIContainers uiContainers, LevelEditorController levelEditorController, WorldEditorManager worldEditorManager)
        {
            this.uiContainers = uiContainers;
            this.worldEditorManager = worldEditorManager;
            this.levelEditorController = levelEditorController;

            uiContainerBindingContext.Bind(uiContainers, x => x.Containers, OnContainersChanged);
        }

        private void OnContainersChanged(IList<IUIContainer> obj)
        {
            if (!uiContainers.TryGetContainer(UIDocumentId, out UIDocumentContainer container))
            {
                return;
            }

            bindingContext?.Dispose();
            bindingContext = new BindingContext();

            if (updateGridButton is not null)
            {
                updateGridButton.clicked -= OnUpdateGridClicked;
            }

            var root = container.VisualRoot;
            updateGridButton = root.Q<Button>("RebuildGrid");

            updateGridButton.clicked += OnUpdateGridClicked;
        }

        private void OnUpdateGridClicked()
        {
            updateGridButton.SetEnabled(false);
            worldEditorManager.RebuildWorld().ContinueWith(() => updateGridButton.SetEnabled(true)).Preserve().Forget();
        }
    }
}