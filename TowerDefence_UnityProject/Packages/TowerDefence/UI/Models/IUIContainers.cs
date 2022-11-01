using System.Collections.Generic;
using DataBinding.BaseClasses;
using DataBinding.Helpers;
using NoUtil.Extentsions;

namespace TowerDefence.UI.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface IUIContainers : IModelBase
    {
        IList<IUIContainer> Containers { get; }

        public bool TryGetContainerWithId(string id, out IUIContainer uiContainer)
        {
            return Containers.TryFind(x => x.Name == id, out uiContainer);
        }
    }
}