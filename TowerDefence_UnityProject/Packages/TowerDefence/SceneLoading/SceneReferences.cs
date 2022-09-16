using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Packages.TowerDefence.SceneLoading
{
    [CreateAssetMenu(fileName = "Scene References", menuName = "Configuration/Scene References")]
    public class SceneReferences : ScriptableObject
    {
        [field: SerializeField] public AssetReference MainMenuScene { get; private set; }
        [field: SerializeField] public AssetReference GameScene { get; private set; }
    }
}