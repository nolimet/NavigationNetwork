using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.World.Grid
{
    [CreateAssetMenu(menuName = "Configuration/Grid World Settings", fileName = "Grid World Settings")]
    public sealed class GridWorldSettings : ScriptableObject
    {
        [SerializeField] private AssetReferenceT<Material> tileMaterial;
        [SerializeField] private AssetReferenceT<GameObject> entrancePrefab;
        [SerializeField] private AssetReferenceT<GameObject> exitPrefab;
        [SerializeField] private Vector2 tileSize = Vector2.one;
        [SerializeField] private Vector2Int tileGroupSize = Vector2Int.one * 8;
        [SerializeField] private Vector2Int tileGroupSubDivideCount = Vector2Int.one * 3;

        public Vector2 TileSize => tileSize;
        public Vector2Int TileGroupSize => tileGroupSize;
        public Vector2Int TileGroupGroupSubDivideCount => tileGroupSubDivideCount;

        private async UniTask<T> GetAddressableTResult<T>(AssetReferenceT<T> reference) where T : Object
        {
            if (!reference.Asset)
            {
                await reference.LoadAssetAsync();
            }

            return reference.Asset as T;
        }

        public UniTask<Material> GetTileMaterial() => GetAddressableTResult(tileMaterial);
        public UniTask<GameObject> GetTileEntrance() => GetAddressableTResult(entrancePrefab);
        public UniTask<GameObject> GetTileExit() => GetAddressableTResult(exitPrefab);
    }
}