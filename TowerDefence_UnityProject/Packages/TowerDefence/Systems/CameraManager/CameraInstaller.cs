using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace TowerDefence.Systems.CameraManager
{
    [CreateAssetMenu(fileName = "Camera Installer", menuName = "Installers/Camera Installer")]
    public class CameraInstaller : ScriptableObjectInstaller<CameraInstaller>
    {
        [FormerlySerializedAs("_cameraSettings")] [SerializeField]
        private CameraSettings cameraSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(cameraSettings).AsSingle();
            Container.BindInterfacesTo<CameraMovementController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraZoomController>().AsSingle().NonLazy();
        }
    }
}