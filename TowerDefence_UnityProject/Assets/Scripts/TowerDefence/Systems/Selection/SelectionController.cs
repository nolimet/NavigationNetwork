﻿using NoUtil.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefence.Input;
using TowerDefence.Systems.Selection.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TowerDefence.Systems.Selection
{
    public class SelectionController
    {
        private readonly ISelectionModel selectionModel;
        private readonly SelectionInputActions selectionInput;
        private readonly List<ISelectable> selectionBuffer = new List<ISelectable>();

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

        private void OnDragEnded(InputAction.CallbackContext obj)
        {
            selectionInput.Main.MousePosition.ReadValue<Vector2>().QuickLog("Drag Ended");
        }

        private void OnDragStarted(InputAction.CallbackContext obj)
        {
            selectionInput.Main.MousePosition.ReadValue<Vector2>().QuickLog("Drag Started");
        }

        private void OnClickPreformed(InputAction.CallbackContext obj)
        {
            selectionInput.Main.MousePosition.ReadValue<Vector2>().QuickLog("Click");
            SelectObject(selectionInput.Main.MousePosition.ReadValue<Vector2>());
        }

        private void SelectObject(Vector2 cursorPosition)
        {
            var results = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(cursorPosition));
            results.Count().QuickLog("Click Results");
            selectionModel.Selection.Clear();
            if (results != null && results.Length > 0)
            {
                foreach (var result in results)
                {
                    result.GetComponentsInChildren(true, selectionBuffer);

                    foreach (var item in selectionBuffer)
                        selectionModel.Selection.Add(item);
                }
            }
        }

        private void SelectObject(Vector2 corner1, Vector2 corner2)
        {
        }
    }
}