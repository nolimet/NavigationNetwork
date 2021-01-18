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
            Container.BindFactory<PathLineRenderer, PathLineRenderer.Factory>().FromComponentInNewPrefab(lineGenericPrefab);

            Container.Bind<PathBuilderService>().AsSingle();
            Container.Bind<WorldController>().AsSingle();
        }
    }
}