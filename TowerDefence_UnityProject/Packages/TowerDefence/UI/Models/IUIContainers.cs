using DataBinding.BaseClasses;
using DataBinding.Helpers;
using System.Collections.Generic;

namespace TowerDefence.UI.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    public interface IUIContainers : IModelBase
    {
        IList<IUIContainer> Containers { get; }
    }
}
