using TowerDefence.World.Path;
using UnityEngine;
using Zenject;

namespace TowerDefence.World
{
    [CreateAssetMenu(fileName = "World Installer", menuName = "Installers/World Installer")]
    public class WorldInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private PathLineRenderer lineGenericPrefab;

        public override void InstallBindings()
        {
            var worldContainer = new GameObject("World Container").AddComponent<WorldContainer>();
            worldContainer.DoSetup();
            worldContainer.transform.position = Vector3.zero;

            Container.BindInstance(worldContainer);
            Container.BindFactory<PathRendererBase, PathRendererBase.Factory>().FromComponentInNewPrefab(lineGenericPrefab);

            Container.BindInterfacesAndSelfTo<PathWalkerService>().AsSingle();
            Container.Bind<PathBuilderService>().AsSingle();
            Container.Bind<WorldController>().AsSingle();
        }
    }
}