using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.World.Grid
{
    [CreateAssetMenu(menuName = "Configuration/Grid World Settings", fileName = "Grid World Settings")]
    public class GridWorldSettings : ScriptableObject
    {
        [SerializeField] private AssetReferenceT<Shader> tileShader;
        [SerializeField] private AssetReferenceT<Texture2D> tileTexture;
        [SerializeField] private Vector2 tileSize = Vector2.one;

        public Vector2 TileSize => tileSize;

        public async UniTask<Shader> GetTileShader()
        {
            if (!tileShader.IsDone)
            {
                var task = tileShader.LoadAssetAsync();
                await task;
                return task.Result;
            }
            else
            {
                return tileShader.Asset as Shader;
            }
        }

        public async UniTask<Texture2D> GetTileTexture()
        {
            await tileTexture.LoadAssetAsync();
            return tileTexture.Asset as Texture2D;
        }
    }
}