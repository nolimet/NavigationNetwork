using UnityEngine;
using Zenject;

namespace TowerDefence.UI.Installers
{
    [CreateAssetMenu(fileName = "Project UI Installer", menuName = "Installers/UI Installers/Project")]
    public class ProjectUIInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameObject worldUIContainerPrefab;

        [SerializeField] private GameObject screenUIContainerPrefab;

        public override void InstallBindings()
        {
            var uiContainer = new GameObject("UI Container", typeof(UIContainer)).GetComponent<UIContainer>();
            var worldUIContainer = Instantiate(worldUIContainerPrefab, uiContainer.transform, false);
            var screenUIContainer = Instantiate(screenUIContainerPrefab, uiContainer.transform, false);

            uiContainer.Setup(worldUIContainer.transform, screenUIContainer.transform);
            DontDestroyOnLoad(uiContainer);

            Container.BindInstance(uiContainer).AsSingle();
        }
    }
}