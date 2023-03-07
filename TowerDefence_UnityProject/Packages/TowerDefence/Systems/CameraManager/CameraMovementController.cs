﻿using System;
using System.Collections.Generic;
using DataBinding;
using TowerDefence.Input;
using TowerDefence.World.Grid;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TowerDefence.Systems.CameraManager
{
    public class CameraMovementController : IDisposable, ITickable
    {
        private readonly ICameraContainer cameraContainer;
        private readonly BindingContext bindingContext = new();
        private readonly CameraSettings settings;
        private readonly InputActions inputActions;
        private readonly GridWorld gridWorld;

        private readonly InputAction rightClickAction, mouseDeltaAction, verticalMoveAction, horizontalMoveAction;

        private Camera targetCamera;

        internal CameraMovementController(ICameraContainer cameraContainer, InputActions inputActions, CameraSettings settings, GridWorld gridWorld)
        {
            this.settings = settings;
            this.cameraContainer = cameraContainer;
            this.inputActions = inputActions;
            this.gridWorld = gridWorld;

            rightClickAction = inputActions.Main.RightClick;
            mouseDeltaAction = inputActions.Main.MouseDelta;
            verticalMoveAction = inputActions.CameraMove.Vertical;
            horizontalMoveAction = inputActions.CameraMove.Horizontal;

            bindingContext.Bind(cameraContainer, x => x.Cameras, OnCamerasChanged);
        }

        private void OnCamerasChanged(IList<CameraReference> obj)
        {
            if (cameraContainer.TryGetCameraById("MainCamera", out var cameraReference))
            {
                targetCamera = cameraReference.Camera;
            }
        }

        public void Tick()
        {
            if (!targetCamera) return;

            Vector3 newPos = targetCamera.transform.position;
            if (rightClickAction.IsPressed())
            {
                var cameraDelta = (Vector3)mouseDeltaAction.ReadValue<Vector2>();
                newPos -= (targetCamera.ScreenToWorldPoint(cameraDelta) - targetCamera.ScreenToWorldPoint(Vector3.zero)) * settings.MoveSpeed;
            }
            // else
            // {
            //     var cameraMove = new Vector3(_horizontalMoveAction.ReadValue<int>(), _verticalMoveAction.ReadValue<int>()) * _settings.MoveSpeed;
            //     newPos = targetCamera.transform.position + cameraMove;
            // }

            var bounds = gridWorld.GetGridBounds();
            targetCamera.transform.position = bounds.Contains(newPos) ? newPos : bounds.ClosestPoint(newPos);
        }

        public void Dispose()
        {
            bindingContext.Dispose();
        }
    }
}