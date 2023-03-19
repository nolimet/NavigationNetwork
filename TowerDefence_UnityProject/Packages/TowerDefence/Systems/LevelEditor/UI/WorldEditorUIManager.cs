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
        private const string UIDocumentId = "LevelEditorUI";

        private readonly BindingContext bindingContext = new();
        private BindingContext worldBindingContext = new();

        private readonly IUIContainers uiContainers;
        private readonly ILevelEditorModel levelEditorModel;

        private TextField widthInput;
        private TextField heightInput;

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
            worldBindingContext.Bind(world, x => x.Width, OnWorldSizeChanged);
            worldBindingContext.Bind(world, x => x.Height, OnWorldSizeChanged);
        }

        private void OnWorldSizeChanged(uint _)
        {
            var world = levelEditorModel.World;
            heightInput?.SetValueWithoutNotify(world.Height.ToString());
            widthInput?.SetValueWithoutNotify(world.Width.ToString());
        }

        private void OnUIContainerChanged(IList<IUIContainer> _)
        {
            if (!uiContainers.TryGetContainer(UIDocumentId, out AddressableUIDocumentContainer document)) return;

            var root = document.VisualRoot;

            var gridSize = root.Q("GridSize");
            widthInput = gridSize.Q<TextField>("Width");
            heightInput = gridSize.Q<TextField>("Height");

            if (levelEditorModel.World is not null)
            {
                var world = levelEditorModel.World;
                heightInput.value = world.Height.ToString();
                widthInput.value = world.Width.ToString();
            }

            heightInput.RegisterValueChangedCallback(OnHeightInputChanged);
            widthInput.RegisterValueChangedCallback(OnWidthInputChanged);
        }

        private void OnWidthInputChanged(ChangeEvent<string> evt)
        {
            if (evt.newValue == string.Empty) return;

            uint value = uint.TryParse(evt.newValue, out value) && value > 0 ? value : 1;
            levelEditorModel.World.Width = value;
        }

        private void OnHeightInputChanged(ChangeEvent<string> evt)
        {
            if (evt.newValue == string.Empty) return;

            uint value = uint.TryParse(evt.newValue, out value) && value > 0 ? value : 1;
            levelEditorModel.World.Height = value;
        }
    }
}