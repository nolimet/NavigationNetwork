using System;
using UnityEngine;

namespace TowerDefence.Systems.CameraManager
{
    [Serializable]
    public class CameraSettings
    {
        public float PanSpeed = 1f;
        public float MoveSpeed = 1f;

        [field: SerializeField] public float MinZoom { get; private set; } = 2f;
        [field: SerializeField] public float ZoomStepSize { get; private set; } = 1.2f;
    }
}
