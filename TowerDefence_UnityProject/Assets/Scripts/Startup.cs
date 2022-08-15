using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace TowerDefence.Project
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private AssetReference nextScene;
        private async void Start()
        {
            await new WaitForSeconds(0.5f);
            await nextScene.LoadSceneAsync();
        }
    }
}