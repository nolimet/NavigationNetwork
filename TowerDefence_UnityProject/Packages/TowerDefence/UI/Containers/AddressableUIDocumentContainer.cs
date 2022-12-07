using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

namespace TowerDefence.UI.Containers
{
    internal sealed class AddressableUIDocumentContainer : UIDocumentContainer
    {
        [SerializeField] private AssetReferenceT<VisualTreeAsset> visualTreeAsset;
        private AsyncOperationHandle<VisualTreeAsset> assetReference;


        protected override async void Start()
        {
            if (!visualTreeAsset.IsValid())
            {
                assetReference = visualTreeAsset.LoadAssetAsync();
                await assetReference;
            }

            Document.visualTreeAsset = assetReference.Result;

            base.Start();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Document.visualTreeAsset = null;
            visualTreeAsset.ReleaseAsset();
        }
    }
}