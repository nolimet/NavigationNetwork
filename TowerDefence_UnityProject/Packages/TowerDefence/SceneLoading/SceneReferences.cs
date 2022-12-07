using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Packages.TowerDefence.SceneLoading
{
    [CreateAssetMenu(fileName = "Scene References", menuName = "Configuration/Scene References")]
    public class SceneReferences : ScriptableObject
    {
        [field: SerializeField] public AssetReference MainMenuScene { get; private set; }
        [field: SerializeField] public AssetReference GameScene { get; private set; }

        public UniTask LoadGameScene() => LoadScene(GameScene);
        public UniTask LoadMainMenuScene() => LoadScene(MainMenuScene);

        private static async UniTask LoadScene(AssetReference sceneReference)
        {
            if (sceneReference.IsValid())
            {
                sceneReference.ReleaseAsset();
            }

            await sceneReference.LoadSceneAsync();
        }
    }
}