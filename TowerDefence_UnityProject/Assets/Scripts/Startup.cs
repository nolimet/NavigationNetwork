using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Project
{
    public sealed class Startup : MonoBehaviour
    {
        [SerializeField] private AssetReference nextScene;
        private async void Start()
        {
            await new WaitForSeconds(0.5f);
            await nextScene.LoadSceneAsync();
        }
    }
}