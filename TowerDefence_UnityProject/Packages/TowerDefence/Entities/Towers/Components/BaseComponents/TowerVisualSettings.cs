using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TowerDefence.Entities.Components;
using TowerDefence.Entities.Towers.Components.Interfaces;
using TowerDefence.Entities.Towers.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace TowerDefence.Entities.Towers.Components.BaseComponents
{
    [Serializable, Component(ComponentType.Tower, AllowMultiple = false)]
    [JsonObject(MemberSerialization.OptIn)]
    public class TowerVisualSettings : IComponent, IAsyncInitializer, IDisposable
    {
        [JsonProperty] [SerializeField] private AssetReferenceT<Sprite> towerSprite;


        public async Task AsyncPostInit(ITowerObject towerObject, ITowerModel towerModel)
        {
            var spriteRenderer = towerObject.Transform.GetComponent<SpriteRenderer>();

            var loadTask = towerSprite.LoadAssetAsync();
            if (!loadTask.IsDone) await loadTask;

            if (towerSprite.IsValid()) spriteRenderer.sprite = loadTask.Result;
        }

        public void Dispose()
        {
            towerSprite.ReleaseAsset();
        }
    }
}