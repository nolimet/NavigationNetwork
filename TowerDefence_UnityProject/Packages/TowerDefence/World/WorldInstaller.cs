using TowerDefence.Systems.WorldLoader;
using TowerDefence.World.Grid;
using UnityEngine;
using Zenject;

namespace TowerDefence.World
{
    [CreateAssetMenu(fileName = "World Installer", menuName = "Installers/World Installer")]
    public class WorldInstaller : ScriptableObjectInstaller
    {
        // [SerializeField] private PathLineRenderer lineGenericPrefab;

        [SerializeField] private GridWorldSettings gridWorldSettings;

        public override void InstallBindings()
        {
            var worldContainer = new GameObject("World Container").AddComponent<WorldContainer>();
            worldContainer.DoSetup();
            worldContainer.transform.position = Vector3.zero;
            DontDestroyOnLoad(worldContainer);

            Container.BindInstance(worldContainer);
            // Container.BindFactory<PathRendererBase, PathRendererBase.Factory>().FromComponentInNewPrefab(lineGenericPrefab);

            // Container.BindInterfacesAndSelfTo<PathWalkerService>().AsSingle();
            // Container.Bind<PathBuilderService>().AsSingle();
            // Container.Bind<PathWorldController>().AsSingle();

            Container.Bind<GridWorld>().AsSingle();
            Container.Bind<GridVisualGenerator>().AsSingle();
            Container.Bind<GridGenerator>().AsSingle();
            Container.Bind<GridCellSelector>().AsSingle().NonLazy();

            Container.Bind<WorldLoadController>().AsSingle();

            Container.BindInstance(gridWorldSettings).AsSingle();
        }
    }
}