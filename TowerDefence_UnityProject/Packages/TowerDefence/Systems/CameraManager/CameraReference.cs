using UnityEngine;
using Zenject;

namespace TowerDefence.Systems.CameraManager
{
    public sealed class CameraReference : MonoBehaviour
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }

        [Inject] private ICameraContainer cameraContainer;

        private void Awake() => cameraContainer.Cameras.Add(this);

        private void OnDestroy() => cameraContainer.Cameras.Remove(this);

        private void Reset()
        {
            if (Camera) return;
            Camera = GetComponent<Camera>();
            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = Camera.tag;
            }
        }
    }
}