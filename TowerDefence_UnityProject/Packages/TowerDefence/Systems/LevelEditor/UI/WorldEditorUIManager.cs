using System.Collections.Generic;
using DataBinding;
using TowerDefence.Systems.LevelEditor.Models;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Models;
using UnityEngine.UIElements;

namespace TowerDefence.Systems.LevelEditor.UI
{
    public class WorldEditorUIManager
    {
        private const string UIDocumentId = "";
        private const string gridWorldRootId = "";

        private readonly BindingContext bindingContext = new();
        private BindingContext worldBindingContext = new();

        private readonly IUIContainers uiContainers;
        private readonly ILevelEditorModel levelEditorModel;

        private TextField widthInput;
        private TextField heightInput;

        private VisualElement gridWorldRoot;

        public WorldEditorUIManager(IUIContainers uiContainers, ILevelEditorModel levelEditorModel)
        {
            this.uiContainers = uiContainers;
            this.levelEditorModel = levelEditorModel;

            bindingContext.Bind(uiContainers, x => x.Containers, OnUIContainerChanged);
            bindingContext.Bind(levelEditorModel, x => x.World, OnWorldChanged);
        }

        private void OnWorldChanged(IWorldLayoutModel world)
        {
            worldBindingContext?.Dispose();
            worldBindingContext = new BindingContext();
            if (world is null) return;
            worldBindingContext.Bind(world, x => x.Cells, OnGridCellsChanged);
        }

        private void OnGridCellsChanged(IList<ICellModel> cells)
        {
        }

        private void OnUIContainerChanged(IList<IUIContainer> _)
        {
            if (!uiContainers.TryGetContainer(UIDocumentId, out AddressableUIDocumentContainer document)) return;

            var root = document.VisualRoot;
            gridWorldRoot = root.Q(gridWorldRootId);
            gridWorldRoot.Clear();

            var widthInput = root.Q<TextField>("Width");
        }
    }
}