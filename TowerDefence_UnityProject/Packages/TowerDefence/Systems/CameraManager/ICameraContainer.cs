using System.Collections.Generic;
using DataBinding.BaseClasses;
using DataBinding.Helpers;
using NoUtil.Extentsions;

namespace TowerDefence.Systems.CameraManager
{
    [DataModel(Shared = true, AddToZenject = true)]
    public interface ICameraContainer : IModelBase
    {
        IList<CameraReference> Cameras { get; }

        public bool TryGetCameraById(string id, out CameraReference cameraReference)
        {
            id = id.ToLower();
            return Cameras.TryFind(x => x.Id.ToLower() == id, out cameraReference);
        }
    }
}