using System.Collections.Generic;
using DataBinding;
using TowerDefence.Systems.LevelEditor.Managers;
using TowerDefence.UI.Containers;
using TowerDefence.UI.Models;
using UnityEngine.UIElements;

namespace TowerDefence.Systems.LevelEditor.UI
{
    public class CellEditorUIController
    {
        private readonly IUIContainers uiContainers;
        private readonly CellEditor cellEditor;
        private readonly BindingContext bindingContext = new();

        private TextField cellWeightTextField;

        public CellEditorUIController(IUIContainers uiContainers, CellEditor cellEditor)
        {
            this.uiContainers = uiContainers;
            this.cellEditor = cellEditor;

            bindingContext.Bind(this.uiContainers, x => x.Containers, OnUIContainersChanged);
        }

        private void OnUIContainersChanged(IList<IUIContainer> _)
        {
            if (!uiContainers.TryGetContainer<UIDocumentContainer>("LevelEditorUI", out var uiDocument)) return;

            cellWeightTextField?.UnregisterValueChangedCallback(OnCellWeightChanged);

            var root = uiDocument.VisualRoot;
            var cellSettings = root.Q("CellSettings");

            cellWeightTextField = cellSettings.Q<TextField>("CellWeight");
            cellWeightTextField.RegisterValueChangedCallback(OnCellWeightChanged);
        }

        private void OnCellWeightChanged(ChangeEvent<string> changeEvent)
        {
            if (byte.TryParse(changeEvent.newValue, out var value))
            {
                cellEditor.ChangeCellsForSelection(value, true);
            }
            else if (changeEvent.newValue != string.Empty)
            {
                cellWeightTextField.SetValueWithoutNotify(changeEvent.previousValue);
            }
        }
    }
}