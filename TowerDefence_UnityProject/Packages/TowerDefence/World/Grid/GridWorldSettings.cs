using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.World.Grid
{
    [CreateAssetMenu(menuName = "Configuration/Grid World Settings", fileName = "Grid World Settings")]
    public class GridWorldSettings : ScriptableObject
    {
        [SerializeField] private AssetReferenceT<Material> tileMaterial;
        [SerializeField] private Vector2 tileSize = Vector2.one;

        public Vector2 TileSize => tileSize;

        public async UniTask<Material> GetTileMaterial()
        {
            if (!tileMaterial.IsDone)
            {
                var task = tileMaterial.LoadAssetAsync();
                await task;
                return task.Result;
            }
            else
            {
                return tileMaterial.Asset as Material;
            }
        }
    }
}