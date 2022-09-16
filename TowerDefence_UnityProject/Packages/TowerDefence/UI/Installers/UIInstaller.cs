using System.Threading.Tasks;
using TowerDefence.UI.Game.Health;
using TowerDefence.UI.Game.Tower.Range;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace TowerDefence.UI.Installers
{
    [CreateAssetMenu(fileName = "UI Installer", menuName = "Installers/UI Installer")]
    public sealed class UIInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private HealthDrawer healthbarPrefab;

        [SerializeField] private GameObject worldUIContainerPrefab;

        [SerializeField] private GameObject screenUIContainerPrefab;

        [SerializeField] private AssetReference hudControllerPrefab;
        [SerializeField] private AssetReference towerRangeDrawer;

        public override void InstallBindings()
        {
            var uiContainer = new GameObject("UI Container", typeof(UIContainer)).GetComponent<UIContainer>();
            var worldUIContainer = Instantiate(worldUIContainerPrefab, uiContainer.transform, false);
            var screenUIContainer = Instantiate(screenUIContainerPrefab, uiContainer.transform, false);

            uiContainer.Setup(worldUIContainer.transform, screenUIContainer.transform);
            DontDestroyOnLoad(uiContainer);

            Container.BindInstance(uiContainer).AsSingle();
            Container.BindInterfacesAndSelfTo<HealthDrawerController>().AsSingle().NonLazy();
            Container.BindFactory<HealthDrawer, HealthDrawer.Factory>().FromComponentInNewPrefab(healthbarPrefab);

            Container.BindInterfacesAndSelfTo<TowerRangeDrawerController>().AsSingle().WithArguments(towerRangeDrawer).NonLazy();

            InstallAsync();
        }

        private async void InstallAsync()
        {
            await Task.Delay(100);
            var hudPrefab = await this.hudControllerPrefab.LoadAssetAsync<GameObject>() as GameObject;
            Container.InstantiatePrefab(hudPrefab, Container.Resolve<UIContainer>().ScreenUIContainer);
        }
    }
}