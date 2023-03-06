using UnityEngine;
using Zenject;

namespace TowerDefence.Systems.CameraManager
{
    [CreateAssetMenu(fileName = "Camera Installer", menuName = "Installers/Camera Installer")]
    public class CameraInstaller : ScriptableObjectInstaller<CameraInstaller>
    {
        [SerializeField] private CameraSettings _cameraSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(_cameraSettings).AsSingle();
            Container.BindInterfacesTo<CameraMovementController>().AsSingle().NonLazy();
        }
    }
}