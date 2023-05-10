using System;
using System.Collections.Generic;
using System.Linq;
using DataBinding;
using Sirenix.Utilities;
using TowerDefence.Input;
using TowerDefence.Systems.CameraManager;
using TowerDefence.Systems.Selection.Models;
using TowerDefence.World.Grid;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace TowerDefence.Systems.Selection
{
    public sealed class SelectionController : IDisposable
    {
        private readonly ISelectionModel selectionModel;
        private readonly ICameraContainer cameraContainer;

        private readonly InputActions inputActions;
        private readonly List<ISelectable> selectionBuffer = new();
        private readonly Collider2D[] results = new Collider2D[8192];

        private readonly BindingContext bindingContext = new();

        private Camera mainCamera;

        public SelectionController(ICameraContainer cameraContainer, ISelectionModel selectionModel, InputActions inputActions)
        {
            this.selectionModel = selectionModel;
            this.inputActions = inputActions;
            this.cameraContainer = cameraContainer;

            inputActions.Enable();
            inputActions.Main.Enable();
            inputActions.Main.LeftClick.Enable();
            inputActions.Main.Drag.Enable();

            inputActions.Main.LeftClick.performed += OnClickPreformed;
            inputActions.Main.Drag.performed += OnDragStarted;
            inputActions.Main.Drag.canceled += OnDragEnded;

            bindingContext.Bind(cameraContainer, x => x.Cameras, OnCamerasChanged);
        }

        private void OnCamerasChanged(IList<CameraReference> obj)
        {
            if (cameraContainer.TryGetCameraById("MainCamera", out var cameraReference))
                mainCamera = cameraReference.Camera;
        }

        private void OnDragStarted(InputAction.CallbackContext obj)
        {
            selectionModel.DragStartPosition = inputActions.Main.MousePosition.ReadValue<Vector2>();
            selectionModel.Dragging = true;
        }

        private void OnDragEnded(InputAction.CallbackContext obj)
        {
            selectionModel.DragEndPosition = inputActions.Main.MousePosition.ReadValue<Vector2>();
            selectionModel.Dragging = false;

            ChangeSelection(selectionModel.DragStartPosition, selectionModel.DragEndPosition);
        }

        private void OnClickPreformed(InputAction.CallbackContext obj)
        {
            var pointerData = new PointerEventData(EventSystem.current)
            {
                position = inputActions.Main.MousePosition.ReadValue<Vector2>()
            };

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);
            if (!raycastResults.Any()) ChangeSelection(inputActions.Main.MousePosition.ReadValue<Vector2>());
        }

        private void ChangeSelection(Vector2 cursorPosition)
        {
            if (mainCamera == null) return;

            var hitPoint = mainCamera.ScreenToWorldPoint(cursorPosition);
            var hitCount = Physics2D.OverlapPointNonAlloc(hitPoint, results);
            ProcessHits(hitCount, hitPoint: hitPoint);
        }

        private void ChangeSelection(Vector2 corner1, Vector2 corner2)
        {
            if (Camera.main == null) return;

            var camera = Camera.main;

            corner1 = camera.ScreenToWorldPoint(corner1);
            corner2 = camera.ScreenToWorldPoint(corner2);

            var max = Vector2.Max(corner1, corner2);
            var min = Vector2.Min(corner1, corner2);

            var hitCount = Physics2D.OverlapAreaNonAlloc(min, max, results);
            var center = (max + min) / 2f;
            Vector3 size = max - min;
            size.z = 1000f;
            ProcessHits(hitCount, true, new Bounds(center, size));
        }

        private void ProcessHits(int hitCount, bool wasArea = false, Bounds area = default, Vector2 hitPoint = default)
        {
            selectionModel.Selection.Clear();
            if (hitCount == 0) return;

            var newSelection = new List<ISelectable>();

            for (var i = 0; i < hitCount; i++)
            {
                results[i].GetComponentsInChildren(true, selectionBuffer);
                foreach (var selectable in selectionBuffer)
                    if (selectable is SelectableCellGroup scg)
                        if (wasArea) newSelection.AddRange(scg.GetSelectedCells(area));
                        else newSelection.Add(scg.GetSelectedCell(hitPoint));
                    else newSelection.Add(selectable);
            }

            selectionModel.Selection.AddRange(newSelection);
        }

        private void ReleaseUnmanagedResources()
        {
            selectionBuffer.Clear();
            inputActions.Main.LeftClick.performed -= OnClickPreformed;
            inputActions.Main.Drag.performed -= OnDragStarted;
            inputActions.Main.Drag.canceled -= OnDragEnded;
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing) bindingContext?.Dispose();
        }

        ~SelectionController()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}