using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using TowerDefence.Input;
using TowerDefence.Systems.Selection.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TowerDefence.Systems.Selection
{
    public sealed class SelectionController
    {
        private readonly ISelectionModel selectionModel;
        private readonly SelectionInputActions selectionInput;
        private readonly List<ISelectable> selectionBuffer = new();
        private readonly Collider2D[] results = new Collider2D[256];

        public SelectionController(ISelectionModel selectionModel, SelectionInputActions selectionInput)
        {
            this.selectionModel = selectionModel;
            this.selectionInput = selectionInput;

            selectionInput.Enable();
            selectionInput.Main.Enable();
            selectionInput.Main.Click.Enable();
            selectionInput.Main.Drag.Enable();

            selectionInput.Main.Click.performed += OnClickPreformed;
            selectionInput.Main.Drag.performed += OnDragStarted;
            selectionInput.Main.Drag.canceled += OnDragEnded;
        }

        ~SelectionController()
        {
            selectionBuffer.Clear();
            selectionInput.Main.Click.performed -= OnClickPreformed;
            selectionInput.Main.Drag.performed -= OnDragStarted;
            selectionInput.Main.Drag.canceled -= OnDragEnded;
        }

        private void OnDragStarted(InputAction.CallbackContext obj)
        {
            selectionModel.DragStartPosition = selectionInput.Main.MousePosition.ReadValue<Vector2>();
            selectionModel.Dragging = true;
        }

        private void OnDragEnded(InputAction.CallbackContext obj)
        {
            selectionModel.DragEndPosition = selectionInput.Main.MousePosition.ReadValue<Vector2>();
            selectionModel.Dragging = false;

            SelectObject(selectionModel.DragStartPosition, selectionModel.DragEndPosition);
        }

        private void OnClickPreformed(InputAction.CallbackContext obj)
        {
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = selectionInput.Main.MousePosition.ReadValue<Vector2>()
            };

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);
            if (!raycastResults.Any())
            {
                SelectObject(selectionInput.Main.MousePosition.ReadValue<Vector2>());
            }
        }

        private void SelectObject(Vector2 cursorPosition)
        {
            if (Camera.main == null) return;

            var hitCount = Physics2D.OverlapPointNonAlloc(Camera.main.ScreenToWorldPoint(cursorPosition), results);

            selectionModel.Selection.Clear();
            if (hitCount == 0) return;

            for (int i = 0; i < hitCount; i++)
            {
                var result = results[i];
                result.GetComponentsInChildren(true, selectionBuffer);

                foreach (var item in selectionBuffer)
                    selectionModel.Selection.Add(item);
            }
        }

        private void SelectObject(Vector2 corner1, Vector2 corner2)
        {
            if (Camera.main == null) return;
            var camera = Camera.main;

            corner1 = camera.ScreenToWorldPoint(corner1);
            corner2 = camera.ScreenToWorldPoint(corner2);

            var max = Vector2.Max(corner1, corner2);
            var min = Vector2.Min(corner1, corner2);

            var hitCount = Physics2D.OverlapAreaNonAlloc(min, max, results);

            if (hitCount == 0) return;

            selectionModel.Selection.Clear();
            var newSelection = new List<ISelectable>();

            for (int i = 0; i < hitCount; i++)
            {
                var result = results[i];
                result.GetComponentsInChildren(true, selectionBuffer);

                newSelection.AddRange(selectionBuffer);
            }

            selectionModel.Selection.AddRange(newSelection);
        }
    }
}