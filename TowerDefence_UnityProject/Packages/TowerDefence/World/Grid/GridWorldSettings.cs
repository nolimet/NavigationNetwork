using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.World.Grid
{
    [CreateAssetMenu(menuName = "Configuration/Grid World Settings", fileName = "Grid World Settings")]
    public sealed class GridWorldSettings : ScriptableObject
    {
        [SerializeField] private AssetReferenceT<Material> tileMaterial;
        [SerializeField] private AssetReferenceT<GameObject> entracePrefab;
        [SerializeField] private AssetReferenceT<GameObject> exitPrefab;
        [SerializeField] private Vector2 tileSize = Vector2.one;

        public Vector2 TileSize => tileSize;

        public async UniTask<Material> GetTileMaterial()
        {
            if (!tileMaterial.Asset)
            {
                await tileMaterial.LoadAssetAsync();
            }

            return tileMaterial.Asset as Material;
        }
    }
}