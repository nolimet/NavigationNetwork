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
    public class CameraMovementController : IDisposable, ITickable
    {
        private readonly ICameraContainer _cameraContainer;
        private readonly BindingContext _bindingContext = new();
        private readonly CameraSettings _settings;
        private readonly InputActions _inputActions;
        private readonly GridWorld _gridWorld;

        private readonly InputAction _rightClickAction, _mouseDeltaAction, _verticalMoveAction, _horizontalMoveAction;

        private Camera targetCamera;

        internal CameraMovementController(ICameraContainer cameraContainer, InputActions inputActions, CameraSettings settings, GridWorld gridWorld)
        {
            _settings = settings;
            _cameraContainer = cameraContainer;
            _inputActions = inputActions;
            _gridWorld = gridWorld;

            _rightClickAction = inputActions.Main.RightClick;
            _mouseDeltaAction = inputActions.Main.MouseDelta;
            _verticalMoveAction = inputActions.CameraMove.Vertical;
            _horizontalMoveAction = inputActions.CameraMove.Horizontal;

            _bindingContext.Bind(cameraContainer, x => x.Cameras, OnCamerasChanged);
        }

        private void OnCamerasChanged(IList<CameraReference> obj)
        {
            if (_cameraContainer.TryGetCameraById("MainCamera", out var cameraReference))
            {
                targetCamera = cameraReference.Camera;
            }
        }

        public void Tick()
        {
            if (!targetCamera) return;

            Vector3 newPos;
            if (_rightClickAction.IsPressed())
            {
                var cameraDelta = (Vector3)_mouseDeltaAction.ReadValue<Vector2>();
                newPos = targetCamera.transform.position - cameraDelta / Screen.dpi / (targetCamera.orthographicSize * _settings.PanRation);
            }
            // else
            // {
            //     var cameraMove = new Vector3(_horizontalMoveAction.ReadValue<int>(), _verticalMoveAction.ReadValue<int>()) * _settings.MoveSpeed;
            //     newPos = targetCamera.transform.position + cameraMove;
            // }

            var bounds = _gridWorld.GetGridBounds();
            targetCamera.transform.position = bounds.Contains(newPos) ? newPos : bounds.ClosestPoint(newPos);
        }

        public void Dispose()
        {
            _bindingContext.Dispose();
        }
    }
}