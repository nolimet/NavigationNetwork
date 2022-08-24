using UnityEngine;
using Zenject;

namespace TowerDefence.Packages.TowerDefence.SceneLoading
{
    public class SceneLoadingInstaller : MonoInstaller
    {
        [SerializeField] private SceneReferences sceneReferences;
        public override void InstallBindings()
        {
            Container.BindInstance(sceneReferences).AsSingle();
        }
    }
}