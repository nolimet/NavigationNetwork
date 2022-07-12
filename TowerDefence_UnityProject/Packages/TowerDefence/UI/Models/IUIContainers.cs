using DataBinding.BaseClasses;
using DataBinding.Helpers;
using System.Collections.Generic;

namespace TowerDefence.UI.Models
{
    [DataModel(AddToZenject = true, Shared = true)]
    internal interface IUIContainers : IModelBase
    {
        IList<IUIContainer> UIContainers { get; }
    }
}
