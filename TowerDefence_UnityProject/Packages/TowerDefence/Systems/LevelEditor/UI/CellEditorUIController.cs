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
        private readonly IUIContainers _uiContainers;
        private readonly CellEditor _cellEditor;
        private readonly BindingContext _bindingContext = new();

        private TextField cellWeightTextField;

        public CellEditorUIController(IUIContainers uiContainers, CellEditor cellEditor)
        {
            _uiContainers = uiContainers;
            _cellEditor = cellEditor;

            _bindingContext.Bind(_uiContainers, x => x.Containers, OnUIContainersChanged);
        }

        private void OnUIContainersChanged(IList<IUIContainer> _)
        {
            if (!_uiContainers.TryGetContainer<UIDocumentContainer>("LevelEditorUI", out var uiDocument)) return;

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
                _cellEditor.ChangeCellsForSelection(value, true);
            }
            else if (changeEvent.newValue != string.Empty)
            {
                cellWeightTextField.SetValueWithoutNotify(changeEvent.previousValue);
            }
        }
    }
}