using System;
using System.Collections.Generic;
using DataBinding;
using TowerDefence.Input;
using TowerDefence.World.Grid;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TowerDefence.Systems.CameraManager
{
    internal class CameraZoomController : IDisposable, ITickable
    {
        private readonly ICameraContainer cameraContainer;
        private readonly InputActions inputActions;
        private readonly CameraSettings settings;
        private readonly GridWorld gridWorld;

        private readonly BindingContext bindingContext = new();

        private Camera targetCamera;

        public CameraZoomController(ICameraContainer cameraContainer, InputActions inputActions, CameraSettings settings, GridWorld gridWorld)
        {
            this.cameraContainer = cameraContainer;
            this.inputActions = inputActions;
            this.settings = settings;
            this.gridWorld = gridWorld;

            bindingContext.Bind(cameraContainer, m => m.Cameras, OnCamerasChanged);

            inputActions.Main.MouseWheel.performed += OnScroll;
        }

        private void OnCamerasChanged(IList<CameraReference> obj)
        {
            if (cameraContainer.TryGetCameraById("MainCamera", out var cameraReference)) targetCamera = cameraReference.Camera;
        }

        private void OnScroll(InputAction.CallbackContext obj)
        {
            var scroll = obj.ReadValue<Vector2>().normalized;
            var currentZoom = targetCamera.orthographicSize;

            var currentZoomLevel = Mathf.Floor(currentZoom / settings.ZoomStepSize);
            var bounds = gridWorld.GetGridBounds().extents;

            var maxZoom = Mathf.Max(settings.MinZoom, bounds.y);

            targetCamera.orthographicSize = Mathf.Clamp((currentZoomLevel + -scroll.y) * settings.ZoomStepSize, settings.MinZoom, maxZoom);
        }

        public void Dispose()
        {
            bindingContext.Dispose();

            inputActions.Main.MouseWheel.performed -= OnScroll;
        }

        public void Tick()
        {
        }
    }
}
